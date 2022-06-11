using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;
using FluentValidation;

namespace BLL.Validation
{
    public class CommentValidator : AbstractValidator<CommentModel>
    {
        public CommentValidator()
        {
            RuleFor(cm => cm.Text).NotEmpty();
            RuleFor(cm => cm.PublicationDate).GreaterThan(new DateTime(2022, 01, 01));
            RuleFor(cm => cm.CommentState).IsInEnum();
        }
    }
}
