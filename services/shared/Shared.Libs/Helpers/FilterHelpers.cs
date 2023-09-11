namespace ECommerce.Shared.Libs.Helpers;

public static class FilterHelpers
{
    public static Expression<Func<TEntity, bool>> BuildSearchPredicate<TEntity>(string keyword, string[] searchProperties)
    {
        var parameter = Expression.Parameter(typeof(TEntity), nameof(TEntity));
        var dbFunctionsParameter = Expression.Parameter(typeof(DbFunctions), nameof(DbFunctions));
        var predicate = (Expression)(Expression.Constant(false));

        var compareMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) })!;
        var toStringMethod = typeof(object).GetMethod(nameof(object.ToString))!;

        foreach (var property in searchProperties)
        {
            var propertyInfo = typeof(TEntity).GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo != null)
            {

                var propertyExpression = Expression.Property(parameter, propertyInfo);
                var compareExpression = ExpressionHelpers.ILike(propertyExpression, keyword);

                predicate = Expression.OrElse(predicate, compareExpression);
            }
        }

        return Expression.Lambda<Func<TEntity, bool>>(predicate, parameter);
    }
}