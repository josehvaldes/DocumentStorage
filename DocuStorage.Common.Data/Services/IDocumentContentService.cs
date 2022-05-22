namespace DocuStorage.Common.Data.Services;

using DocuStorage.Common.Data.Model;

public interface IDocumentContentService
{
    void GetDocContent(Document document);
    Task SaveDocContent(Document document);
    Task DeleteContent(int documentId);
}

