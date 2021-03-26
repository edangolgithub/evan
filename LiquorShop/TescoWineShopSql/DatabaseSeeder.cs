using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TescoWineShopSql
{
   public class DatabaseSeeder
    {
        public static  void Seed(TescoWineShopSql.WineContext context)
        {

            context.BeverageCategories.AddOrUpdate(a => a.BeverageCategoryName,
              new BeverageCategory {BeverageCategoryId=1, BeverageCategoryName = "Whisky" },
              new BeverageCategory {BeverageCategoryId=2, BeverageCategoryName = "Wine" },
              new BeverageCategory {BeverageCategoryId=3, BeverageCategoryName = "Beer" },
              new BeverageCategory {BeverageCategoryId=4, BeverageCategoryName = "Soft Drink" }


              );

            InsertBeverages().ForEach(b => context.Beverages.AddOrUpdate(a => a.BeverageId, b));
            //InsertSales().ForEach(s => context.Sales.AddOrUpdate(a => a.SaleId, s));
            InsertSuppliers().ForEach(s => context.Suppliers.AddOrUpdate(a => a.SupplierId, s));
            //InsertPurchases().ForEach(s => context.Purchases.AddOrUpdate(a => a.PurchaseId, s));
            InsertCustomers().ForEach(c => context.Customers.AddOrUpdate(a => a.CustomerId, c));
            context.Vats.AddOrUpdate(a => a.VatId, new Vat { VatId = 1, VatAmount = 13 });
            context.UserRoles.AddOrUpdate(a => a.UserRoleId,
              new UserRole
              {
                  UserRoleId = 1,
                  UserInRole = UserRoles.Staff
              },
               new UserRole
               {
                   UserRoleId = 2,
                   UserInRole = UserRoles.Administrator
               },
          new UserRole
          {
              UserRoleId = 3,
              UserInRole = UserRoles.Accountant
          },
               new UserRole
               {
                   UserRoleId = 4,
                   UserInRole = UserRoles.Cashier
               },
          new UserRole
          {
              UserRoleId = 5,
              UserInRole = UserRoles.Guest
          },
               new UserRole
               {
                   UserRoleId = 6,
                   UserInRole = UserRoles.HeadChef
               },
          new UserRole
          {
              UserRoleId = 7,
              UserInRole = UserRoles.KitchenSupport
          },
               new UserRole
               {
                   UserRoleId = 8,
                   UserInRole = UserRoles.Manager
               },
          new UserRole
          {
              UserRoleId = 9,
              UserInRole = UserRoles.SousChef
          },
               new UserRole
               {
                   UserRoleId = 10,
                   UserInRole = UserRoles.Waiter
               });
            context.Users.AddOrUpdate(a => a.UserName,
                new User
                {
                    UserId = 1,
                    UserName = "admin",
                    UserRoleId = 2,
                    UserEmail = "dangolevan@gmail.com",
                    UserCity = "Kathmandu",
                    Password = "dMRo3oqVLOs=",
                    UserAddress = "Dathu Sadak",
                    UserTole = "Khusibu",
                    UserPhone = "9849178036",
                    UserCreatedOn = DateTime.Today
                },
                   new User
                   {
                       UserId = 2,
                       UserName = "a",
                       Password = "R2LOIQb+UyM=",
                       UserRoleId = 2,
                       UserEmail = "ram@gmail.com",
                       UserCity = "Kathmandu",
                       UserAddress = "Dathu Sadak",
                       UserTole = "Khusibu",
                       UserPhone = "9849178036",
                       UserCreatedOn = DateTime.Today
                   },


                    new User
                    {
                        UserId = 2,
                        UserName = "b",
                        Password = "R2LOIQb+UyM=",
                        UserRoleId = 3,
                        UserEmail = "dangolevan@gmail.com",
                        UserCity = "Kathmandu",
                        UserAddress = "Dathu Sadak",
                        UserTole = "Khusibu",
                        UserPhone = "9849178036",
                        UserCreatedOn = DateTime.Today
                    },
                     new User
                     {
                         UserId = 3,
                         UserName = "c",
                         Password = "R2LOIQb+UyM=",
                         UserRoleId = 4,
                         UserEmail = "dangolevan@gmail.com",
                         UserCity = "Kathmandu",
                         UserAddress = "Dathu Sadak",
                         UserTole = "Khusibu",
                         UserPhone = "9849178036",
                         UserCreatedOn = DateTime.Today
                     }
                   );

            //context.AccountPeriods.AddOrUpdate(a => a.Period,
            //    new AccountPeriod { AccountPeriodId = 1, Period = 2074 },
            //    new AccountPeriod { AccountPeriodId = 2, Period = 2075 },
            //    new AccountPeriod { AccountPeriodId = 3, Period = 2076 }
            //    );
            context.Companies.AddOrUpdate(a => a.CompanyName,
               new Company { CompanyId = 1, CompanyName = "Tesco Wine Store", CompanyAddress = "Reliable Colony, Bhainsepati", CompanyCity = "Lalitpur", CompanyPanNumber = "12345", CompanyPhone = "12345" }

             );
            context.AccountClasses.AddOrUpdate(a => a.Name,
                new AccountClass { AccountClassId = 1, Name = "Asset" },
                new AccountClass { AccountClassId = 2, Name = "Expense" },
                new AccountClass { AccountClassId = 3, Name = "Revenue" },
                new AccountClass { AccountClassId = 4, Name = "Liabilities" },
                new AccountClass { AccountClassId = 5, Name = "Equity" }
                );
            context.AccountGroups.AddOrUpdate(a => a.AccountGroupName,
             new AccountGroup { AccountClassId = 1, AccountGroupName = "FixedAsset" }
      );


            context.LedgerAccounts.AddOrUpdate(a => a.AccountName,
               new LedgerAccount { AccountClassId = 1, AccountGroupId = 1, IsSystemLedger = true, AccountName = "Cash", CompanyId = 1, ShowInChart = true },
              new LedgerAccount { AccountClassId = 3, AccountGroupId = 1, IsSystemLedger = true, AccountName = "Sales", CompanyId = 1, ShowInChart = true },
              new LedgerAccount { AccountClassId = 2, AccountGroupId = 1, IsSystemLedger = true, AccountName = "Purchase", CompanyId = 1, ShowInChart = true },
              new LedgerAccount { AccountClassId = 1, AccountGroupId = 1, AccountName = "Debitors", CompanyId = 1, ShowInChart = true },
              new LedgerAccount { AccountClassId = 4, AccountGroupId = 1, IsSystemLedger = true, AccountName = "Creditors", CompanyId = 1, ShowInChart = true },
              new LedgerAccount { AccountClassId = 4, AccountGroupId = 1, IsSystemLedger = true, AccountName = "Tesco Suppliers", ParentLedgerAccountId = 5, CompanyId = 1, ShowInChart = true },
              new LedgerAccount { AccountClassId = 1, AccountGroupId = 1, IsSystemLedger = true, AccountName = "Bank", CompanyId = 1, ShowInChart = true },
              new LedgerAccount { AccountClassId = 1, AccountGroupId = 1, IsSystemLedger = true, AccountName = "Tesco Bank", CompanyId = 1, ParentLedgerAccountId = 7, ShowInChart = true },
              new LedgerAccount { AccountClassId = 1, AccountGroupId = 1, IsSystemLedger = true, AccountName = "Accounts Receivable", CompanyId = 1, ShowInChart = true },
              new LedgerAccount { AccountClassId = 4, AccountGroupId = 1, IsSystemLedger = true, AccountName = "Accounts Payable", CompanyId = 1, ShowInChart = true },
              new LedgerAccount { AccountClassId = 2, AccountGroupId = 1, AccountName = "Salaries", CompanyId = 1, ShowInChart = true },
              new LedgerAccount { AccountClassId = 2, AccountGroupId = 1, AccountName = "Rent Expense", CompanyId = 1, ShowInChart = true },
              new LedgerAccount { AccountClassId = 2, AccountGroupId = 1, AccountName = "Advertising Expenses", CompanyId = 1, ShowInChart = true },
              new LedgerAccount { AccountClassId = 1, AccountGroupId = 1, AccountName = "FixedAsset", CompanyId = 1, ShowInChart = true },
              new LedgerAccount { AccountClassId = 1, AccountGroupId = 1, IsSystemLedger =true, AccountName = "Ram",ParentLedgerAccountId=4, CompanyId = 1, ShowInChart = true }



              );
            var barpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WineShop\\BarCode";
            var imagepath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WineShop\\Images";
            var documentpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WineShop\\Documents";
            var jsonpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WineShop\\Json";
            context.Administrations.AddOrUpdate(a => a.Key,

                new Administration { Key = "TaxRate", value = "13" },
                new Administration { Key = "ServiceCharge", value = "10" },
                new Administration { Key = "ImageFolder", value = imagepath },
                new Administration { Key = "DocumentFolder", value = documentpath },
                new Administration { Key = "Tables", value = "50" },
                new Administration { Key = "JsonFolder", value = jsonpath },
                new Administration { Key = "VatEnabled", value = "0" },
                new Administration { Key = "Vat", value = "13" },
                new Administration { Key = "FiscalYear", value = "2075/2076" },
                new Administration { Key = "DiscountStyle", value = "Percent" },
                new Administration { Key = "AutomateLedgerPost", value = "1" },
                  new Administration { Key = "UseBarCode", value = "0" },
                   new Administration { Key = "Currency", value = "1" },
                new Administration { Key = "BarCodeFolder", value = barpath });



            context.SaveChanges();
        }

        private static List<Customer> InsertCustomers()
        {
            var customers = new List<Customer>{
        new Customer{ CustomerId=1, CustomerName="Ram", CustomerAddress="Bhaisepati", CustomerPhone="23234245"}
    };
            return customers;
        }

        private static List<Supplier> InsertSuppliers()
        {
            var suppliers = new List<Supplier>{
        new Supplier{ SupplierId=1, SupplierName="Tesco Suppliers", SupplierAddress="Thamel", SupplierPhone="123456789",SupplierCity="Kathmandu" }
         };
            return suppliers;

        }
        private static List<Beverage> InsertBeverages()
        {
            var beverages = new List<Beverage>{
           new Beverage{ BeverageId=1, BeverageCategoryId=1, DrinkType=Drinktype.Whisky, Name="Nepal Ice Beer Bottle 650", Country="Nepal", Price=110, Volume=650, Image="Nepalice.png"},
           new Beverage{ BeverageId=2,BeverageCategoryId=1, DrinkType=Drinktype.Whisky, Name="Golden Oak 750", Country="Nepal", Price=110, Volume=750, Image="goldenoak.png"},
           new Beverage{ BeverageId=3,BeverageCategoryId=1, DrinkType=Drinktype.Rum, Name="Royal Stag 750", Country="Nepal", Price=110, Volume=750, Image="royalstag.png"},
           new Beverage{ BeverageId=4,BeverageCategoryId=1, DrinkType=Drinktype.Wine, Name="Signature Rare 750", Country="Nepal", Price=110, Volume=750, Image="signature.png"},
           new Beverage{ BeverageId=5,BeverageCategoryId=2, DrinkType=Drinktype.Vodka, Name="McDowell's No 1", Country="Nepal", Price=110, Volume=750, Image="dis.png"}

           };
            return beverages;

        }
    }
}
