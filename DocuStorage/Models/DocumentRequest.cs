namespace DocuStorage.Models;

using System.ComponentModel.DataAnnotations;

public class DocumentRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Category { get; set;  }
    public string Description { get; set; }

    [Required]
    public IFormFile FormFile { get; set; }
}

