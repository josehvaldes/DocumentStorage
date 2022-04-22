namespace DocuStorage.Models;

public class DocumentResponse
{
    public int Id { get; set; }
    public DateTime CreateOn { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public Byte[] Content { get; set; }

    public string Checked { get; set; }
}

