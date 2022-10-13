using Microsoft.EntityFrameworkCore;
using Shop.Domain.CategoryAgg;
using Shop.Query.Category.DTOs;

namespace Shop.Query.Category;

internal static class CategoryMapper
{
    public static CategoryDto Map(this Domain.CategoryAgg.Category? category)
    {
        if (category == null)
            return null;

        return new CategoryDto()
        {
            Id = category.Id,
            Title = category.Title,
            CreationDate = category.CreationDate,
            SeoData = category.SeoData,
            Slug = category.Slug,
            Children = category.Children.MapChildren()
        };

    }

    public static List<CategoryDto> Map(this List<Domain.CategoryAgg.Category> categories)
    {
        var model = new List<CategoryDto>();

        categories.ForEach(category =>
        {
            model.Add(new CategoryDto()
            {
                Title = category.Title,
                Slug = category.Slug,
                Id = category.Id,
                SeoData = category.SeoData,
                CreationDate = category.CreationDate,
                Children = category.Children.MapChildren()
            });
        });

        return model;
    }
    public static List<ChildCategoryDto> MapChildren(this List<Domain.CategoryAgg.Category> children)
    {
        var model = new List<ChildCategoryDto>();
        children.ForEach(c =>
        {
            model.Add(new ChildCategoryDto()
            {
                Id = c.Id,
                Title = c.Title,
                CreationDate = c.CreationDate,
                SeoData = c.SeoData,
                Slug = c.Slug,
                ParentId = (long)c.ParentId,
                Children =c.Children.MapSecondaryChildren()
            });
        });
        return model;
    }
    private static List<SecondaryChildCategoryDto> MapSecondaryChildren(this List<Domain.CategoryAgg.Category> children)
    {
        var model = new List<SecondaryChildCategoryDto>();
        children.ForEach(c =>
        {
            model.Add(new SecondaryChildCategoryDto()
            {
                Id = c.Id,
                Title = c.Title,
                CreationDate = c.CreationDate,
                SeoData = c.SeoData,
                Slug = c.Slug,
                ParentId = (long)c.ParentId
            });
        });
        return model;
    }
}