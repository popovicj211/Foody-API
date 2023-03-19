﻿using Application.Commands.User;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Commands.User
{
    public class EFUpdateUserCommand : BaseService, IUpdateUserCommand
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHashing _hasher;

        public EFUpdateUserCommand(DBContext context, IMapper mapper, IPasswordHashing hasher) : base(context)
        {
            _mapper = mapper;
            _hasher = hasher;
        }

        public void Execute(UserDTO request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == request.Id);
          //  SQL_Latin1_General_CP1_CI_AS
            if (user == null)
            {
                throw new EntityNotFoundException("User");
            }

            if (request.Email != null)
            {
                if (!_context.Users.Any(u => u.Email == request.Email))
                {
                    user.Email = request.Email;
                }
            }

            if (user.FirstName != request.FirstName)
            {
                user.FirstName = request.FirstName;
            }

            if (user.LastName != request.LastName)
            {
                user.LastName = request.LastName;
            }

            if (user.Username != request.Username)
            {
                user.Username = request.Username;
            }

            bool isPasswordSame = _hasher.ValidatePassword(request.Password, user.Password);

            if (!isPasswordSame)
            {
                user.Password = _hasher.HashPassword(request.Password);
            }

            if (request.RoleId != 0)
            {
                if (user.RoleId != request.RoleId)
                {
                    if (_context.Roles.Any(r => r.Id == request.RoleId))
                    {
                        user.RoleId = request.RoleId;
                    }
                    else
                    {
                        throw new EntityNotFoundException("Role");
                    }
                }
            }

            _context.SaveChanges();
        }
    }
}
