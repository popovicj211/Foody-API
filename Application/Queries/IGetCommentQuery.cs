using Application.DataTransfer;
using Application.Interfaces;

namespace Application.Queries
{
    public interface IGetCommentQuery : IQuery<int, CommentDTO>
    {
    }
}
