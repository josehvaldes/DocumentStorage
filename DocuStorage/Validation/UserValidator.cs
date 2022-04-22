namespace DocuStorage.Validation;
using DocuStorage.Helpers;
using DocuStorage.Models;
using FluentValidation;

public class UserValidator: AbstractValidator<UserRequest>
{

    public UserValidator() 
    {
        RuleFor(user => (Roles)user.Role).IsInEnum();
        RuleFor(user => user.Username).NotEmpty();
        RuleFor(user => user.Password).NotEmpty();
    }
}
