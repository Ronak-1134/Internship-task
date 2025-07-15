using Mission.Entities.context;
using Mission.Entities.Models;
using Mission.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Repositories.Repositories
{
    public class AdminUserRepository(MissionDbContext _missionDbContext):IAdminUserRepository
    {
        public List<UserDetails> UserDetialsList()
        {
            var res = _missionDbContext.Users.Where(x => x.UserType == "user" && !x.IsDeleted).Select(x => new UserDetails
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                EmailAddress = x.EmailAddress,
                UserType = x.UserType,
            });
            return res.ToList();
        }

        public string DeleteUser(int id)
        {
            var user = _missionDbContext.Users.Where(x=>x.Id == id).FirstOrDefault();

            if (user == null) throw new Exception("Account not exist");

            user.IsDeleted = true;
            user.ModifiedDate= DateTime.UtcNow;
            _missionDbContext.Users.Update(user);
            _missionDbContext.SaveChanges();
            return "Account deleted!";
        }
    }
}
