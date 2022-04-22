namespace DocuStorate.Data.Model;

public class Document
{
    public int Id { get; set; }
    public DateTime CreateOn { get; set; }
    public string Name { get; set; }    
    public string Description { get; set; }
    public string Category { get; set; }
    public Byte[] Content { get; set; }    
}
