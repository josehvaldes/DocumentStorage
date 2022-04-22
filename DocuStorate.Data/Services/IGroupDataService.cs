using DocuStorate.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuStorate.Data.Services
{
    public interface IGroupDataService
    {
        Group Create(Group request);

        List<Group> GetAll();
        List<Group> GetByUser(int userId);
        void AssignToUser(int userId, int[] groups);
        void Delete(int id);
    }
}
