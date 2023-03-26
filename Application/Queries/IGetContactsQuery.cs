using Application.DataTransfer;
using Application.Interfaces;
using Application.Pagination;
using Application.Searches;

namespace Application.Queries
{
    public interface IGetContactsQuery : IQuery<BaseSearchRequest, PagedResponse<ContactDTO>>
    {
    }
}
