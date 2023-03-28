using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concurs.Domain;
using Concurs.Repository;

namespace Concurs.Service
{
    internal class UserService
    {
        private UserDBRepository UserDBRepository;

        public UserService(UserDBRepository UserDBRepository)
        {
            this.UserDBRepository = UserDBRepository;
        }

        public User FindByEmailPassword(string email, string password)
        {
            User user = UserDBRepository.FindByEmailPassword(email, password);
            return user;
        }
    }
}
