using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_Bonus.Repository;
using Proiect_Bonus.Domain;

namespace Proiect_Bonus.Service
{
    internal class EchipaService
    {
        private EchipaRepository echipaRepository;

        public EchipaService(EchipaRepository repo)
        {
            echipaRepository = repo;
        }

        public List<Echipa> GetEchipe()
        {
            return echipaRepository.findAll();
        }

        public Echipa GetEchipaByIndex(int index)
        {
            int i = 0;
            foreach (Echipa echipa in GetEchipe())
            {
                if (i == index)
                    return echipa;
                i++;
            }
            return null;
        }
    }
}
