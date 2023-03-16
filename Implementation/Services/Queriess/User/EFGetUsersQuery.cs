﻿using Application.DataTransfer;
using Application.Helpers;
using Application.Pagination;
using Application.Queries.User;
using Application.Searches;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Queriess.User
{
    public class EFGetUsersQuery : BaseService, IGetUsersQuery
    {
        private readonly IMapper _mapper;
        public EFGetUsersQuery(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public PagedResponse<UserDTO> Execute(BaseSearchRequest request)
        {
            var users = _context.Users.AsQueryable();

            return users.Where(d => !d.IsDeleted).ProjectTo<UserDTO>(_mapper.ConfigurationProvider).Select(user => new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                IsActived = user.IsActived,
                ImagePath = user.ImagePath,
                IsDeleted = user.IsDeleted,
                Role = user.Role
            }).Paginate(request.PerPage, request.Page);
        }
    }
}
