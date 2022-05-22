namespace DocuStorage.Common.Data.Services;

using DocuStorage.Common.Data.Model;


public interface IUserDataService
{
    bool Update(User user);
    User Create(User user);
    User? Get(int id);
    User? Get(User userparams);
    List<User> GetAll();
    void Delete(int userId);
}

