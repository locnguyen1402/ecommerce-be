using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Shared.Libs;

public static class ExpressionHelpers
{
    public static MethodCallExpression NormalizeString(Expression stringExpression)
    {
        var lowerCaseMethod = typeof(string).GetMethod(nameof(string.ToLower), System.Type.EmptyTypes)!;

        var functions = Expression.Property(null, typeof(EF).GetProperty(nameof(EF.Functions))!);
        var unaccentMethod = typeof(NpgsqlFullTextSearchDbFunctionsExtensions).GetMethod(
                                nameof(NpgsqlFullTextSearchDbFunctionsExtensions.Unaccent),
                                new Type[] { functions.Type, typeof(string) }
                            )!;

        var callExpression = Expression.Call(stringExpression, lowerCaseMethod);
        callExpression = Expression.Call(null, unaccentMethod, functions, callExpression);

        return callExpression;
    }
}