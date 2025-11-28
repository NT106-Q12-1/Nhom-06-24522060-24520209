using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai06
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string tokenNhanDuoc = "";

            if (args.Length > 0)
            {
                tokenNhanDuoc = args[0];
            }
            Application.Run(new UserInfoForm(tokenNhanDuoc));
        }
    }
}
