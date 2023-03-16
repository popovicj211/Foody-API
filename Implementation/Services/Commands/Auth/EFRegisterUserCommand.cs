using Application.DataTransfer;
using Application.Queries.Auth;
using AutoMapper;
using EFDataAccess;
using Implementation.EFServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Commands.Auth
{
    public class EFRegisterUserCommand : BaseService, IRegisterUserQuery
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public EFRegisterUserCommand(DBContext context, IMapper mapper, IConfiguration config) : base(context)
        {
            _mapper = mapper;
            _config = config;
        }

        public string Execute(UserDTO search)
        {
            throw new NotImplementedException();
        }
    }
}
