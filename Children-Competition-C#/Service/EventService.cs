using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concurs.Domain;
using Concurs.Repository;

namespace Concurs.Service
{
    internal class EventService
    {
        private EventDBRepository eventDBRepository;
        public EventService(EventDBRepository eventDBRepository)
        {
            this.eventDBRepository = eventDBRepository;
        }

        public List<Event> FindAll()
        {
            return (List<Event>)this.eventDBRepository.FindAll();
        }

        public Event FinyByNameAge(string Name, int AgeInRange)
        {
            return this.eventDBRepository.FindByNameAge(Name, AgeInRange);
        }

        public int AddEnrolledToEvent(Guid id)
        {
            return this.eventDBRepository.AddEnrolledToEvent(id);
        }
    }
}
