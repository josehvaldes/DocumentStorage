using DocuStorate.Data.Model;

namespace DocuStorate.Data.Services
{
    public interface IUserDataService
    {
        User Update(User user);
        User Create(User user);
        User? Get(int id);
        User? Get(User userparams);
        List<User> GetAll();
        void Delete(int userId);
    }
}
