using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rms.Data;

namespace TescoWineShopSql
{
    public enum UserRoles {Staff, Administrator,Manager,Accountant,Cashier,Waiter,HeadChef,SousChef,KitchenSupport,Guest}
    public class User: ModelBase
    {
        public int UserId { get; set; }
      
        [MaxLength(50, ErrorMessage = "Less than 50 chars")]
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Menus { get; set; }
      
        private string _UserEmail;
        [EmailAddress]
        public string UserEmail
        {
            get { return _UserEmail; }
            set
            {
                SetProperty<string>(ref _UserEmail, value);
            }
        }
        public string UserPhone { get; set; }
        public string UserAddress { get; set; }
        public string UserTole { get; set; }
        public string UserCity { get; set; }
        public int UserRoleId { get; set; }
       public UserRole UserRole { get; set; }
      //  public User UserCreatedBy { get; set; }
        //public int UserCreatedByUserId { get; set; }
        public DateTime UserCreatedOn { get; set; }

    }

    public class UserRole
    {
        public int UserRoleId { get; set; }
        public UserRoles UserInRole { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
