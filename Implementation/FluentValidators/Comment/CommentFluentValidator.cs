using Application.DataTransfer;
using EFDataAccess;
using FluentValidation;

namespace Implementation.FluentValidators.Comment
{
    public class CommentFluentValidator : AbstractValidator<CommentDTO>
    {
        protected readonly DBContext _context;

        [Obsolete]
        public CommentFluentValidator(DBContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(u => u.Content)
                .NotEmpty()
                .WithMessage("Message is required.")
                .MinimumLength(3)
                .WithMessage("Message must be at least 3 characters long.");

            RuleFor(g => g.UserId)
                .NotEmpty()
                .WithMessage("User is required.")
                .Must(ExistInDBComment)
                .WithMessage("User doesn't exist.");
        }

        private bool ExistInDBComment(int userId) => _context.Users.Any(p => p.Id == userId);
    }
}
