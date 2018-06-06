using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class Util
    {
        public static string ReadLineWithMessage(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
    }
}
