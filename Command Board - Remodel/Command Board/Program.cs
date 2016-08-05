using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Command_Board
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
            Form1 f= new Form1();

            if(f.IsDisposed == false)
                Application.Run(f);
        }
    }
}
