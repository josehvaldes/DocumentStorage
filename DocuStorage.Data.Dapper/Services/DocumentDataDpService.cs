using DocuStorate.Common.Data.Model;
using DocuStorate.Common.Data.Services;

namespace DocuStorage.Data.Dapper.Services;

public class DocumentDataDpService : IDocumentDataService
{
    private ISqlDataProvider _dataProvider;
    private IDocumentContentService _documentContentService;

    public DocumentDataDpService(ISqlDataProvider dataProvider, IDocumentContentService documentContentService)
    {
        _dataProvider = dataProvider;
        _documentContentService = documentContentService;
    }

    public void AssignToGroup(int groupId, int[] documents)
    {

        using var con = _dataProvider.GetConnection();
        con.Open();

        int delRows = con.Execute("select * from assign_documents_to_group(@groupid, @docs)", 
            new { groupid = groupId, docs = documents });

    }

    public void AssignToUser(int userId, int[] documents)
    {
        using var con = _dataProvider.GetConnection();
        con.Open();

        int delRows = con.Execute("select * from assign_documents_to_user(@userId, @docs)",
            new { userId = userId, docs = documents });

    }

    public Document Create(Document document)
    {

        using var con = _dataProvider.GetConnection();
        con.Open();
        document.Created_On = DateTime.Now;
        var id = con.ExecuteScalar<int>("insert into documents(name, category, description, created_on) values (@name, @category, @description, @created_on) RETURNING id",
            document);

        if (id <= 0)
        {
            throw new Exception("Invalid Id returned for document content");
        }
        else 
        {
            document.Id = id;
            _documentContentService.SaveDocContent(document);
        }

        return document;
    }

    public void Delete(int did)
    {
        using var con = _dataProvider.GetConnection();
        con.Open();

        int delRows = con.Execute("delete from documents where id = @id", new { id = did });
        if (delRows <= 0)
        {
            throw new Exception("Document not found");
        }
        else 
        {
            _documentContentService.DeleteContent(did);
        }
    }

    public Document? Get(int did)
    {
        using var con = _dataProvider.GetConnection();
        con.Open();

        var document = con.Query<Document>("select * from get_document(@id)",
            new { id = did }).FirstOrDefault();

        if (document != null)
        {
            _documentContentService.GetDocContent(document);
        }

        return document;        
    }

    public List<Document> GetAll()
    {
        using var con = _dataProvider.GetConnection();
        con.Open();
        var documents = con.Query<Document>("select * from get_documents()").ToList();
        return documents;
    }

    public List<DocumentGroup> GetAllAvailableUser(int uid)
    {
        using var con = _dataProvider.GetConnection();
        con.Open();
        var documents = con.Query<DocumentGroup>("select * from get_all_docs_by_user_id(@userId)",
            new { userId = uid }).ToList();

        return documents;
    }

    public List<Document> GetByGroupId(int gid)
    {
        using var con = _dataProvider.GetConnection();
        con.Open();
        var documents = con.Query<Document>("select * from get_documents_by_group_id(@groupId)"
            , new { groupId = gid }).ToList();

        return documents;
    }

    public List<Document> GetByUserId(int uid)
    {
        using var con = _dataProvider.GetConnection();
        con.Open();
        var documents = con.Query<Document>("select * from get_documents_by_user_id(@userId)"
            , new { userId = uid }).ToList();

        return documents;
    }

    public List<DocumentGroup> GetInGroupsByUser(int uid)
    {
        using var con = _dataProvider.GetConnection();
        con.Open();
        var documents = con.Query<DocumentGroup>("select * from get_docs_in_groups_by_user_id(@userId)",
            new { userId = uid }).ToList();

        return documents;
    }
}

