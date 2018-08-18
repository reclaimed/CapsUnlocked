using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapsUnlock
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            // Show the system tray icon.
            using (TrayIconController pi = new TrayIconController())
            {
                pi.Display();

                // Make sure the application runs!
                Application.Run();
            }


        }
    }
}
