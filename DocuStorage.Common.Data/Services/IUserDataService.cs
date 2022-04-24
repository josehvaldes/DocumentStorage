namespace DocuStorate.Common.Data.Services;

using DocuStorate.Common.Data.Model;


public interface IUserDataService
{
    User Update(User user);
    User Create(User user);
    User? Get(int id);
    User? Get(User userparams);
    List<User> GetAll();
    void Delete(int userId);
}

