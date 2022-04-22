namespace DocuStorage.Models;

using DocuStorate.Data.Model;

public class UserDocumentsResponse
{
    public int Userid { get; set; }
    public Document [] UserDocuments { get; set; }
    public Document[] GroupDocuments { get; set; }

}
