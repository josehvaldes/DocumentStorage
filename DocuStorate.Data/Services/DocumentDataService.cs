namespace DocuStorate.Data.Services;

using DocuStorage.Common;
using DocuStorate.Data.Model;
using Npgsql;

/// <summary>
/// 
/// </summary>
public class DocumentDataService : IDocumentDataService
{
    public void AssignToGroup(int groupId, int[] documents)
    {
        var query = "select * from assign_documents_to_group(@groupid, @docs)";
        using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
        con.Open();

        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("groupid", groupId);
        cmd.Parameters.Add("docs", NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer).Value = documents;
        var val = cmd.ExecuteNonQuery();
    }

    public void AssignToUser(int userId, int[] documents)
    {
        var query = "select * from assign_documents_to_user(@userId, @docs)";
        using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
        con.Open();

        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("userId", userId);
        cmd.Parameters.Add("docs", NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer).Value = documents;

        var val = cmd.ExecuteNonQuery();
    }

    public Document Create(Document document)
    {
        var query = "insert into documents(name, category, description, created_on, content) values (@name, @category, @description, @createdOn, @content) RETURNING id";

        using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
        con.Open();

        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("name", document.Name);
        cmd.Parameters.AddWithValue("category", (object)document.Category??DBNull.Value);
        cmd.Parameters.AddWithValue("description", (object) document.Description??DBNull.Value);
        cmd.Parameters.AddWithValue("createdOn", DateTime.Now);
           
        cmd.Parameters.Add("content", NpgsqlTypes.NpgsqlDbType.Bytea, document.Content?.Length??0).Value = document.Content;

        var val = cmd.ExecuteScalar();
        document.Id = Int32.Parse(val?.ToString() ?? "0");
            
        return document;
    }

    public Document? Get(int id)
    {
        var query = "select * from get_document(@id)";

        using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
        con.Open();
        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("id", id);

        using var reader = cmd.ExecuteReader();
        Document? document= null;
        while (reader.Read())
        {
            document = new Document()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Category = reader.IsDBNull(2) ? "" : reader.GetString(2),
                Description = reader.IsDBNull(3) ? "" : reader.GetString(3),
                CreateOn = reader.GetDateTime(4),
            };

            document.Content = (byte[])reader[5];
        }

        return document;
    }

    public List<Document> GetAll()
    {
        List<Document> result = new List<Document>();
        var query = "select * from get_documents()";

        using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
        con.Open();
        using var cmd = new NpgsqlCommand(query, con);
        using var reader = cmd.ExecuteReader();
            
        while (reader.Read())
        {
            var document = new Document()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Category = reader.IsDBNull(2) ? "" : reader.GetString(2),
                Description = reader.IsDBNull(3) ? "" : reader.GetString(3),
                CreateOn = reader.GetDateTime(4),
            };

            result.Add(document);
        }

        return result;
    }

    public List<Document> GetByGroupId(int groupId)
    {
        var list = new List<Document>();
        var query = "select * from get_documents_by_group_id(@groupId)";
        using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
        con.Open();
        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("groupId", groupId);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var document = new Document()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Category = reader.IsDBNull(2) ? "" : reader.GetString(2),
                Description = reader.IsDBNull(3) ? "" : reader.GetString(3),
                CreateOn = reader.GetDateTime(4),
            };

            list.Add(document);
        }

        return list;
    }

    public List<Document> GetByUserId(int userId)
    {
        var list = new List<Document>();
        var query = "select * from get_documents_by_user_id(@userId)";

        using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
        con.Open();
        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("userId", userId);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var document = new Document()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Category = reader.IsDBNull(2) ? "" : reader.GetString(2),
                Description = reader.IsDBNull(3) ? "" : reader.GetString(3),
                CreateOn = reader.GetDateTime(4),
            };


            list.Add(document);
        }

        return list;
    }

    public List<DocumentGroup> GetInGroupsByUser(int userId)
    {
        var list = new List<DocumentGroup>();
        var query = "select * from get_docs_in_groups_by_user_id(@userId)";
        using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
        con.Open();
        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("userId", userId);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var document = new DocumentGroup()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Category = reader.IsDBNull(2) ? "" : reader.GetString(2),
                Description = reader.IsDBNull(3) ? "" : reader.GetString(3),
                CreateOn = reader.GetDateTime(4),
                Source = reader.IsDBNull(5) ? "" : reader.GetString(5),
                SourceType = "GROUP"
            };
            list.Add(document);
        }
        return list;
    }

    public List<DocumentGroup> GetAllAvailableUser(int userId)
    {
        var list = new List<DocumentGroup>();
        var query = "select * from get_all_docs_by_user_id(@userId)";
        using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
        con.Open();
        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("userId", userId);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var document = new DocumentGroup()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Category = reader.IsDBNull(2) ? "" : reader.GetString(2),
                Description = reader.IsDBNull(3) ? "" : reader.GetString(3),
                CreateOn = reader.GetDateTime(4),
                Source = reader.IsDBNull(5) ? "" : reader.GetString(5),
                SourceType = reader.IsDBNull(6) ? "" : reader.GetString(6)

            };
            list.Add(document);
        }
        return list;
    }

    public void Delete(int id)
    {
        using var con = new NpgsqlConnection(Configuration.DatabaseConnection());
        con.Open();
        var query = "delete from documents where id = @id";
        using var cmd = new NpgsqlCommand(query, con);
        cmd.Parameters.AddWithValue("id", id);
        var val = cmd.ExecuteNonQuery();
    }
}

