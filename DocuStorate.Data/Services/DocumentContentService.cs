namespace DocuStorage.Data.Services;

using DocuStorage.Common;
using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;
using Microsoft.Extensions.Configuration;
using Npgsql;


public class DocumentContentService : IDocumentContentService
{
    private readonly IConfiguration _configuration;

    public DocumentContentService(IConfiguration configuration) 
    {
        _configuration = configuration;
    }

    public void GetDocContent(Document document)
    {
        var query = "select * from get_document_content(@refId)";

        using var con = new NpgsqlConnection(_configuration.DatabaseContentConnection());
        con.Open();
        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("refId", document.Id);

        using var reader = cmd.ExecuteReader();
        while (reader.Read()) 
        {
            document.Content = (byte[])reader[0];
        }

    }

    public Task SaveDocContent(Document document)
    {
        var query = "insert into document_contents(ref_id, content) values (@refId, @content) RETURNING id";

        using var con = new NpgsqlConnection(_configuration.DatabaseContentConnection());
        con.Open();

        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("refId", document.Id);
        cmd.Parameters.Add("content", NpgsqlTypes.NpgsqlDbType.Bytea, document.Content?.Length ?? 0).Value = document.Content;

        cmd.ExecuteScalar();

        return Task.CompletedTask;
    }


    public Task DeleteContent(int documentId)
    {
        using var con = new NpgsqlConnection(_configuration.DatabaseContentConnection());
        con.Open();
        var query = "delete from document_contents where ref_id = @id";
        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("id", documentId);
        var val = cmd.ExecuteNonQuery();

        return Task.CompletedTask;
    }
}

