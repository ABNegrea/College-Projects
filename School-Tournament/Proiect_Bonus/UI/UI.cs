using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_Bonus.Service;
using Proiect_Bonus.Repository;
using Proiect_Bonus.Domain;
using System.Globalization;

namespace Proiect_Bonus.UI
{
    internal class Interface
    {
        private JucatorService JucatorSrv;
        private EchipaService EchipaSrv;
        private MeciService MeciSrv;
        private JucatorActivService JucatorActivSrv;
        public Interface()
        {
            JucatorSrv = new JucatorService(new JucatorRepository("Jucatori.txt"));
            EchipaSrv = new EchipaService(new EchipaRepository("Echipe.txt"));
            MeciSrv = new MeciService(new MeciRepository("Meciuri.txt"));
            JucatorActivSrv = new JucatorActivService(new JucatorActivRepository("JucatoriActivi.txt"), new JucatorRepository("Jucatori.txt"));
        }

        public void ShowAllJucatoriFromEchipa(string echipa)
        {
            Console.WriteLine();
            int i = 0;
            foreach (Jucator jucator in JucatorSrv.GetJucatoriFromEchipa(echipa))
            {
                Console.WriteLine(i.ToString() + ". " + jucator.ToString());
                i++;
            }
        }

        public void ShowAllEchipe(List<Echipa> lst)
        {
            int i = 0;
            foreach (Echipa echipa in lst)
            {
                Console.WriteLine(i.ToString() + ". " + echipa.ToString());
                i++;
            }
        }

        public void ShowMeciuri(List<Meci> lst)
        {
            int i = 0;
            foreach (Meci meci in lst)
            {
                Console.WriteLine(i.ToString() + ". " + meci.ToString());
                i++;
            }
        }

        public void ShowJucatoriActivi(List<JucatorActiv> lst)
        {
            int i = 0;
            foreach (JucatorActiv juc in lst)
            {
                Console.WriteLine(i.ToString() + ". " + "Nume: " +
                    JucatorSrv.GetJucatorNume(juc.idJucator) + " | Puncte Inscrise: " +
                    juc.puncteInscrise.ToString() + " | Rol: " + juc.tip);
                i++;
            }
        }

        public void ShowScorMeci(int index)
        {
            Meci meci = MeciSrv.GetMeciByIndex(index);
            int scor1 = JucatorActivSrv.GetScoreEchipaMeci(meci.echipa1, meci);
            int scor2 = JucatorActivSrv.GetScoreEchipaMeci(meci.echipa2, meci);
            Console.WriteLine();
            Console.WriteLine(meci.echipa1 + " " + scor1.ToString() + " - " +
                scor2.ToString() + " " + meci.echipa2);
        }

        public void Menu()
        {
            Console.WriteLine();
            Console.WriteLine("Comenzi disponibile:");
            Console.WriteLine("1. Afiseaza toti jucatorii unei echipe.");
            Console.WriteLine("2. Afiseaza toti jucatorii activi a unei echipe dintr-un meci.");
            Console.WriteLine("3. Afiseaza toate meciurile dintr-o anumita perioada.");
            Console.WriteLine("4. Afiseaza scorul de la un anumit meci.");
            Console.WriteLine("0. Iesire.");
            Console.Write("Alege comanda dorita: ");
        }

        public void ShowMenu()
        {
            bool OK = true;
            int com;
            while (OK)
            {
                Menu();
                com = int.Parse(Console.ReadLine());
                if (com == 0)
                    OK = false;
                else if (com == 1)
                {
                    Console.WriteLine();
                    int index;
                    Console.WriteLine("Alege echipa (dupa index):");
                    ShowAllEchipe(EchipaSrv.GetEchipe());
                    Console.Write("Indexul dorit: ");
                    index = int.Parse(Console.ReadLine());
                    ShowAllJucatoriFromEchipa(EchipaSrv.GetEchipaByIndex(index).nume);
                }
                else if (com == 2)
                {
                    Console.WriteLine();
                    int index1, index2;
                    Console.WriteLine("Alege echipa (dupa index):");
                    ShowAllEchipe(EchipaSrv.GetEchipe());
                    Console.Write("Indexul dorit: ");
                    index1 = int.Parse(Console.ReadLine());

                    Console.WriteLine();
                    Console.WriteLine("Alege meciul (dupa index):");
                    ShowMeciuri(MeciSrv.GetMeciuri());
                    Console.Write("Indexul dorit: ");
                    index2 = int.Parse(Console.ReadLine());

                    Console.WriteLine();
                    ShowJucatoriActivi(JucatorActivSrv.GetJucatoriActivi(EchipaSrv.GetEchipaByIndex(index1), MeciSrv.GetMeciByIndex(index2)));
                }
                else if (com == 3)
                {
                    string date1, date2;
                    Console.WriteLine();
                    Console.Write("Prima data (format: dd/MM/yyyy): ");
                    date1 = Console.ReadLine();
                    Console.Write("A doua data (format: dd/MM/yyyy): ");
                    date2 = Console.ReadLine();
                    //string date1 = "13/05/2021";
                    //string date2 = "21/08/2022";
                    Console.WriteLine();
                    ShowMeciuri(MeciSrv.GetAllMeciuriByData(DateTime.ParseExact(date1, "d/M/yyyy", new CultureInfo("en-CA")), DateTime.ParseExact(date2, "d/M/yyyy", new CultureInfo("en-CA"))));
                }
                else if (com == 4)
                {
                    Console.WriteLine();
                    int index;
                    Console.WriteLine("Alege meciul (dupa index):");
                    ShowMeciuri(MeciSrv.GetMeciuri());
                    Console.Write("Indexul dorit: ");
                    index = int.Parse(Console.ReadLine());
                    ShowScorMeci(index);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Comanda invalida!");
                }
            }
        }
    }
}
