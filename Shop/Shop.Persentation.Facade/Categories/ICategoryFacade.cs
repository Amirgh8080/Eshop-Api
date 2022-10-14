using Common.Application;
using Shop.Application.Categories.Add_Child;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Delete;
using Shop.Application.Categories.Edit;
using Shop.Query.Category.DTOs;

namespace Shop.Persentation.Facade.Categories;

public interface ICategoryFacade
{
    Task<OperationResult> AddChild(AddChildCategoryCommand command);
    Task<OperationResult> Create(CreateCategoryCommand command);
    Task<OperationResult> Edit(EditCategoryCommand command);
    Task<OperationResult> Delete(long categoryId);



    Task<CategoryDto> GetById(long id);
    Task<List<ChildCategoryDto>> GetCategoriesByParentId(long parentId);
    Task<List<CategoryDto>> GetAllCategories();
}