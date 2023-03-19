using Application.DataTransfer;
using Application.Exceptions;
using Application.Interfaces;
using Application.Queries.Auth;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFDataAccess;
using Implementation.EFServices;
using Implementation.Services.Exstensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
            var user =  _context.Users.Where(u => !u.IsDeleted && u.IsActived && u.Email == search.Email).Include(r => r.Role).ProjectTo<UserDTO>(_mapper.ConfigurationProvider).FirstOrDefault();

            if (user == null)
            {
                throw new EntityNotFoundException("User");
            }

            if (!_hasher.ValidatePassword(search.Password, user.Password))
            {
                throw new PasswordNotValidException();
            }

            var getJwtToken = _config.GetJwtToken(user);
            return getJwtToken;
        }
    }
}
