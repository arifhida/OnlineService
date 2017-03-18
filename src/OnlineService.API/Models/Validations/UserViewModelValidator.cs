using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineService.API.Models.Validations
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full Name cannot be empty");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(User => User.Email).NotEmpty().WithMessage("Email cannot be empty")
                .EmailAddress().WithMessage("Valid email address required");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Pasword cannot be empty").Length(8, 20)
                .WithMessage("password minimum 8 character and maximum 20 character");
            RuleFor(x => x.MobileNo).NotEmpty().WithMessage("Mobile No. cannot be empty");
        }
    }
}
