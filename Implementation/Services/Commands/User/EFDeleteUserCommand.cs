using Application.Commands.User;
using Application.Exceptions;
using AutoMapper;
using EFDataAccess;
using Implementation.EFServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.User
{
    public class EFDeleteUserCommand : BaseService, IDeleteUserCommand
    {
        private readonly IMapper _mapper;
        public EFDeleteUserCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public void Execute(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException("User");
            }

            if (user.IsDeleted == true)
            {
                throw new AlreadyExistException();
            }

            user.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
