using System;
using System.Windows.Forms;

namespace Enjoying.Logging.WindowsApp
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

            Enjoying.Logging.Desktop.Application.UseEnjoyingLogging();

            Application.Run(new LoggingExampleForm());
        }       
    }   
}
