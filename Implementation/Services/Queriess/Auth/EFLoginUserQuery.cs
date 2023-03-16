using Application.DataTransfer;
using Application.Exceptions;
using Application.Interfaces;
using Application.Queries.Auth;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bogus.DataSets;
using EFDataAccess;
using Implementation.EFServices;
using Microsoft.EntityFrameworkCore;
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
    public class EFLoginUserQuery : BaseService, ILoginUserQuery
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IPasswordHashing _hasher;

        public EFLoginUserQuery(DBContext context, IMapper mapper, IConfiguration config, IPasswordHashing hasher) : base(context)
        {
            _mapper = mapper;
            _config = config;
            _hasher = hasher;
        }

        public string Execute(LoginDTO search)
        {
            var user =  _context.Users.Where(u => !u.IsDeleted && u.IsActived && u.Email == search.Email && u.Password == search.Password).Include(r => r.Role).ProjectTo<UserDTO>(_mapper.ConfigurationProvider).Select(u => new UserDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                IsActived = u.IsActived,
                ImagePath = u.ImagePath,
                IsDeleted = u.IsDeleted,
                Role = u.Role
            }).FirstOrDefault();

            if (user == null)
            {
                throw new EntityNotFoundException("User");
            }

            if (!_hasher.ValidatePassword(search.Password, user.Password))
            {
                throw new PasswordNotValidException();
            }
            var a = _hasher.HashPassword(search.Password);

            var tokenHandler = new JwtSecurityTokenHandler();
            string key = _config.GetSection("JwtKey").Value;
            var keyBytes = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}"),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
