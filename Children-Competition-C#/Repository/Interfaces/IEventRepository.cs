using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concurs.Domain;

namespace Concurs.Repository.Interfaces
{
    internal interface IEventRepository: IRepository<Guid, Event>
    {
        int AddEnrolledToEvent(Guid id);

        Event FindByNameAge(string name, int ageInRange);
    }
}
