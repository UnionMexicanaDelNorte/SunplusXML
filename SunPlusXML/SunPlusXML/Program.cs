using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
namespace SunPlusXML
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
          
            String thisprocessname = Process.GetCurrentProcess().ProcessName;

            if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 1)
            {
                if (args.Length > 0)//quiere entrar en modo ligero, pero ya hay otra instancia
                {
                    return; 
                }
                else
                {
                    //quiere entrar en modo pesado, debemos de aniquilar cualquier otra instancia
                    var processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
                    int cuantos = processes.Length;
                    int con = 0;
                    foreach (var process in processes)
                    {
                        con++;
                        if(con!=cuantos)
                        {
                            process.CloseMainWindow();
                            process.Kill();
                        }
                    }
                }
                

            }
                
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            if (args.Length >0)
            {
                Application.Run(new Form1(true));
            }
            else
            {
                Application.Run(new Form1(false));
            }
           
        
        }
    }
}
