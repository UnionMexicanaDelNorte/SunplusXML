using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liga
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(args.Length==8)
            {
                Application.Run(new Form1(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]));
            }
            else
            {
                Application.Run(new Form1(args));
            }
           
        }
    }
}
