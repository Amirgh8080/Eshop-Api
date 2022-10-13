using Common.Query;
using Shop.Query.Category.DTOs;

namespace Shop.Query.Category.GetList;

public record GetCategoryListQuery : IQuery<List<CategoryDto>>;