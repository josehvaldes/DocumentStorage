using DocuStorate.Data.Model;
using DocuStorage.Models;

namespace DocuStorage.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user);
        User Update(User user);
        void Delete(int userId);
    }
}
