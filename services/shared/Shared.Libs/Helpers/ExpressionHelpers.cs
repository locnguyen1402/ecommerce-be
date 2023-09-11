namespace ECommerce.Shared.Libs.Helpers;

public static class ExpressionHelpers
{
    public static MemberExpression EfFunctions => Expression.Property(null, typeof(EF).GetProperty(nameof(EF.Functions))!);
    public static MethodInfo toStringMethod => typeof(object).GetMethod(nameof(object.ToString), System.Type.EmptyTypes)!;
    public static MethodInfo toLowerMethod => typeof(string).GetMethod(nameof(string.ToLower), System.Type.EmptyTypes)!;

    public static MethodCallExpression ToLower(Expression stringExpression)
    {
        return Expression.Call(stringExpression, toLowerMethod);
    }
    public static MethodCallExpression Unaccent(Expression stringExpression)
    {

        var unaccentMethod = typeof(NpgsqlFullTextSearchDbFunctionsExtensions).GetMethod(
                                nameof(NpgsqlFullTextSearchDbFunctionsExtensions.Unaccent),
                                new Type[] { EfFunctions.Type, typeof(string) }
                            )!;

        var callExpression = Expression.Call(null, unaccentMethod, EfFunctions, stringExpression);

        return callExpression;
    }

    public static MethodCallExpression ILike(MemberExpression member, string keyword)
    {
        var iLikeMethod = typeof(NpgsqlDbFunctionsExtensions).GetMethod(
                                nameof(NpgsqlDbFunctionsExtensions.ILike),
                                new Type[] { EfFunctions.Type, typeof(string), typeof(string) }
                            )!;

        var memberExpression = (Expression)(member);
        if (member.Member.GetMemberType() != typeof(string))
        {
            memberExpression = Expression.Call(member, toStringMethod);
        }

        var normalizedMemberExpression = Unaccent(memberExpression);
        var keywordExpression = Unaccent(Expression.Constant($"%{keyword}%", typeof(string)));
        var compareExpression = Expression.Call(null, iLikeMethod, EfFunctions, normalizedMemberExpression, keywordExpression);

        return compareExpression;
    }
}