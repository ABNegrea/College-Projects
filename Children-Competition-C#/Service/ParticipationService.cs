using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concurs.Domain;
using Concurs.Repository;

namespace Concurs.Service
{
    internal class ParticipationService
    {
        private ParticipationDBRepository participationDBRepository;

        public ParticipationService(ParticipationDBRepository participationDBRepository)
        {
            this.participationDBRepository = participationDBRepository;
        }

        public Participation AddParticipation(Participation participation)
        {
            return this.participationDBRepository.Save(participation);
        }
        public List<Participation> FindAll()
        {
            return (List<Participation>)this.participationDBRepository.FindAll();
        }
        public int ParticipationCountChild(Guid IdChild)
        {
            return participationDBRepository.ParticipationCountChild(IdChild);
        }
    }
}
