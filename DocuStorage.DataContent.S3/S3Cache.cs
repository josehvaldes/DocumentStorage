namespace DocuStorage.DataContent.S3;

using System.Collections.Generic;

public class S3Cache : IS3Cache
{
    private Dictionary<string, byte[]> _cache;

    public S3Cache() 
    {
        _cache = new Dictionary<string, byte[]>();
    }

    public bool Contains(string key) 
    {
        return _cache.ContainsKey(key);
    }

    public void Add(string key, byte[] content) 
    {
        _cache.Add(key, content);
    }

    public void Remove(string key) 
    {
        _cache.Remove(key);
    }

    public byte[]? GetData(string key) 
    {
        if (_cache.ContainsKey(key))
        {
            return _cache[key];
        }
        else 
        {
            return null;
        }
    }
}

