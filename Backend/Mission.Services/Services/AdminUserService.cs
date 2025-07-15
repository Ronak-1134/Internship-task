using Mission.Entities.Models;
using Mission.Repositories.IRepositories;
using Mission.Repositories.Repositories;
using Mission.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Services.Services
{
    public class AdminUserService(IAdminUserRepository _adminUserRepository):IAdminUserService
    {
        public List<UserDetails> UserDetailsList()
        {
            return _adminUserRepository.UserDetialsList();
        }
        public string Userdelete(int id)
        {
            return _adminUserRepository.DeleteUser(id);
        }
    }
}
