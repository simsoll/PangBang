#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
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
            var levelConfiguration = new LevelConfiguration(new Vector2(0, 20),  5.0f, Color.Black, 80.0f, 30.0f, 10.0f, Color.Black);

            using (var game = new PangBang(screenConfiguration, levelConfiguration))
                game.Run();
        }
    }
#endif
}