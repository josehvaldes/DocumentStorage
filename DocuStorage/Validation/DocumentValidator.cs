namespace DocuStorage.Validation;

using DocuStorage.Models;
using FluentValidation;



public class DocumentValidator: AbstractValidator<DocumentRequest>
{

    public DocumentValidator() 
    {
        RuleFor(document => document.Name).NotEmpty().Length(1, 50);
        RuleFor(document => document.Category).NotEmpty().Length(1, 50);
        RuleFor(document => document.FormFile).NotEmpty();
    }
}

