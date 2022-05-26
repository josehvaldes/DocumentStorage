namespace DocuStorage.Common.Data.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IS3Cache
{
    bool Contains(string key);
    void Add(string key, byte[] content);
    void Remove(string key);
    byte[]? GetData(string key);
}

