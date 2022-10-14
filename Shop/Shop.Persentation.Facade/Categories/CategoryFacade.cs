using Common.Application;
using MediatR;
using Shop.Application.Categories.Add_Child;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Delete;
using Shop.Application.Categories.Edit;
using Shop.Query.Category.DTOs;
using Shop.Query.Category.GetById;
using Shop.Query.Category.GetByParentId;
using Shop.Query.Category.GetList;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Persentation.Facade.Categories;

internal class CategoryFacade:ICategoryFacade
{
    private readonly IMediator _mediator;

    public CategoryFacade(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationResult> AddChild(AddChildCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Create(CreateCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Delete(long categoryId)
    {
        return await _mediator.Send(new DeleteCategoryCommand(categoryId));
    }

    public async Task<CategoryDto> GetById(long id)
    {
        return await _mediator.Send(new GetCategoryByIdQuery(id));
    }

    public async Task<List<ChildCategoryDto>> GetCategoriesByParentId(long parentId)
    {
        return await _mediator.Send(new GetCategoryByParentIdQuery(parentId));
    }

    public async Task<List<CategoryDto>> GetAllCategories()
    {
        return await _mediator.Send(new GetCategoryListQuery());
    }
}