using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_Bonus.Domain;
using Proiect_Bonus.Repository;
using Proiect_Bonus.UI;

namespace Proiect_Bonus
{
    internal class Run
    {
        static void Main(string[] args)
        {
            Interface UI = new Interface();
            UI.ShowMenu();
        }
    }
}
