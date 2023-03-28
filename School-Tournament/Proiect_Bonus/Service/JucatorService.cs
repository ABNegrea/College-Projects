using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_Bonus.Domain;
using Proiect_Bonus.Repository;

namespace Proiect_Bonus.Service
{
    internal class JucatorService
    {
        private JucatorRepository jucatorRepository;

        public JucatorService(JucatorRepository repo)
        {
            jucatorRepository = repo;
        }

        public List<Jucator> GetJucatori()
        {
            return jucatorRepository.findAll();
        }

        public List<Jucator> GetJucatoriFromEchipa(string echipa)
        {
            List<Jucator> jucatori = new List<Jucator>();
            foreach (Jucator jucator in jucatorRepository.findAll())
                if (jucator.echipa == echipa)
                    jucatori.Add(jucator);
            return jucatori;
        }

        public string GetJucatorNume(Guid id)
        {
            return jucatorRepository.findOne(id).nume;
        }
    }
}
