using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries.User;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFDataAccess;
using Implementation.EFServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Queriess.User
{
    public class EFGetUserQuery : BaseService, IGetUserQuery
    {
        private readonly IMapper _mapper;
        public EFGetUserQuery(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public UserDTO Execute(int id)
        {
            var user = _context.Users.Where(u => !u.IsDeleted && u.IsActived).Include(r => r.Role).ProjectTo<UserDTO>(_mapper.ConfigurationProvider).Select(u => new UserDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                IsActived = u.IsActived,
                ImagePath = u.ImagePath,
                IsDeleted = u.IsDeleted,
                Role = u.Role
            }).FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException("User");
            }

            return user;
        }
    }
}
