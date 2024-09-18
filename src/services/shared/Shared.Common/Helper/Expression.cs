using System.Linq.Expressions;

namespace ECommerce.Shared.Common.Helper;

public static class PredicateBuilder
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
        var body = Expression.AndAlso(expr1.Body, invokedExpr);
        return Expression.Lambda<Func<T, bool>>(body, expr1.Parameters);
    }

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
        var body = Expression.OrElse(expr1.Body, invokedExpr);
        return Expression.Lambda<Func<T, bool>>(body, expr1.Parameters);
    }
}