using FluentValidation;

namespace Application.Customers.Create;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(r => r.LastName)
            .NotEmpty()
            .MaximumLength(50)
            .WithName("Last Name");

        RuleFor(r => r.Email)
            .EmailAddress()
            .MaximumLength(255);

        RuleFor(r => r.PhoneNumber)
            .NotEmpty()
            .MaximumLength(20)
            .WithName("Phone Number");

        RuleFor(r => r.Country)
            .NotEmpty()
            .MaximumLength(3)
            .WithName("Address Country");

        RuleFor(r => r.Country)
            .NotEmpty()
            .MaximumLength(200)
            .WithName("Address Line 1");
    }
}