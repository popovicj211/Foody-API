using Application.Commands;
using Application.DataTransfer;
using AutoMapper;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Commands
{
    public class EFAddContactCommand : BaseService, IAddContactCommand
    {
        private readonly IMapper _mapper;

        public EFAddContactCommand(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public void Execute(ContactDTO request)
        {
            var mappingToDto = this._mapper.Map<ContactEntity>(new ContactDTO
            {
                FullName = request.FullName,
                Email = request.Email,
                Message = request.Message
            });

            _context.Contacts.Add(mappingToDto);
            _context.SaveChanges();
        }
    }
}
