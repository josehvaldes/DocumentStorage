namespace DocuStorage.Common.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IBackup
{
    bool Backup(int documentId);    

}

