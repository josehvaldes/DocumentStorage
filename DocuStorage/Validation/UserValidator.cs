namespace DocuStorage.Validation;
using DocuStorage.Helpers;
using DocuStorage.Models;
using FluentValidation;

public class UserValidator: AbstractValidator<UserRequest>
{

    public UserValidator() 
    {
        RuleFor(user => (Roles)user.Role).IsInEnum();
        RuleFor(user => user.Username).NotEmpty().Length(1, 50);
        RuleFor(user => user.Password).NotEmpty().Length(1, 50);
    }
}
