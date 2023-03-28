using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_Bonus.Repository;
using Proiect_Bonus.Domain;

namespace Proiect_Bonus.Service
{
    internal class MeciService
    {
        private MeciRepository meciRepository;

        public MeciService(MeciRepository repo)
        {
            meciRepository = repo;
        }

        public List<Meci> GetMeciuri()
        {
            return meciRepository.findAll();
        }

        public List<Meci> GetAllMeciuriByData(DateTime data1, DateTime data2)
        {
            List<Meci> meciList = new List<Meci>();
            foreach (Meci meci in meciRepository.findAll())
                if(DateTime.Compare(meci.data, data1)>=0 && DateTime.Compare(meci.data,data2)<=0)
                    meciList.Add(meci);
            return meciList;
        }

        public Meci GetMeciByIndex(int index)
        {
            int i = 0;
            foreach (Meci meci in GetMeciuri())
            {
                if (i == index)
                    return meci;
                i++;
            }
            return null;
        }
    }
}
