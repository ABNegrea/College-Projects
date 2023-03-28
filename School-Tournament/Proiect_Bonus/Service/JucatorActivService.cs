using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_Bonus.Domain;
using Proiect_Bonus.Repository;

namespace Proiect_Bonus.Service
{
    internal class JucatorActivService
    {
        private JucatorActivRepository jucatorActivRepository;
        private JucatorRepository jucatorRepository;

        public JucatorActivService(JucatorActivRepository repo, JucatorRepository repo2)
        {
            jucatorActivRepository = repo;
            jucatorRepository = repo2;
        }

        public List<JucatorActiv> GetJucatori()
        {
            return jucatorActivRepository.findAll();
        }

        public List<JucatorActiv> GetJucatoriActivi(Echipa echipa, Meci meci)
        {
            List<JucatorActiv> lst = new List<JucatorActiv> ();
            foreach (JucatorActiv jucatorActiv in GetJucatori())
                if (jucatorActiv.idMeci == meci.id)
                     if(jucatorRepository.findOne(jucatorActiv.idJucator).echipa == echipa.nume)
                        lst.Add(jucatorActiv);
            return lst;
        }

        public int GetScoreEchipaMeci(string echipa, Meci meci)
        {
            int score = 0;
            foreach (JucatorActiv jucatorActiv in GetJucatori())
                if (jucatorActiv.idMeci == meci.id)
                    if (jucatorRepository.findOne(jucatorActiv.idJucator).echipa == echipa)
                        score += jucatorActiv.puncteInscrise;
            return score;
        }
    }
}
