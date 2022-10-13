using Common.Query;
using Shop.Query.Category.DTOs;

namespace Shop.Query.Category.GetByParentId;

public record GetCategoryByParentIdQuery(long ParentId) : IQuery<List<ChildCategoryDto>>;