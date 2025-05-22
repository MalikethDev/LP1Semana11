using System;
using System.Collections.Generic;

namespace PlayerManagerMVC
{
    /// <summary>
    /// The player listing program.
    /// </summary>
    public class Program
    {
        private static void Main(string[] args)
        {
            var controller = new PlayerController();
            controller.Run();
        }
    }
}