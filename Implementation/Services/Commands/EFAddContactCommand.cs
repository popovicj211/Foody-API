using Application.Commands;
using Application.DataTransfer;
using AutoMapper;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Commands
{
    public class EFAddContactCommand : BaseService, IAddContactCommand
    {
        private readonly IMapper _mapper;
        public EFAddContactCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public void Execute(ContactDTO request)
        {
            var mappingToDto = _mapper.Map<ContactEntity>(new ContactDTO
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
