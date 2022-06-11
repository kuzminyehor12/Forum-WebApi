using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;
using FluentValidation;

namespace BLL.Validation
{
    public class TopicValidator : AbstractValidator<TopicModel>
    {
        public TopicValidator()
        {
            RuleFor(tm => tm.Description).NotEmpty();
            RuleFor(tm => tm.PublicationDate).GreaterThan(new DateTime(2022, 01, 01));
            RuleFor(tm => tm.TopicState).IsInEnum();
        }
    }
}
