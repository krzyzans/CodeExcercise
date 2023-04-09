using CodeExcercise.Common.Models.Domain;
using FluentValidation;

namespace CodeExcercise.Common.Validators
{
    public class ModelValidator : AbstractValidator<Customer>
    {
        public ModelValidator()
        {
            RuleFor(r => r.Ident)
                .Empty().WithMessage("{PropertyName} cannot be provided.");
            RuleFor(r => r.Firstname)
                .NotEmpty().WithMessage("{PropertyName} has to be provided");
            RuleFor(r => r.Surname)
                .NotEmpty().WithMessage("{PropertyName} has to be provided");
        }
    }
}