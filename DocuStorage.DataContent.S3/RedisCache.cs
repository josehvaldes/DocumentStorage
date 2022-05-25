namespace DocuStorage.DataContent.S3;

using DocuStorage.Common;
using StackExchange.Redis;
using System;

public class RedisCache : IS3Cache
{
    private readonly IDatabase _cache;
    private static readonly string _configuration = Configuration.RedisConfig();

    private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
    {
        return ConnectionMultiplexer.Connect(_configuration);
    });

    public static ConnectionMultiplexer Connection
    {
        get
        {
            return lazyConnection.Value;
        }
    }

    public RedisCache() 
    {
        this._cache = Connection.GetDatabase();
    }

    public void Add(string key, byte[] content)
    {
        string bitsString = Convert.ToBase64String(content);
        var result = this._cache.StringSet(key, bitsString);
        if (!result) 
        {
            throw new Exception("Redis Cache unexpected value");
        }
    }

    public bool Contains(string key)
    {
        return this._cache.KeyExists(key);
    }

    public byte[]? GetData(string key)
    {
        var value = this._cache.StringGet(key);
        var bytes = Convert.FromBase64String(value);
        return bytes;
    }

    public void Remove(string key)
    {
        var response = this._cache.KeyDelete(key);
    }
}

