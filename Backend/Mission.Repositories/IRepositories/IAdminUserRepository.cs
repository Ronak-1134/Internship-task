using Mission.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Repositories.IRepositories
{
    public interface IAdminUserRepository
    {
        List<UserDetails> UserDetialsList();
        string DeleteUser(int id);
    }
}
