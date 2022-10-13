using Common.Application.Validation;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.CategoryAgg;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.EF;

namespace Shop.Infrastructure.Persistent.Ef.CategoryAgg;

internal class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ShopContext context) : base(context)
    {
    }

    public async Task<bool> DeleteCategory(long categoryId)
    {
        var category =await Context.Categories
            .Include(c=>c.Children)
            .ThenInclude(c=>c.Children).FirstOrDefaultAsync(f => f.Id == categoryId);
        if (category == null)
            return false;


        var isExistProduct = await Context.Products
            .AnyAsync(f => f.CategoryId == categoryId ||
                           f.SubCategoryId == categoryId ||
                           f.SeconderySubCategory == categoryId);

        if (isExistProduct)
            return false;

        if (category.Children.Any(c => c.Children.Any()))
        {
            Context.RemoveRange(category.Children.SelectMany(s=>s.Children));
        }
        Context.RemoveRange(category.Children);
        Context.RemoveRange(category);
        return true;
    }
}