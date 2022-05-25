namespace DocuStorage.Tests.Mockups;

using DocuStorage.DataContent.S3;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[TestFixture]
public class RedisCacheIntegrationTests
{
    private RedisCache _cache;

    [SetUp]
    public void Setup() 
    {
        _cache = new RedisCache();
    }

    [Test]
    public void Add_NoExceptions() 
    {
        try 
        {
            var value = "Hello World";
            var content = Encoding.UTF8.GetBytes(value);
            _cache.Add("add", content);
        }
        catch(Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public void GetData_NoExceptions() 
    {
        try
        {
            var key = "get";
            var value = "Hello World";
            
            var content = Encoding.UTF8.GetBytes(value);
            _cache.Add(key, content);

            var data = _cache.GetData(key);

            string bitsString = Encoding.UTF8.GetString(data??new byte[0]);
            Assert.AreEqual(bitsString, value);
        }
        catch (Exception ex) 
        {
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public void Contains_isTrue() 
    {
        try
        {
            var key = "contains";
            _cache.Add(key, new byte[0]);

            var value = _cache.Contains(key);
            Assert.AreEqual(true, value);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public void Contains_isFalse_NoExceptions()
    {
        try
        {
            var key = "Random";

            var value = _cache.Contains(key);
            Assert.AreEqual(false, value);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [Test]
    public void Remove_NoExceptions() 
    {
        try
        {
            var cache = new RedisCache();
            var key = "add";
            cache.Remove(key);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
}

