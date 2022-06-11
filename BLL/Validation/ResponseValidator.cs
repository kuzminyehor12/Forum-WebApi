using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;
using FluentValidation;

namespace BLL.Validation
{
    public class ResponseValidator : AbstractValidator<ResponseModel>
    {
        public ResponseValidator()
        {
            RuleFor(rm => rm.Text).NotEmpty();
            RuleFor(rm => rm.ResponseState).IsInEnum();
            RuleFor(rm => rm.PublicationDate).GreaterThan(new DateTime(2022, 01, 01));
        }
    }
}
