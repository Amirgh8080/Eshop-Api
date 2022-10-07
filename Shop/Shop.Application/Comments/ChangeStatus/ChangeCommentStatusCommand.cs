

using Common.Application;
using FluentValidation;
using Shop.Domain.CommentAgg;

namespace Shop.Application.Comments.ChangeStatus
{
    public record ChangeCommentStatusCommand(long CommentId, CommentStatus Status) :IBaseCommand;
   
}
