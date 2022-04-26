namespace DocuStorate.Common.Data.Model;

public class DocumentGroup
{
    public int Id { get; set; }
    public DateTime CreateOn { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Source { get; set; }
    public string Source_Type { get; set; }
}
