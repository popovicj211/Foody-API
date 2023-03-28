using Application.DataTransfer;
using Application.Exceptions;
using Application.Interfaces;
using Application.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFDataAccess;
using Implementation.EFServices;
using Implementation.Services.Exstensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Implementation.Services.Queriess
{
    public class EFLoginUserQuery : BaseService, ILoginUserQuery
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IPasswordHashing _hasher;

        public EFLoginUserQuery(DBContext context, IMapper mapper, IConfiguration config, IPasswordHashing hasher) : base(context)
        {
            this._mapper = mapper;
            this._config = config;
            this._hasher = hasher;
        }

        public string Execute(LoginDTO search)
        {
            // var user = _context.Users.Where(u => !u.IsDeleted && u.IsActived && u.Email == search.Email).Include(r => r.Role).ProjectTo<UserDTO>(this._mapper.ConfigurationProvider).FirstOrDefault();
            var user = _context.Users.Where(u => !u.IsDeleted && u.IsActived && u.Email == search.Email).Include(r => r.Role).FirstOrDefault();

            if (user == null)
            {
                throw new EntityNotFoundException("User");
            }

            if (!this._hasher.ValidatePassword(search.Password, user.Password))
            {
                throw new PasswordNotValidException();
            }

            var getJwtToken = this._config.GetJwtToken(user);
            return getJwtToken;
        }
    }
}
