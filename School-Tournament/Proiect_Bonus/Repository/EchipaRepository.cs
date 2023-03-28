using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_Bonus.Domain;

namespace Proiect_Bonus.Repository
{
    internal class EchipaRepository : AbstractRepository<Guid, Echipa>
    {
        public string filename { get; set; }

        public EchipaRepository(string filename) : base(filename)
        {
        }
        public override void LoadData()
        {
            entities = new List<Echipa>();
            string path = Directory.GetCurrentDirectory() + @"\Data\Echipe.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(';');
                    Echipa ech = new Echipa(line);
                    entities.Add(ech);
                }
            }
        }
    }
}
