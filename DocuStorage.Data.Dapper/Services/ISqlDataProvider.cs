namespace DocuStorage.Data.Dapper.Services;

public interface ISqlDataProvider<T>
{
    public ISqlDapperWrapper GetConnection();

}
