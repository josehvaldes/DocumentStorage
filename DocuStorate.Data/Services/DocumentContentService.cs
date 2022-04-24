namespace DocuStorate.Data.Services;

using DocuStorage.Common;
using DocuStorate.Common.Data.Model;
using DocuStorate.Common.Data.Services;
using Npgsql;


public class DocumentContentService : IDocumentContentService
{

    public void GetDocContent(Document document)
    {
        var query = "select * from get_document_content(@refId)";

        using var con = new NpgsqlConnection(Configuration.DatabaseContentConnection());
        con.Open();
        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("refId", document.Id);

        using var reader = cmd.ExecuteReader();
        while (reader.Read()) 
        {
            document.Content = (byte[])reader[0];
        }
    }

    public void SaveDocContent(Document document)
    {
        var query = "insert into document_contents(ref_id, content) values (@refId, @content) RETURNING id";

        using var con = new NpgsqlConnection(Configuration.DatabaseContentConnection());
        con.Open();

        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("refId", document.Id);
        cmd.Parameters.Add("content", NpgsqlTypes.NpgsqlDbType.Bytea, document.Content?.Length ?? 0).Value = document.Content;

        cmd.ExecuteScalar();
    }


    public void DeleteContent(int documentId)
    {
        using var con = new NpgsqlConnection(Configuration.DatabaseContentConnection());
        con.Open();
        var query = "delete from document_contents where ref_id = @id";
        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("id", documentId);
        var val = cmd.ExecuteNonQuery();
    }
}

