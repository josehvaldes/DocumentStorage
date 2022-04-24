namespace DocuStorate.Common.Data.Services;

using DocuStorate.Common.Data.Model;

public interface IDocumentContentService
{
    void GetDocContent(Document document);
    void SaveDocContent(Document document);
    void DeleteContent(int documentId);
}

