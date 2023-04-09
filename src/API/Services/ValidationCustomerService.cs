using System.Text;
using Abstraction.Services;
using CodeExcercise.Common.Exceptions;
using CodeExcercise.Common.Models.Domain;
using FluentValidation;
using FluentValidation.Results;

namespace CodeExcercise.Services;

/// <inheritdoc />
public class ValidationCustomerService : IValidationService<Customer>
{
    private readonly ILogger<ValidationCustomerService> logger;
    private readonly AbstractValidator<Customer> validator;

    /// <summary>
    /// Constructor
    /// </summary>
    public ValidationCustomerService(
        ILogger<ValidationCustomerService> logger, 
        AbstractValidator<Customer> validator)
    {
        this.logger = logger;
        this.validator = validator;
    }

    /// <inheritdoc />
    public async Task ValidateAndThrow(Customer validate, CancellationToken cancelationToken)
    {
        ValidationResult validationResult = await validator.ValidateAsync(validate, cancelationToken);

        if (!validationResult.IsValid)
        {
            StringBuilder message = new StringBuilder();

            foreach (var validationResultError in validationResult.Errors)
            {
                message.AppendLine(validationResultError.ErrorMessage);
            }

            ValidationErrorException validation = new ValidationErrorException(message.ToString());
        }
    }
}