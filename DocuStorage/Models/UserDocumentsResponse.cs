namespace DocuStorage.Models;

using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;

public class UserDocumentsResponse
{
    public int Userid { get; set; }
    public Document [] UserDocuments { get; set; }
    public Document[] GroupDocuments { get; set; }

}
