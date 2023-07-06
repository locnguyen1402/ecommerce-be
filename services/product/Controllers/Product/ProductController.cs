using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using ECommerce.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Product;

public class ProductController : BaseController
{
    private readonly IProductRepository _productRepository;
    public ProductController(
            ILogger<ProductController> logger,
            IMapper mapper,
            IProductRepository productRepository
        ) : base(logger, mapper)
    {
        _productRepository = productRepository;
    }

    [HttpGet("v1")]
    public async Task<IActionResult> GetProductV1s([FromQuery] ProductListQuery listQuery)
    {
        var query = _productRepository.Query;

        if (!string.IsNullOrEmpty(listQuery.keyword))
        {
            query = query
            .Select(p => new
            {
                product = p,
                matches = string.Join("", new[] {
                    p.Title.ToLower(),
                    p.Author.ToLower(),
                })
            })
            .Where(p => p.matches.Contains(listQuery.keyword.ToLower()))
            .Select(p => p.product);

            // query = query.Where(p =>
            //             string.Concat(p.Title, p.Author)
            //                 .ToLower()
            //                 .Contains(listQuery.keyword.ToLower())
            //                 );
        }

        query = query.OrderBy(p => p.CreatedAt);

        var sw = Stopwatch.StartNew();
        var list = await PaginationInfo.ToPaginatedListAsync(listQuery.Page, listQuery.PageSize, query);
        Console.WriteLine($"Performance test v1 {sw.ElapsedMilliseconds}ms");
        sw.Stop();

        await PaginationInfo.AttachPaginationInfoToHeader(listQuery.Page, listQuery.PageSize, query);

        return Ok(list);
    }

    [HttpGet("v2")]
    public async Task<IActionResult> GetProductV2s([FromQuery] ProductListQuery listQuery)
    {
        var query = _productRepository.Query;

        if (!string.IsNullOrEmpty(listQuery.keyword))
        {
            query = query.Where(p =>
                            p.Title.ToLower().Contains(EF.Functions.Unaccent(listQuery.keyword.ToLower())) ||
                            p.Author.ToLower().Contains(listQuery.keyword.ToLower())
                                );
        }

        query = query.OrderBy(p => p.CreatedAt);

        var sw = Stopwatch.StartNew();
        var list = await PaginationInfo.ToPaginatedListAsync(listQuery.Page, listQuery.PageSize, query);
        Console.WriteLine($"Performance test v2 {sw.ElapsedMilliseconds}ms");
        sw.Stop();

        await PaginationInfo.AttachPaginationInfoToHeader(listQuery.Page, listQuery.PageSize, query);

        return Ok(list);
    }

    [HttpGet("v3")]
    public async Task<IActionResult> GetProductV3s([FromQuery] ProductListQuery listQuery)
    {
        var query = _productRepository.Query;


        if (!string.IsNullOrEmpty(listQuery.keyword))
        {
            query = query.Where(BuildSearchPredicate<Product>(listQuery.keyword, new string[] { "Title", "Author" }));
        }

        query = query.OrderBy(p => p.CreatedAt);

        var sw = Stopwatch.StartNew();
        var list = await PaginationInfo.ToPaginatedListAsync(listQuery.Page, listQuery.PageSize, query);
        Console.WriteLine($"Performance test v3 {sw.ElapsedMilliseconds}ms");
        sw.Stop();

        await PaginationInfo.AttachPaginationInfoToHeader(listQuery.Page, listQuery.PageSize, query);

        return Ok(list);
    }

    private object? GetPropertyValue(object obj, string propertyName)
    {
        return obj.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)?.GetValue(obj);
    }

    private bool FilterProduct(Product product, string keyword)
    {
        var searchProperties = new string[] { "Title", "Author" };

        foreach (var property in searchProperties)
        {
            var propertyValue = GetPropertyValue(product, property);
            if (propertyValue != null && propertyValue.ToString()!.ToLower().Contains(keyword.ToLower()))
            {
                return true;
            }
        }

        return false;
    }

    private Expression<Func<TEntity, bool>> BuildSearchPredicate<TEntity>(string keyword, string[] searchProperties)
    {
        var parameter = Expression.Parameter(typeof(TEntity), nameof(TEntity));
        var predicate = (Expression)(Expression.Constant(false));

        var lowerCaseMethod = typeof(string).GetMethod("ToLower", System.Type.EmptyTypes)!;
        var compareMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;

        foreach (var property in searchProperties)
        {
            var propertyInfo = typeof(TEntity).GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo != null)
            {

                var propertyExpression = Expression.Property(parameter, propertyInfo);

                var lowerCasePropertyExpression = Expression.Call(propertyExpression, lowerCaseMethod);
                var lowerCaseKeywordExpression = Expression.Call(
                                                    Expression.Constant(
                                                        keyword, typeof(string)
                                                    ),
                                                    lowerCaseMethod
                                                );

                var compareExpression = Expression.Call(lowerCasePropertyExpression, compareMethod, lowerCaseKeywordExpression);

                predicate = Expression.OrElse(compareExpression, predicate);
            }
        }

        return Expression.Lambda<Func<TEntity, bool>>(predicate, parameter);
    }
}