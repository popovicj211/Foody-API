using Application.DataTransfer;
using Application.Interfaces;

namespace Application.Queries
{
    public interface IGetContactQuery : IQuery<int, ContactDTO>
    {
    }
}
