using Application.Commands;
using Application.Exceptions;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Commands
{
    public class EFDeleteUserCommand : BaseService, IDeleteUserCommand
    {
        public EFDeleteUserCommand(DBContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException("User");
            }

            if (user.IsDeleted == true)
            {
                throw new AlreadyExistException();
            }

            user.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
