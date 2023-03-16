using Application.Commands.User;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Bogus.DataSets;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.User
{
    public class EFAddUserCommand : BaseService, IAddUserCommand
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHashing _hasher;
        public EFAddUserCommand(DBContext context, IMapper mapper, IPasswordHashing hasher) : base(context)
        {
            _mapper = mapper;
            _hasher = hasher;
        }

        public void Execute(UserDTO request)
        { 
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                throw new AlreadyExistException();
            }

           var mappingToDto = _mapper.Map<UserEntity>(new UserDTO
          {
              FirstName = request.FirstName,
              LastName = request.LastName,
              Username = request.Username,
              Email = request.Email,
              Password = _hasher.HashPassword(request.Password),
              RoleId = request.RoleId,
          });

            _context.Users.Add(mappingToDto);
            _context.SaveChanges();
        }

    }
}
