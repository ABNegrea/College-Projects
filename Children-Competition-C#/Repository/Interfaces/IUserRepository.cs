using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concurs.Domain;

namespace Concurs.Repository.Interfaces
{
    internal interface IUserRepository: IRepository<Guid, User>
    {
        User FindByEmailPassword(string email, string password);
    }
}
