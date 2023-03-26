using Application.DataTransfer;
using Application.Helpers;
using Application.Pagination;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Queriess
{
    public class EFGetContactsQuery : BaseService, IGetContactsQuery
    {
        private readonly IMapper _mapper;

        public EFGetContactsQuery(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public PagedResponse<ContactDTO> Execute(BaseSearchRequest request)
        {
            var contacts = _context.Contacts.AsQueryable();

            return contacts.Where(d => !d.IsDeleted).ProjectTo<ContactDTO>(this._mapper.ConfigurationProvider).Select(u => new ContactDTO
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Message = u.Message
            }).Paginate(request.PerPage, request.Page);
        }
    }
}
