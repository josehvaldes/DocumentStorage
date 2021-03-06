namespace DocuStorage.Data.Dapper.Services;

using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;
using System;

public class DocumentContentDpService : IDocumentContentService
{
    private ISqlDataProvider _dataProvider;

    public DocumentContentDpService(ISqlDataProvider dataProvider) 
    {
        _dataProvider = dataProvider;
    }

    public Task DeleteContent(int documentId)
    {
        using var con = _dataProvider.GetDocumentContentConnection();
        con.Open();

        int delRows = con.Execute("delete from document_contents where ref_id = @id", new { id = documentId });
        if (delRows <= 0)
        {
            throw new Exception("Row not found");
        }

        return Task.CompletedTask;
    }

    public void GetDocContent(Document document)
    {
        using var con = _dataProvider.GetDocumentContentConnection();
        con.Open();

        var result = con.Query("select content from get_document_content(@refId)",
            new { refId = document.Id }).FirstOrDefault();

        document.Content = result?.content;
    }

    public Task SaveDocContent(Document document)
    {
        using var con = _dataProvider.GetDocumentContentConnection();
        con.Open();

        var id = con.ExecuteScalar<int>("insert into document_contents(ref_id, content) values (@refId, @content) RETURNING id", 
            new { refId = document.Id, content = document.Content });

        if (id <= 0)
        {
            throw new Exception("Invalid Id returned for document content");
        }

        return Task.CompletedTask;
    }
}
