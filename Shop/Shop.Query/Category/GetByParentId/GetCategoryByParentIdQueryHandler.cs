using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Category.DTOs;

namespace Shop.Query.Category.GetByParentId;

public class GetCategoryByParentIdQueryHandler : IQueryHandler<GetCategoryByParentIdQuery,List<ChildCategoryDto>>
{
    private readonly ShopContext _context;

    public GetCategoryByParentIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<ChildCategoryDto>> Handle(GetCategoryByParentIdQuery request, CancellationToken cancellationToken)
    {
        var result= await _context.Categories.Where(c => c.ParentId == request.ParentId)
            .ToListAsync(cancellationToken);

        return result.MapChildren();
    }
}