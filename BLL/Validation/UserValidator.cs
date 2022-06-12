using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;
using FluentValidation;

namespace BLL.Validation
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(um => um.Nickname)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20);

            RuleFor(um => um.BirthDate).InclusiveBetween(new DateTime(1900, 01, 01), DateTime.Now);
            RuleFor(um => um.Role).IsInEnum();
        }
    }
}
