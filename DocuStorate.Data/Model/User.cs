namespace DocuStorate.Data.Model;
using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public int Role { get; set; }

    [JsonIgnore]
    public string Password { get; set; }
}

