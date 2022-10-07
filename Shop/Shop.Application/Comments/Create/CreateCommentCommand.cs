
using Common.Application;

namespace Shop.Application.Comments.Create
{
    public record CreateCommentCommand(long userId, long productId, string text):IBaseCommand;
}
