namespace DocuStorage.Validation;

using DocuStorage.Models;
using FluentValidation;



public class GroupValidator: AbstractValidator<GroupRequest>
{
    public GroupValidator() 
    {
        RuleFor(group => group.Name).NotEmpty();
    }
}
