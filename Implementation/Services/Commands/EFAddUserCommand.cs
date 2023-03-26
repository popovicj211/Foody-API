using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Commands
{
    public class EFAddUserCommand : BaseService, IAddUserCommand
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHashing _hasher;

        public EFAddUserCommand(DBContext context, IMapper mapper, IPasswordHashing hasher) : base(context)
        {
            this._mapper = mapper;
            this._hasher = hasher;
        }

        public void Execute(UserDTO request)
        {
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                throw new AlreadyExistException();
            }

            var mappingToDto = this._mapper.Map<UserEntity>(new UserDTO
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Email = request.Email,
                Password = this._hasher.HashPassword(request.Password),
                RoleId = request.RoleId,
            });

            _context.Users.Add(mappingToDto);
            _context.SaveChanges();
        }
    }
}
