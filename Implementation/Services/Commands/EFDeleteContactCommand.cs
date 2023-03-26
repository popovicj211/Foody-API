using Application.Commands;
using Application.Exceptions;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Commands
{
    public class EFDeleteContactCommand : BaseService, IDeleteContactCommand
    {
        public EFDeleteContactCommand(DBContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(u => u.Id == id);

            if (contact == null)
            {
                throw new EntityNotFoundException("Contact");
            }

            if (contact.IsDeleted == true)
            {
                throw new AlreadyExistException();
            }

            contact.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
