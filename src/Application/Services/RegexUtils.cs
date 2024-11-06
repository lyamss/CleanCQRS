using FluentValidation;
using Domain.Dtos.Commands.Authentification;
using Domain.Dtos.Commands.Items;
namespace Application.Services
{
    public interface IEmailDtoValidator : IValidator<string> { }
    internal sealed class EmailDtoValidator : AbstractValidator<string>, IEmailDtoValidator
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

    public interface IIdDtoValidator : IValidator<int> { }
    internal sealed class IdDtoValidator : AbstractValidator<int>, IIdDtoValidator
    {
        public IdDtoValidator()
        {
            this.RuleFor(id => id)
                .NotEmpty()
                .WithMessage("Id no valid")
                .GreaterThanOrEqualTo(0).WithMessage("Id must be positive");
        }
    }

    public interface INameDtoValidator : IValidator<string> { }
    internal sealed class NameDtoValidator : AbstractValidator<string>, INameDtoValidator
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

    public interface IPasswordValidator : IValidator<string> { }
    internal sealed class PasswordValidator : AbstractValidator<string>, IPasswordValidator
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
    public interface IPriceValidator : IValidator<double> { }
    internal sealed class PriceValidator : AbstractValidator<double>, IPriceValidator
    {
        public PriceValidator()
        {
            this.RuleFor(price => price)
            .GreaterThan(0).WithMessage("Price must be greater than 0")
            .LessThanOrEqualTo(10000).WithMessage("The price cannot exceed 10,000");
        }
    }

    public interface IDescriptionDtoValidator : IValidator<string> { }
    internal sealed class DescriptionDtoValidator : AbstractValidator<string>, IDescriptionDtoValidator
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

    public interface ICreateUserCommandValidator : IValidator<CreateUserCommand> { }
    internal sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>, ICreateUserCommandValidator
    {
        public CreateUserCommandValidator()
        {
            this.RuleFor(em => em.Email).SetValidator(new EmailDtoValidator());
            this.RuleFor(ps => ps.Password).SetValidator(new PasswordValidator());
        }
    }
    public interface IAddItemsCommandValidator : IValidator<AddItemsCommand> { }
    internal sealed class AddItemsCommandValidator : AbstractValidator<AddItemsCommand>, IAddItemsCommandValidator
    {
        public AddItemsCommandValidator()
        {
            this.RuleFor(pr => pr.Price).SetValidator(new PriceValidator());
            this.RuleFor(dr => dr.Description).SetValidator(new DescriptionDtoValidator());
            this.RuleFor(nm => nm.Name).SetValidator(new  NameDtoValidator());
        }
    }
}