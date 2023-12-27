namespace PrimeValLife.Core.Utilities;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text;

public static class Utility
{
    public static string MyConnectionString { get; private set; }

    #region Configuration Manager
    static class ConfigurationManager
    {
        public static IConfiguration AppSetting { get; }
        public static string MyConnectionString { get; set; }

        //static ConfigurationManager()
        //{
        //    AppSetting = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsetting.json")
        //        .Build();
        //}
    }
    #endregion

    #region Set Session Value
    public static void SetSessionValue(HttpContext httpContext, string Key, string Value)
    {
        if (httpContext != null && !string.IsNullOrEmpty(Key) && !string.IsNullOrEmpty(Value))
        {
            httpContext.Session.Set(Key, Encoding.ASCII.GetBytes(Value));
        }
    }
    #endregion

    public static void SetConnectionString(string Con)
    {
        MyConnectionString = Con;
    }

    public static string GetConnectionstring()
    {
        return MyConnectionString;
    }

    #region Get Session Value
    public static string GetSessionValue(HttpContext httpContext, string key)
    {
        string sessionValue = string.Empty;
        if (httpContext != null && !string.IsNullOrEmpty(key))
        {
            httpContext.Session.TryGetValue(key, out byte[] valueBytes);

            if (valueBytes != null)
            {
                sessionValue = Encoding.UTF8.GetString(valueBytes);
            }
        }
        return sessionValue;
    }
    #endregion

    #region Convert to Int
    public static int GetInt(Object Value)
    {
        int result = 0;
        if (Value != null)
        {
            int.TryParse(Value.ToString(), out result);
        }
        return result;
    }
    #endregion

    #region Convert to Int32
    public static Int32 GetInt32(Object Value)
    {
        int result = 0;
        if (Value != null)
        {
            Int32.TryParse(Value.ToString(), out result);
        }
        return result;
    }
    #endregion

    #region Convert to long
    public static long GetLong(Object Value)
    {
        long result = 0;
        if (Value != null)
        {
            long.TryParse(Value.ToString(), out result);
        }
        return result;
    }
    #endregion

    #region Convert to bool
    public static bool GetBool(Object Value)
    {
        bool result = false;
        if (Value != null)
        {
            bool.TryParse(Value.ToString(), out result);
        }
        return result;
    }
    #endregion

    #region Convert to Byte
    public static byte Getbyte(Object Value)
    {
        byte result = 0;
        if (Value != null)
        {
            byte.TryParse(Value.ToString(), out result);
        }
        return result;
    }
    #endregion

    #region Get App Setting
    public static string GetAppSetting(string Key)
    {
        var value = ConfigurationManager.AppSetting.GetSection("AppSettings:" + Key).Value;
        if (value != null)
        {
            return value;
        }
        return string.Empty;
    }
    #endregion
}