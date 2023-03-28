using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concurs.Domain;
using Concurs.Repository;

namespace Concurs.Service
{
    internal class ChildService
    {
        private  ChildDBRepository childDBRepository;

        public ChildService(ChildDBRepository childDBRepository)
        {
            this.childDBRepository = childDBRepository;
        }

        public Child AddChild(Child child)
        {
            return this.childDBRepository.Save(child);
        }

        public Child FindChildByNameAge(string FirstName, string LastName, int age)
        {
            return this.childDBRepository.FindChildByNameAge(FirstName, LastName, age);
        }
    }
}
