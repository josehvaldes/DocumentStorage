namespace DocuStorage.Models;

using System.ComponentModel.DataAnnotations;

public class UserRequest
{
    public int Id { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public int Role { get; set; }
}
