using Application.Commands;
using Application.Exceptions;
using AutoMapper;
using EFDataAccess;
using Implementation.EFServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Commands
{
    public class EFDeleteContactCommand : BaseService, IDeleteContactCommand
    {
        private readonly IMapper _mapper;
        public EFDeleteContactCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public void Execute(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(u => u.Id == id);

            if (contact == null)
            {
                throw new EntityNotFoundException("Contact");
            }

            if (contact.IsDeleted == true)
            {
                throw new AlreadyExistException();
            }

            contact.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
