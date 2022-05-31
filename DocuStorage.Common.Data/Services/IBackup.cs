namespace DocuStorage.Common.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IBackup
{
    Task<bool> Backup(int documentId);

    Task<bool> Delete(int documentId);
}

