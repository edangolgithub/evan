using LiquorShopService.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TescoWineShopSql;

namespace LiquorShopService.Implementations
{
    public class AdministrationService : DbService, IAdministrationService
    {
        public bool DeleteAndRefreshDatabase()
        {
            try
            {
                Context.Database.Connection.Close();
                Context.Database.Delete();
                Context.Database.Initialize(false);
                DatabaseSeeder.Seed(Context);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
                // throw ex;
                // false;
            }

        }

        public int DeleteCompany(Company selectedCompany)
        {
            Context.Companies.Remove(selectedCompany);
            return Context.SaveChanges();
        }

        public async Task<User> FindUserByUserNameAndPassword(string uname, string pass)
        {
            try
            {
                return await Context.Users.Include(a => a.UserRole).Where(a => a.UserName == uname).Where(a => a.Password == pass).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<IEnumerable<BeverageCategory>> getAllBeverageCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Beverage>> GetAllBeveragesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Company> GetCurrentCompanyAsync(string company, string password)
        {
            return await Context.Companies.Where(a => a.CompanyName == company && a.CompanyPassword == password).FirstOrDefaultAsync();
        }

        public WineContext GetCurrentContext()
        {
            return Context;
        }

        public async Task<Company> GetSelectedCompanyAsync()
        {
            return await Context.Companies.FirstOrDefaultAsync();
        }

        public async Task<int> SaveBeverageAsync(List<Rms.Classes.MenuItem> MenuItems)
        {
            using (DbContextTransaction transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var catlist = await Context.BeverageCategories.ToListAsync();
                    var items = await Context.Beverages.ToListAsync();
                    foreach (var bevcat in MenuItems.GroupBy(a => a.MenuCategoryName))
                    {

                        var cat = bevcat.Key;

                        BeverageCategory flag = catlist.Where(a => a.BeverageCategoryName == cat).FirstOrDefault();

                        if (flag == null)
                        {
                            flag = new BeverageCategory();
                            flag.BeverageCategoryName = cat;
                            Context.BeverageCategories.Add(flag);

                        }
                        else
                        {
                            Context.Entry(flag).State = EntityState.Modified;
                        }
                        await Context.SaveChangesAsync();
                        foreach (var i in bevcat)
                        {
                            var beverage = i.beverage;
                            var price = i.Price;
                            Beverage flag1 = items.Where(a => a.Name.Trim().ToUpper() == beverage.Trim().ToUpper()).FirstOrDefault();
                            if (flag1 == null)
                            {
                                flag1 = new Beverage();
                                flag1.Name = beverage;
                                flag1.Price = price;
                                flag1.IsPopular = i.IsPopular;
                                flag1.ShowInChart = i.ShowInChart;
                                int volume = 0;
                                int.TryParse(i.Volume, out volume);
                                flag1.Volume = volume;
                                flag1.Image = i.Image;
                                flag1.DrinkType = Drinktype.Whisky;
                                flag1.BeverageCategoryId = flag.BeverageCategoryId;
                                Context.Beverages.Add(flag1);
                            }
                            else
                            {
                                Context.Entry(flag1).State = EntityState.Modified;
                            }
                            await Context.SaveChangesAsync();

                        }


                    }
                    transaction.Commit();
                    return 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }


        }

        //public AdministrationService()
        //{
        //    ResolveFiscalYear();
        //}

        int IAdministrationService.DeleteAdministration(Administration Administration)
        {
            Context.Administrations.Remove(Administration);
            return Context.SaveChanges();
        }


        Administration IAdministrationService.FindAdministrationByID(int id)
        {
            throw new NotImplementedException();
        }



        IEnumerable<Administration> IAdministrationService.GetAllAdministrations()
        {
            return Context.Administrations.ToList();
        }

        async Task<IEnumerable<Administration>> IAdministrationService.GetAllAdministrationsAsync()
        {
            return await Context.Administrations.ToListAsync();
        }

        async Task<IEnumerable<Company>> IAdministrationService.GetAllCompaniesAsync()
        {
            return await Context.Companies.Include(a => a.FiscalYear).ToListAsync();
        }




        async Task<IEnumerable<LedgerAccount>> IAdministrationService.GetAllLedgerAccountsForChartSettingsAsync()
        {
            return await Context.LedgerAccounts.ToListAsync();
        }


        void IAdministrationService.ResetAdministration()
        {
            DatabaseSeeder.Seed(GetCurrentContext());

        }

        int IAdministrationService.SaveAdministration(Administration Administration)
        {
            Context.Administrations.Add(Administration);
            return Context.SaveChanges();
        }


        async Task<int> IAdministrationService.SaveBeverageCategoryAsync()
        {
            return await Context.SaveChangesAsync();
        }

        int IAdministrationService.SaveCompany(Company Company, FiscalYear fy)
        {
            try
            {
                //foreach (var item in Context.Companies)
                //{
                //    item.IsActive = false;
                //}
                //Company.IsActive = true;
                var v = Context.FiscalYears.Where(a => a.StartDate == fy.StartDate).Where(a => a.EndDate == fy.EndDate).FirstOrDefault();
                if (v == null)
                {
                    Context.FiscalYears.Add(fy);
                    Context.SaveChanges();
                    Company.FiscalYearId = fy.FiscalYearId;
                }
                if (Company.CompanyId <= 0)
                {
                    Context.Companies.Add(Company);
                }
                else
                {
                    Company.FiscalYearId = v.FiscalYearId;
                    Context.Entry(Company).State = EntityState.Modified;
                }
                return  Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
