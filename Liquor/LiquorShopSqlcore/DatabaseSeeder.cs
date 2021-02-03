using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TescoWineShopSql
{
    public class DatabaseSeeder
    {
        public static void Seed(ModelBuilder modelbuilder)
        {

            modelbuilder.Entity<BeverageCategory>().HasData(
             new BeverageCategory { BeverageCategoryId = 1, BeverageCategoryName = "Whisky" },
             new BeverageCategory { BeverageCategoryId = 2, BeverageCategoryName = "Wine" },
             new BeverageCategory { BeverageCategoryId = 3, BeverageCategoryName = "Beer" },
             new BeverageCategory { BeverageCategoryId = 4, BeverageCategoryName = "Soft Drink" }
            );

            modelbuilder.Entity<Vat>().HasData(new Vat { VatId = 1, VatAmount = 13 });
            modelbuilder.Entity<UserRole>().HasData(
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
            modelbuilder.Entity<User>().HasData(
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
               UserId = 3,
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
                UserId = 4,
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
            modelbuilder.Entity<Company>().HasData(
     new Company { CompanyId = 1, CompanyName = "Tesco Wine Store", CompanyAddress = "Reliable Colony, Bhainsepati", CompanyCity = "Lalitpur", CompanyPanNumber = "12345", CompanyPhone = "12345" }

   );
            modelbuilder.Entity<AccountClass>().HasData(
                new AccountClass { AccountClassId = 1, Name = "Asset" },
                new AccountClass { AccountClassId = 2, Name = "Expense" },
                new AccountClass { AccountClassId = 3, Name = "Revenue" },
                new AccountClass { AccountClassId = 4, Name = "Liabilities" },
                new AccountClass { AccountClassId = 5, Name = "Equity" }
                );
            modelbuilder.Entity<AccountGroup>().HasData(
                new AccountGroup {AccountGroupId=1, AccountClassId = 1, AccountGroupName = "FixedAsset" }
);


            modelbuilder.Entity<LedgerAccount>().HasData(
        new LedgerAccount { LedgerAccountId=1, AccountClassId = 1, AccountGroupId = 1, IsSystemLedger = true, AccountName = "Cash", CompanyId = 1, ShowInChart = true },
        new LedgerAccount {LedgerAccountId=2, AccountClassId = 3, AccountGroupId = 1, IsSystemLedger = true, AccountName = "Sales", CompanyId = 1, ShowInChart = true },
        new LedgerAccount {LedgerAccountId=3, AccountClassId = 2, AccountGroupId = 1, IsSystemLedger = true, AccountName = "Purchase", CompanyId = 1, ShowInChart = true },
        new LedgerAccount {LedgerAccountId=4, AccountClassId = 1, AccountGroupId = 1, AccountName = "Debitors", CompanyId = 1, ShowInChart = true },
        new LedgerAccount {LedgerAccountId=5, AccountClassId = 4, AccountGroupId = 1, IsSystemLedger = true, AccountName = "Creditors", CompanyId = 1, ShowInChart = true },
        new LedgerAccount {LedgerAccountId=6, AccountClassId = 4, AccountGroupId = 1, IsSystemLedger = true, AccountName = "Tesco Suppliers", ParentLedgerAccountId = 5, CompanyId = 1, ShowInChart = true },
        new LedgerAccount { LedgerAccountId=7,AccountClassId = 1, AccountGroupId = 1, IsSystemLedger = true, AccountName = "Bank", CompanyId = 1, ShowInChart = true },
        new LedgerAccount {LedgerAccountId=8, AccountClassId = 1, AccountGroupId = 1, IsSystemLedger = true, AccountName = "Tesco Bank", CompanyId = 1, ParentLedgerAccountId = 7, ShowInChart = true },
        new LedgerAccount { LedgerAccountId=9,AccountClassId = 1, AccountGroupId = 1, IsSystemLedger = true, AccountName = "Accounts Receivable", CompanyId = 1, ShowInChart = true },
        new LedgerAccount {LedgerAccountId=10, AccountClassId = 4, AccountGroupId = 1, IsSystemLedger = true, AccountName = "Accounts Payable", CompanyId = 1, ShowInChart = true },
        new LedgerAccount {LedgerAccountId=11, AccountClassId = 2, AccountGroupId = 1, AccountName = "Salaries", CompanyId = 1, ShowInChart = true },
        new LedgerAccount {LedgerAccountId=12, AccountClassId = 2, AccountGroupId = 1, AccountName = "Rent Expense", CompanyId = 1, ShowInChart = true },
        new LedgerAccount {LedgerAccountId=13, AccountClassId = 2, AccountGroupId = 1, AccountName = "Advertising Expenses", CompanyId = 1, ShowInChart = true },
        new LedgerAccount {LedgerAccountId=14, AccountClassId = 1, AccountGroupId = 1, AccountName = "FixedAsset", CompanyId = 1, ShowInChart = true },
        new LedgerAccount {LedgerAccountId=15, AccountClassId = 1, AccountGroupId = 1, IsSystemLedger = true, AccountName = "Ram", ParentLedgerAccountId = 4, CompanyId = 1, ShowInChart = true }



        );
            var barpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WineShop\\BarCode";
            var imagepath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WineShop\\Images";
            var documentpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WineShop\\Documents";
            var jsonpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WineShop\\Json";
            modelbuilder.Entity<Administration>().HasData(

                  new Administration { Administrationid=1, Key = "TaxRate", value = "13" },
                  new Administration { Administrationid=2,Key = "ServiceCharge", value = "10" },
                  new Administration {Administrationid=3, Key = "ImageFolder", value = imagepath },
                  new Administration { Administrationid=4,Key = "DocumentFolder", value = documentpath },
                  new Administration {Administrationid=5, Key = "Tables", value = "50" },
                  new Administration {Administrationid=6, Key = "JsonFolder", value = jsonpath },
                  new Administration {Administrationid=7, Key = "VatEnabled", value = "0" },
                  new Administration {Administrationid=8, Key = "Vat", value = "13" },
                  new Administration {Administrationid=9, Key = "FiscalYear", value = "2075/2076" },
                  new Administration {Administrationid=10, Key = "DiscountStyle", value = "Percent" },
                  new Administration {Administrationid=11, Key = "AutomateLedgerPost", value = "1" },
                  new Administration {Administrationid=12, Key = "UseBarCode", value = "0" },
                  new Administration {Administrationid=13, Key = "Currency", value = "1" },
                  new Administration {Administrationid=14, Key = "BarCodeFolder", value = barpath });
       

        }
    }
}
