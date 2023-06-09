﻿using Application.DataTransfer;
using EFDataAccess;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Implementation.FluentValidators.User
{
    public class RegisterFluentValidator : AbstractValidator<UserDTO>
    {
        protected readonly DBContext _context;

        [Obsolete]
        public RegisterFluentValidator(DBContext context)
        {
            _context = context;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("Email address is required.")
                .MaximumLength(40)
                .WithMessage("Email address can't be longer than 40 characters.")
                .EmailAddress()
                .WithMessage("Invalid email address.")
                .Must(BeUniqueEmailInDatabase)
                .WithMessage("Email address already exists.");

            RuleFor(u => u.FirstName)
                .NotEmpty()
                .WithMessage("Firstname name is required.")
                .MinimumLength(3)
                .WithMessage("Firstname must be at least 3 characters long.")
                .MaximumLength(30)
                .WithMessage("Firstname can't be longer than 30 characters.")
                .Matches(new Regex("^[A-Z][a-z]{2,15}(\\s[A-Z][a-z]{2,15})*$"))
                .WithMessage("First name must start with capital letter.");

            RuleFor(u => u.LastName)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .MinimumLength(3)
                .WithMessage("Lastname must be at least 3 characters long.")
                .MaximumLength(30)
                .WithMessage("Lastname can't be longer than 30 characters.")
                .Matches(new Regex("^[A-Z][a-z]{2,15}(\\s[A-Z][a-z]{2,15})*$"))
                .WithMessage("Last name must start with capital letter.");

            RuleFor(u => u.Username)
                .NotEmpty()
                .WithMessage("Username is required.")
                .MinimumLength(5)
                .WithMessage("Username must be at least 5 characters long.")
                .MaximumLength(25)
                .WithMessage("Username can't be longer than 25 characters.")
                .Matches(new Regex("^[a-z][a-z0-9]{4,25}$"))
                .WithMessage("Username must start with lower letter and have lower letters and digits.")
                .Must(BeUniqueUsernameInDatabase)
                .WithMessage("Username address already exists.");

            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(7)
                .WithMessage("Password must be at least 7 characters long.")
                .Matches(new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{7,}$"))
                .WithMessage("Password is not valid, please enter at least one digits, lower and upper characters");
        }

        protected virtual bool BeUniqueEmailInDatabase(string email)
        {
            return !_context.Users.Any(u => u.Email == email);
        }

        protected virtual bool BeUniqueUsernameInDatabase(string username)
        {
            return !_context.Users.Any(u => u.Username == username);
        }
    }
}
