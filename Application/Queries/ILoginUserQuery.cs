using Application.DataTransfer;
using Application.Interfaces;

namespace Application.Queries
{
    public interface ILoginUserQuery : IQuery<LoginDTO, string>
    {
    }
}
