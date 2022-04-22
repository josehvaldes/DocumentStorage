namespace DocuStorage.Validation;

using DocuStorage.Models;
using FluentValidation;



public class DocumentValidator: AbstractValidator<DocumentRequest>
{

    public DocumentValidator() 
    {
        RuleFor(document => document.Name).NotEmpty();
        RuleFor(document => document.Category).NotEmpty();
        RuleFor(document => document.FormFile).NotEmpty();
    }
}

