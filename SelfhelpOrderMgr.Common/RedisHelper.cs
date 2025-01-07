
using StackExchange.Redis;
using System;

public static class RedisHelper
{
    private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
    {
        // 假设你有一个配置字符串，例如 "localhost:6379"
        string configuration = "localhost:6379";
        return ConnectionMultiplexer.Connect(configuration);
    });

    public static ConnectionMultiplexer Connection { get { return lazyConnection.Value; } }

    public static IDatabase GetDatabase(int db = -1)
    {
        return Connection.GetDatabase(db);
    }

    public static void SetValue(int db, string key, string value)
    {
        GetDatabase(db).StringSet(key, value);
    }

    public static string GetValue(int db, string key)
    {
        return GetDatabase(db).StringGet(key);
    }

    //其他扩展方法

    public static void Set<T>(int db, string key, T value, TimeSpan? expiry = null)
    {
        GetDatabase(db).StringSet(key, Newtonsoft.Json.JsonConvert.SerializeObject(value), expiry);
    }

    public static T Get<T>(int db, string key)
    {
        var value = GetDatabase(db).StringGet(key);
        return value.IsNullOrEmpty ? default(T) : Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
    }

    public static bool KeyDelete(int db, string key)
    {
        return GetDatabase(db).KeyDelete(key);
    }

    public static long KeyDelete(int db, string[] keys)
    {
        var redisKeys = Array.ConvertAll(keys, key => (RedisKey)key);
        return GetDatabase(db).KeyDelete(redisKeys);
    }

    public static bool KeyExists(int db, string key)
    {
        return GetDatabase(db).KeyExists(key);
    }

    public static bool KeyExpire(int db, string key, TimeSpan? expiry = null)
    {
        return GetDatabase(db).KeyExpire(key, expiry);
    }
}


//使用方法

//// 设置db 0的值
//   RedisHelper.SetValue(0, "mykey", "myvalue");
//// 获取db 0的值
//string value = RedisHelper.GetValue(0, "mykey");
//Console.WriteLine(value);

//// 设置db 1的值
//RedisHelper.SetValue(1, "mykey", "myvalue in db 1");
//// 获取db 1的值
//string valueInDb1 = RedisHelper.GetValue(1, "mykey");
//Console.WriteLine(valueInDb1);



