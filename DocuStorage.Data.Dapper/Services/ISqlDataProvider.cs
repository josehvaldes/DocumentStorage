namespace DocuStorage.Data.Dapper.Services;

public interface ISqlDataProvider
{
    public ISqlDapperWrapper GetConnection();

    public ISqlDapperWrapper GetDocumentContentConnection();

}
