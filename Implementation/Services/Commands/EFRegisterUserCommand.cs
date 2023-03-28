using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;
using Implementation.Services.Exstensions;
using Microsoft.Extensions.Configuration;

namespace Implementation.Services.Commands
{
    public class EFRegisterUserCommand : BaseService, IRegisterUserCommand
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IPasswordHashing _hasher;

        public EFRegisterUserCommand(DBContext context, IMapper mapper, IConfiguration config, IPasswordHashing hasher) : base(context)
        {
            this._mapper = mapper;
            this._config = config;
            this._hasher = hasher;
        }

        public string Execute(UserDTO request)
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
                Password = this._hasher.HashPassword(request.Password)
            });

            _context.Users.Add(mappingToDto);
            _context.SaveChanges();

            var getJwtToken = this._config.GetJwtToken(mappingToDto);

            return getJwtToken;
        }
    }
}
