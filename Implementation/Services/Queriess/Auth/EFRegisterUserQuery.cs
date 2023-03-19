using Application.DataTransfer;
using Application.Exceptions;
using Application.Helpers;
using Application.Interfaces;
using Application.Queries.Auth;
using AutoMapper;
using Azure.Core;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;
using Implementation.Services.Exstensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Queriess.Auth
{
    public class EFRegisterUserQuery : BaseService, IRegisterUserQuery
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IPasswordHashing _hasher;

        public EFRegisterUserQuery(DBContext context, IMapper mapper, IConfiguration config, IPasswordHashing hasher) : base(context)
        {
            _mapper = mapper;
            _config = config;
            _hasher = hasher;
        }

        public string Execute(UserDTO request)
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
                Password = _hasher.HashPassword(request.Password)
            });

            _context.Users.Add(mappingToDto);
            _context.SaveChanges();

            var getJwtToken = _config.GetJwtToken(request);

            return getJwtToken;
        }
    }
 }

