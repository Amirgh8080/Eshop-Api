
using Common.Application;

namespace Shop.Application.Comments.Edit
{
    public record EditCommentCommand(long commentId,string text,long userId):IBaseCommand;
}
