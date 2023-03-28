using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Problema5
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Uri configUri = new Uri("D:\\Folder Facultate\\Anul 2 Semestrul 2\\Medii de Proiectate si Programare\\Proiecte-MPP\\Proiect-MPP-C#\\mpp-proiect-csharp-ABNegrea\\Problema5\\log4net.config");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}
