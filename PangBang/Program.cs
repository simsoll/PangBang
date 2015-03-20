#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using PangBang.Configuration;

#endregion

namespace PangBang
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var screenConfiguration = new ScreenConfiguration(800, 480);

            using (var game = new PangBang(screenConfiguration))
                game.Run();
        }
    }
#endif
}
