using DocuStorage.Models;
using FluentValidation;

namespace DocuStorage.Validation
{
    public class AuthenticateValidator : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticateValidator() 
        {
            RuleFor(user => user.Username).NotEmpty().Length(1, 50);

            //TODO add more validation for Strong Passwords
            RuleFor(user => user.Password).NotEmpty().Length(2, 50);
        }

    }
}
