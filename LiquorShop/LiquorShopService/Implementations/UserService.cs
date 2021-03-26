using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rms.Data;
using System.Data.Entity;
using LiquorShopService.Services;
using TescoWineShopSql;

namespace LiquorShopService.Implementations
{
    public class UserService : DbService, IUserService
    {
        public async Task<int> DeleteUser(User User)
        {
            try
            {
                Context.Users.Remove(User);
                return await SaveAll();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<User> FindUsersById(int id)
        {
            return await Context.Users.FindAsync(id);
        }
        public  IEnumerable<User> GetAllUsers()
        {
            return  Context.Users.ToList();
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await Context.Users.ToListAsync();
        }
    
        public async Task<int> SaveAll()
        {
            return await Context.SaveChangesAsync();
        }
        public async Task<int> SaveUser(User User)
        {
            if (User.UserId < 1)
            {
                Context.Users.Add(User);
            }
            else
            {
                Context.Entry(User).State = EntityState.Modified;
            }
            return await SaveAll();
        }
        public async Task<int> DeleteUserRole(UserRole UserRole)
        {
            Context.UserRoles.Remove(UserRole);
            return await SaveAll();
        }
        public async Task<UserRole> FindUserRolesById(int id)
        {
            return await Context.UserRoles.FindAsync(id);
        }
        public IEnumerable<UserRole> GetAllUserRoles()
        {
            return Context.UserRoles.ToList();
        }
        public async Task<IEnumerable<UserRole>> GetAllUserRolesAsync()
        {
            return await Context.UserRoles.ToListAsync();
        }
       
        public async Task<int> SaveUserRole(UserRole UserRole)
        {
            if (UserRole.UserRoleId < 1)
            {
                Context.UserRoles.Add(UserRole);
            }
            else
            {
                Context.Entry(UserRole).State = EntityState.Modified;
            }
            return await SaveAll();
        }
        public async Task<User> FindUserByUserNameAndPassword(string uname,string pass)
        {
            return await Context.Users.Include(a=>a.UserRole).Where(a => a.UserName == uname).Where(a => a.Password == pass).FirstOrDefaultAsync();
        }
    }
}
