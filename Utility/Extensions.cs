using Newtonsoft.Json;
using System.Text;

namespace Utility
{
    public static class Extensions
    {
        public static string GetString(this byte[] bytes) => Encoding.UTF8.GetString(bytes);
        public static byte[] GetBytes(this string text) => Encoding.UTF8.GetBytes(text);
        public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);
    }
}