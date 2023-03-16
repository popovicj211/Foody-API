using Application.DataTransfer;
using Application.Interfaces;
using Application.Pagination;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public interface IGetContactsQuery : IQuery<BaseSearchRequest, PagedResponse<ContactDTO>>
    {
    }
}
