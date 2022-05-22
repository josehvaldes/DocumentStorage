namespace DocuStorage.Data.Dapper.Services;

using DocuStorage.Common;
using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;
using global::Dapper;
using Npgsql;

public class GroupDataDpService : IGroupDataService
{
    private ISqlDataProvider _dataProvider;

    public GroupDataDpService(ISqlDataProvider dataProvider) 
    {
        _dataProvider = dataProvider;
    }

    public void AssignToUser(int userId, int[] groups)
    {
        using var con = _dataProvider.GetConnection();
        con.Open();
        var rows = con.Execute("select * from assign_groups_to_user(@userId, @groups)", 
            new { userId = userId, groups = groups });
    }

    public Group Create(Group group)
    {
        using var con = _dataProvider.GetConnection();
        con.Open();

        var id = con.ExecuteScalar<int>("insert into groups(name) values (@name) RETURNING id", group);
        if (id > 0)
        {
            group.Id = id;
        }
        else
        {
            throw new NpgsqlException("Invalid ID returned");
        }

        return group;
    }

    public void Delete(int gid)
    {
        using var con = _dataProvider.GetConnection();
        con.Open();

        int delRows = con.Execute("delete from groups where id = @id", new { id = gid });
        if (delRows <= 0)
        {
            throw new Exception("Row not found");
        }
    }

    public List<Group> GetAll()
    {
        using var con = _dataProvider.GetConnection();
        con.Open();

        var list = con.Query<Group>("select * from get_groups()").ToList();
        return list;
    }

    public List<Group> GetByUser(int userId)
    {
        using var con = _dataProvider.GetConnection();
        con.Open();

        var list = con.Query<Group>("select * from get_groups_by_user(@userId)",
            new { userId = userId }).ToList();
        return list;

    }
}

