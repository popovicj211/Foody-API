using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Commands
{
    public class EFUpdateContactCommand : BaseService, IUpdateContactCommand
    {
        private readonly IMapper _mapper;

        public EFUpdateContactCommand(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public void Execute(ContactDTO request)
        {
            var contact = _context.Contacts.FirstOrDefault(u => u.Id == request.Id);

            if (contact == null)
            {
                throw new EntityNotFoundException("Contact");
            }

            if (contact.FullName != request.FullName)
            {
                contact.FullName = request.FullName;
            }

            if (contact.Email != request.Email)
            {
                contact.Email = request.Email;
            }

            if (contact.Message != request.Message)
            {
                contact.Message = request.Message;
            }

            _context.SaveChanges();
        }
    }
}
