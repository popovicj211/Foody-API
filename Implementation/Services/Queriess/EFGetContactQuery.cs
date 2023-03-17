using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Queriess
{
    public class EFGetContactQuery : BaseService, IGetContactQuery
    {
        private readonly IMapper _mapper;
        public EFGetContactQuery(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public ContactDTO Execute(int id)
        {
            var contact = _context.Contacts.Where(u => !u.IsDeleted).ProjectTo<ContactDTO>(_mapper.ConfigurationProvider).Select(u => new ContactDTO
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Message = u.Message
            }).FirstOrDefault(u => u.Id == id);

            if (contact == null)
            {
                throw new EntityNotFoundException("Contact");
            }

            return contact;
        }
    }
}
