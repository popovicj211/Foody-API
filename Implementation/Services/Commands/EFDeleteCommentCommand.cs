using Application.Commands;
using Application.Exceptions;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Commands
{
    public class EFDeleteCommentCommand : BaseService, IDeleteCommentCommand
    {
        public EFDeleteCommentCommand(DBContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            var comment = _context.Comments.FirstOrDefault(u => u.Id == id);

            if (comment == null)
            {
                throw new EntityNotFoundException("Comment");
            }

            if (comment.IsDeleted == true)
            {
                throw new AlreadyExistException();
            }

            comment.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
