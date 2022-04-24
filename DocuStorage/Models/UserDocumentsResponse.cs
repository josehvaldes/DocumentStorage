namespace DocuStorage.Models;

using DocuStorate.Common.Data.Model;
using DocuStorate.Common.Data.Services;

public class UserDocumentsResponse
{
    public int Userid { get; set; }
    public Document [] UserDocuments { get; set; }
    public Document[] GroupDocuments { get; set; }

}
