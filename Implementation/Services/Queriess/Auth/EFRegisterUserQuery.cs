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
                Password = _hasher.HashPassword(request.Password),
                RoleId = request.RoleId,
            });

            _context.Users.Add(mappingToDto);
            _context.SaveChanges();


            var tokenHandler = new JwtSecurityTokenHandler();
            string key = _config.GetSection("JwtKey").Value;
            var keyBytes = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", mappingToDto.Id.ToString()),
                    new Claim(ClaimTypes.Email, mappingToDto.Email),
                    new Claim(ClaimTypes.GivenName, $"{mappingToDto.FirstName} {mappingToDto.LastName}"),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
 }

