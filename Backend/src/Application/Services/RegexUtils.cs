using FluentValidation;
using Domain.Dtos.Commands.Authentification;
using Domain.Dtos.Commands.Items;
namespace Application.Services
{
    public sealed class EmailDtoValidator : AbstractValidator<string>, IValidator<string>
    { 
        public EmailDtoValidator() 
        { 
            this.RuleFor(email => email)
                .NotEmpty()
                .WithMessage("Email syntax no valide")
                .EmailAddress()
                .WithMessage("Email syntax no valide");
        }
    }

    public class IdDtoValidator : AbstractValidator<Guid>
    {
        public IdDtoValidator()
        {
            this.RuleFor(x => x).NotEmpty();
        }
    }

    public sealed class NameDtoValidator : AbstractValidator<string>, IValidator<string>
    {
        public NameDtoValidator()
        {
            this.RuleFor(name => name)
                .NotEmpty()
                .WithMessage("Name cannot be empty")
                .Matches(@"^[a-zA-Z]+$")
                .WithMessage("The name can only contain letters and without spaces");
        }
    }

    public sealed class PasswordValidator : AbstractValidator<string>, IValidator<string>
    {
        public PasswordValidator()
        {
            this.RuleFor(password => password)
                .NotEmpty()
                .WithMessage("Password cannot be empty")
                .MinimumLength(8)
                .WithMessage("Password must contain at least 8 characters")
                .Matches(@"[A-Z]")
                .WithMessage("Password must contain at least one capital letter")
                .Matches(@"[a-z]")
                .WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"[0-9]")
                .WithMessage("Password must contain at least one number")
                .Matches(@"[\W]")
                .WithMessage("Password must contain at least one special character");
        }
    }
    public sealed class PriceValidator : AbstractValidator<double>, IValidator<double>
    {
        public PriceValidator()
        {
            this.RuleFor(price => price)
            .GreaterThan(0).WithMessage("Price must be greater than 0")
            .LessThanOrEqualTo(10000).WithMessage("The price cannot exceed 10,000");
        }
    }

    public sealed class DescriptionDtoValidator : AbstractValidator<string>, IValidator<string>
    {
        public DescriptionDtoValidator()
        {
            this.RuleFor(description => description).MaximumLength(100)
                .WithMessage("Description cannot exceed 100 characters")
                .Must(this.NotContainScriptTags)
                .WithMessage("Description cannot contain script tags");
        }
        private bool NotContainScriptTags(string description)
        {
            return !description.Contains("<script>", StringComparison.OrdinalIgnoreCase);
        }
    }

    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>, IValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            this.RuleFor(em => em.Email).SetValidator(new EmailDtoValidator());
            this.RuleFor(ps => ps.Password).SetValidator(new PasswordValidator());
        }
    }
    public sealed class AddItemsCommandValidator : AbstractValidator<AddItemsCommand>, IValidator<AddItemsCommand>
    {
        public AddItemsCommandValidator()
        {
            this.RuleFor(pr => pr.Price).SetValidator(new PriceValidator());
            this.RuleFor(dr => dr.Description).SetValidator(new DescriptionDtoValidator());
            this.RuleFor(nm => nm.Name).SetValidator(new  NameDtoValidator());
        }
    }
}