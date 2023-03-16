using EFDataAccess;

namespace Implementation.FluentValidators.Comment
{
    public class UpdateCommentFluentValidator : CommentFluentValidator
    {
        private readonly int _id;

        [Obsolete]
        public UpdateCommentFluentValidator(DBContext context, int id) : base(context)
        {
            _id = id;
        }

        private bool ExistInDBComment(int userId) => _context.Users.Any(p => p.Id == userId && p.Id != _id);
    }
}
