using DocuStorage.Models;
using DocuStorage.Common.Data.Model;

namespace DocuStorage.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user);
        bool Update(User user);
        void Delete(int userId);
    }
}
