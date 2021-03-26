using Rms.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace LiquorShopService.Services
{
  public  interface IUserService:IUserRoleService
    {
        Task<int> SaveAll();
        Task<User> FindUsersById(int id);
        Task<User> FindUserByUserNameAndPassword(string uname,string pass);
        Task<IEnumerable<User>> GetAllUsersAsync();
        IEnumerable<User> GetAllUsers();
        Task<int> SaveUser(User User);
        Task<int> DeleteUser(User User);
       
    }
    public interface IUserRoleService
    {
        Task<UserRole> FindUserRolesById(int id);
        Task<IEnumerable<UserRole>> GetAllUserRolesAsync();
        IEnumerable<UserRole> GetAllUserRoles();
        Task<int> SaveUserRole(UserRole UserRole);
        Task<int> DeleteUserRole(UserRole UserRole);
    }
}
