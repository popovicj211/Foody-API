using Application.DataTransfer;
using Application.Interfaces;

namespace Application.Queries
{
    public interface IGetUserQuery : IQuery<int, UserDTO>
    {
    }
}
