using EvanApi.models;
using Microsoft.EntityFrameworkCore;

namespace EvanApi.Data
{
    internal class DatabaseSeeder
    {
          public static void Seed(ModelBuilder modelbuilder)
        {

            modelbuilder.Entity<Account>().HasData(
             new Account { accountid="1", name="Evan Dangol",  address="Khusibu" ,email="dangolevan@gmail.com",phone="123456789", id="1" },
             new Account { accountid="2", name="Bso Amatya",  address="Kilagal" ,email="bso@gmail.com",phone="234567891", id="2" }
            );
            modelbuilder.Entity<AccountType>().HasData(
             new AccountType { accounttypeid="1", accounttype="Daily",id="1" },
             new AccountType { accounttypeid="2",accounttype="Month",  id="2" }
            );
            modelbuilder.Entity<Transaction>().HasData(
             new Transaction {id="1",accountid="1",accounttypeid="1", amount="100", date="2021-01-01",entry="Debit"  },
             new Transaction {id="2",accountid="2",accounttypeid="2", amount="100", date="2021-01-01",entry="Debit"  }
            );
        }

    }
}