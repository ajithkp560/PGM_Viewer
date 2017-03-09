using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PgmViewer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        /*static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }*/
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 frm = new Form1();
            if (args.Length == 1) //make sure an argument is passed
            {
                FileInfo file = new FileInfo(args[0]);
                if (file.Exists) //make sure it's actually a file
                {
                    frm.SetPicture(args[0]);
                }
            }
            Application.Run(frm);
        }
    }
}
