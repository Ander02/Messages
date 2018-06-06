using System.Text;

namespace Sender
{
    public static class Extensions
    {
        public static byte[] GetBytes(this string text) => Encoding.UTF8.GetBytes(text);
    }
}