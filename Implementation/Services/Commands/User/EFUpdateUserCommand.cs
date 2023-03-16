using Application.Commands.User;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Commands.User
{
    public class EFUpdateUserCommand : BaseService, IUpdateUserCommand
    {
        private readonly IMapper _mapper;
        public EFUpdateUserCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public void Execute(UserDTO request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == request.Id);

            if (user == null)
            {
                throw new EntityNotFoundException("User");
            }

            if (request.Email != null)
            {
                if (!_context.Users.Any(u => u.Email == request.Email))
                {
                    user.Email = request.Email;
                }
                else
                {
                    throw new AlreadyExistException();
                }
            }

            if (request.FirstName != null)
            {
                if (user.FirstName != request.FirstName)
                {
                    user.FirstName = request.FirstName;
                }
                else
                {
                    throw new AlreadyExistException();
                }
            }

            if (request.LastName != null)
            {
                if (user.LastName != request.LastName)
                {
                    user.LastName = request.LastName;
                }
                else
                {
                    throw new AlreadyExistException();
                }
            }

            if (request.Username != null)
            {
                if (user.Username != request.Username)
                {
                    user.Username = request.Username;
                }
                else
                {
                    throw new AlreadyExistException();
                }

            }

            if (request.Password != null)
            {
                if (user.Password != request.Password)
                {
                    user.Password = request.Password;
                }
                else
                {
                    throw new AlreadyExistException();
                }

            }

            if (request.RoleId != 0)
            {
                if (user.RoleId != request.RoleId)
                {
                    if (_context.Roles.Any(r => r.Id == request.RoleId))
                    {
                        user.RoleId = request.RoleId;
                    }
                    else
                    {
                        throw new EntityNotFoundException("Role");
                    }
                }
                else
                {
                    throw new AlreadyExistException();
                }
            }

            _context.SaveChanges();
        }
    }
}
