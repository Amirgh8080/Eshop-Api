using Common.Application;
using MediatR;
using Shop.Application.Categories.Delete;
using Shop.Application.Comments.ChangeStatus;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Edit;
using Shop.Query.Comments.DTOs;
using Shop.Query.Comments.GetByFilter;
using Shop.Query.Comments.GetById;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Persentation.Facade.Comments;

public class CommentFacade:ICommentFacade
{
    private IMediator _mediator;

    public CommentFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> ChangeStatus(ChangeCommentStatusCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Create(CreateCommentCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCommentCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Delete(long commandId)
    {
        return await _mediator.Send(new DeleteCategoryCommand(commandId));
    }

    public async Task<CommentDto?> GetCommentById(long id)
    {
        return await _mediator.Send(new GetCommentByIdQuery(id));
    }

    public async Task<CommentFilterResult> GetCommentsByFilter(CommentFilterParams filterParams)
    {
        return await _mediator.Send(new GetCommentByFilterQuery(filterParams));
    }
}