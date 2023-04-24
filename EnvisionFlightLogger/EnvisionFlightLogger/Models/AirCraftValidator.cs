using EnvisionFlightLogger.DataAccess.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnvisionFlightLogger.Models
{
    public class AirCraftValidator : AbstractValidator<Aircraft>
    {
        public AirCraftValidator()
        {
            RuleFor(a => a.Make).NotEmpty().MaximumLength(128).WithMessage("Length of the Make should be greater than 0 and less than 128");
            RuleFor(a => a.Model).NotEmpty().MaximumLength(128).WithMessage("Length of the Model should be greater than 0 and less than 128");
            RuleFor(a => a.Registration).Matches(@"^[a-zA-Z]{1,2}\-[a-zA-Z0-9]{1,5}$").WithMessage("Registration format should be XX-XXXXX").NotEmpty();
            RuleFor(a => a.Location).NotEmpty().MaximumLength(255).WithMessage("Length of the Make should be greater than 0 and less than 255");
            RuleFor(a => a.DateAndTime).NotEmpty().LessThan(DateTime.Now).WithMessage("Date and time should be in the past");
        }
    }
}
