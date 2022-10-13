using Common.Query;
using Shop.Query.Category.DTOs;

namespace Shop.Query.Category.GetById;

public record GetCategoryByIdQuery(long CategoryId) : IQuery<CategoryDto>; 