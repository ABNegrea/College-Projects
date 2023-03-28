using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concurs.Domain;

namespace Concurs.Repository.Interfaces
{
    internal interface IChildRepository: IRepository<Guid, Child>
    {
        Child FindChildByNameAge(string firstName, string lastName, int age);
    }
}
