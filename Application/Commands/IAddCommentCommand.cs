using Application.DataTransfer;
using Application.Interfaces;

namespace Application.Commands
{
    public interface IAddCommentCommand : ICommand<CommentDTO>
    {
    }
}
