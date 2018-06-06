using System.Text;

namespace Viewer
{
    public static class Extensions
    {
        public static string GetString(this byte[] bytes) => Encoding.UTF8.GetString(bytes);
    }
}