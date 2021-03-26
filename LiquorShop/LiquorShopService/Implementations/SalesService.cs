using LiquorShopService.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace LiquorShopService.Implementations
{
    public class SalesService : DbService, ISalesService
    {
        public async Task<IEnumerable<Beverage>> GetAllBeveragesForChartAsync()
        {
            return await Context.Beverages.Where(a => a.ShowInChart).ToListAsync();
        }
        public async Task<IEnumerable<Beverage>> GetAllBeveragesAsync()
        {
            return await Context.Beverages.ToListAsync();
        }

        public async Task<IEnumerable<Beverage>> GetAllBeveragesByDrinkTypeAsync(Drinktype DType)
        {
            return await Context.Beverages.Where(a => a.DrinkType == DType).ToListAsync();
        }
        public async Task<IEnumerable<Beverage>> GetAllBeveragesByName(string BeverageName)
        {
            return await Context.Beverages.Where(a => a.Name.Contains(BeverageName)).ToListAsync();
        }




        public async Task<int> DeleteSaleAsync(Sale Sale)
        {
            if (Sale != null)
            {
                Context.Sales.Remove(Sale);

            }
            return await Context.SaveChangesAsync();
        }



        public async Task<Sale> FindSaleByIDAsync(int id)
        {
            return await Context.Sales.Where(a => a.SaleId == id).SingleOrDefaultAsync();
        }





        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            return await Context.Sales.ToListAsync();
        }

        public Task<int> SaveBeverageAsync(Beverage Beverage)
        {
            throw new NotImplementedException();
        }

        public int SaveSale(Sale Sale)
        {
            if (Sale == null)
            {
                return 0;
            }
            if (Sale.SaleId < 1)
            {
                Context.Sales.Add(Sale);

            }
            return Context.SaveChanges();

        }

        public int SaveInvoice(Invoice Invoice)
        {
            if (Invoice == null)
            {
                return 0;
            }
            if (Invoice.InvoiceId < 1)
            {
                Context.Invoices.Add(Invoice);

            }
            return Context.SaveChanges();

        }


        public async Task<IEnumerable<Sale>> FindSalesByDateAsync(DateTime Start, DateTime End)
        {
            return await Context.Sales.Include("Beverage").Where(a => DbFunctions.TruncateTime(a.SaleDate) >= Start.Date && DbFunctions.TruncateTime(a.SaleDate) <= End.Date)
                .ToListAsync();
        }

        public async Task<int> GetBeverageStockAsync(Beverage Beverage)
        {
            var Sale = await Context.Sales.Where(a => a.BeverageId == Beverage.BeverageId).Select(a => a.Quantity).DefaultIfEmpty().SumAsync();
            var sale = await Context.Sales.Where(a => a.BeverageId == Beverage.BeverageId).Select(a => a.Quantity).DefaultIfEmpty().SumAsync();
            var total = Sale - sale;
            return (total);
        }

        public async Task<IEnumerable<Sale>> FindSalesByBeverageAsync(Beverage Beverage)
        {
            return await Context.Sales.Where(a => a.Beverage.BeverageId == Beverage.BeverageId).ToListAsync();
        }

        public Task<IEnumerable<Sale>> FindSalesTotalByBeverageAsync(Beverage Beverage)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalSaleAsync(Beverage Beverage)
        {
            return await Context.Sales
                         .Where(a => a.BeverageId == Beverage.BeverageId).DefaultIfEmpty().Select(a => (a.Quantity)).SumAsync();

        }

        public async Task<IEnumerable<Invoice>> FindInvoiceByInvoiceNumberAsync(int id)
        {
            return await Context.Invoices.Where(a => a.InvoiceNumber == id).ToListAsync();
        }

        public async Task<IEnumerable<Sale>> FindSalesByBeverageAndDateAsync(DateTime Start, DateTime End, Beverage Beverage = null)
        {
            if (Beverage == null)
            {
                return await Context.Sales
              .Where(a => DbFunctions.TruncateTime(a.SaleDate) >= Start.Date
               && DbFunctions.TruncateTime(a.SaleDate) <= End.Date)
              .ToListAsync();
            }

            return await Context.Sales
                .Where(a => a.Beverage.BeverageId == Beverage.BeverageId)
                .Where(a => DbFunctions.TruncateTime(a.SaleDate) >= Start.Date
                 && DbFunctions.TruncateTime(a.SaleDate) <= End.Date)
                .ToListAsync();
        }

        public int SaveSaleOrder(SaleOrder SaleOrder, string Mode, LedgerAccount SelectedCustomer, LedgerAccount SelectedBankAccount, string ChequeNumber)
        {

            using (DbContextTransaction transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    if (SaleOrder.SaleOrderId < 1)
                    {
                        Context.SaleOrders.Add(SaleOrder);

                    }
                    else
                    {
                        Context.Entry(SaleOrder).State = EntityState.Modified;
                    }
                    var count = Context.SaveChanges();
                    SaveInLedger(SaleOrder, Mode, SelectedCustomer, SelectedBankAccount, ChequeNumber);
                    transaction.Commit();
                    return count;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;

                }
            }
        }

        private void SaveInLedger(SaleOrder SaleOrder, string Mode, LedgerAccount SelectedCustomer, LedgerAccount SelectedBankAccount, string ChequeNumber)
        {
            LedgerTransaction lt = new LedgerTransaction();
            lt.Date = DateTime.Now;
            lt.UserId = 1;
            lt.Description = "sales  ";
            Context.LedgerTransactions.Add(lt);
            var result = Context.SaveChanges();
            if (result > 0)
            {
                if (SaleOrder.Due <= 0)
                {
                    if (Mode == "IsCash")
                    {
                        LedgerTransactionDetail ltd = new LedgerTransactionDetail();
                        ltd.Amount = SaleOrder.AmountAfterDiscount;
                        ltd.LedgerTransactionId = lt.LedgerTransactionId;
                        ltd.LedgerAccountId = 1;
                        Context.LedgerTransactionDetails.Add(ltd);
                        Context.SaveChanges();

                        LedgerTransactionDetail ltd1 = new LedgerTransactionDetail();
                        ltd1.Amount = SaleOrder.AmountAfterDiscount;
                        ltd1.LedgerTransactionId = lt.LedgerTransactionId;
                        ltd1.LedgerAccountId = 2;
                        Context.LedgerTransactionDetails.Add(ltd1);
                        Context.SaveChanges();

                        LedgerGeneral lg = new LedgerGeneral();
                        lg.LedgerAccountId = 1;
                        lg.LedgerTransactionId = lt.LedgerTransactionId;
                        lg.Debit = SaleOrder.AmountAfterDiscount;
                        lg.Credit = 0;
                        lg.JournalEntryDate = lt.Date;
                        Context.Ledgergenerals.Add(lg);
                        Context.SaveChanges();

                        LedgerGeneral lg1 = new LedgerGeneral();
                        lg1.LedgerAccountId = 2;
                        lg1.LedgerTransactionId = lt.LedgerTransactionId;
                        lg1.Credit = SaleOrder.AmountAfterDiscount;
                        lg1.Debit = 0;
                        lg1.JournalEntryDate = lt.Date;
                        Context.Ledgergenerals.Add(lg1);
                        Context.SaveChanges();
                    }
                    else if (Mode == "IsCredit")
                    {
                        LedgerTransactionDetail ltd = new LedgerTransactionDetail();
                        ltd.Amount = SaleOrder.AmountAfterDiscount;
                        ltd.LedgerTransactionId = lt.LedgerTransactionId;
                        ltd.LedgerAccountId = 2;
                        Context.LedgerTransactionDetails.Add(ltd);
                        Context.SaveChanges();

                        LedgerTransactionDetail ltd1 = new LedgerTransactionDetail();
                        ltd1.Amount = SaleOrder.AmountAfterDiscount;
                        ltd1.LedgerTransactionId = lt.LedgerTransactionId;
                        ltd1.LedgerAccountId = SelectedCustomer.LedgerAccountId;
                        Context.LedgerTransactionDetails.Add(ltd1);
                        Context.SaveChanges();

                        LedgerGeneral lg = new LedgerGeneral();
                        lg.LedgerAccountId = 2;
                        lg.LedgerTransactionId = lt.LedgerTransactionId;
                        lg.Debit = 0;
                        lg.Credit = SaleOrder.AmountAfterDiscount;
                        lg.JournalEntryDate = lt.Date;
                        Context.Ledgergenerals.Add(lg);
                        Context.SaveChanges();

                        LedgerGeneral lg1 = new LedgerGeneral();
                        lg1.LedgerAccountId = SelectedCustomer.LedgerAccountId;
                        lg1.LedgerTransactionId = lt.LedgerTransactionId;
                        lg1.Credit = 0;
                        lg1.Debit = SaleOrder.AmountAfterDiscount;
                        lg1.JournalEntryDate = lt.Date;
                        Context.Ledgergenerals.Add(lg1);
                        Context.SaveChanges();
                    }
                    else if (Mode == "IsBank")
                    {
                        LedgerTransactionDetail ltd = new LedgerTransactionDetail();
                        ltd.Amount = SaleOrder.AmountAfterDiscount;
                        ltd.LedgerTransactionId = lt.LedgerTransactionId;
                        ltd.LedgerAccountId = 2;
                        Context.LedgerTransactionDetails.Add(ltd);
                        Context.SaveChanges();

                        LedgerTransactionDetail ltd1 = new LedgerTransactionDetail();
                        ltd1.Amount = SaleOrder.AmountPaid;
                        ltd1.LedgerTransactionId = lt.LedgerTransactionId;
                        ltd1.LedgerAccountId = SelectedBankAccount.LedgerAccountId;
                        Context.LedgerTransactionDetails.Add(ltd1);
                        Context.SaveChanges();

                        LedgerGeneral lg = new LedgerGeneral();
                        lg.LedgerAccountId = 2;
                        lg.LedgerTransactionId = lt.LedgerTransactionId;
                        lg.Debit = 0;
                        lg.Credit = SaleOrder.AmountAfterDiscount;
                        lg.JournalEntryDate = lt.Date;
                        Context.Ledgergenerals.Add(lg);
                        Context.SaveChanges();

                        LedgerGeneral lg1 = new LedgerGeneral();
                        lg1.LedgerAccountId =SelectedBankAccount.LedgerAccountId;
                        lg1.LedgerTransactionId = lt.LedgerTransactionId;
                        lg1.Credit = 0;
                        lg1.Debit = SaleOrder.AmountAfterDiscount;
                        lg1.ChequeNumber = ChequeNumber;
                        lg1.JournalEntryDate = lt.Date;
                        Context.Ledgergenerals.Add(lg1);
                        Context.SaveChanges();
                    }
                }
                else
                {
                    if (Mode == "IsCash")
                    {
                        LedgerTransactionDetail ltd = new LedgerTransactionDetail();
                        ltd.Amount = SaleOrder.AmountPaid;
                        ltd.LedgerTransactionId = lt.LedgerTransactionId;
                        ltd.LedgerAccountId = 1;
                        Context.LedgerTransactionDetails.Add(ltd);
                        Context.SaveChanges();

                        LedgerTransactionDetail ltd1 = new LedgerTransactionDetail();
                        ltd1.Amount = SaleOrder.AmountAfterDiscount;
                        ltd1.LedgerTransactionId = lt.LedgerTransactionId;
                        ltd1.LedgerAccountId = 2;
                        Context.LedgerTransactionDetails.Add(ltd1);
                        Context.SaveChanges();

                        LedgerTransactionDetail ltd2 = new LedgerTransactionDetail();
                        ltd2.Amount = SaleOrder.Due;
                        ltd2.LedgerTransactionId = lt.LedgerTransactionId;
                        ltd2.LedgerAccountId = SelectedCustomer.LedgerAccountId;
                        Context.LedgerTransactionDetails.Add(ltd2);
                        Context.SaveChanges();

                        LedgerGeneral lg = new LedgerGeneral();
                        lg.LedgerAccountId = 1;
                        lg.LedgerTransactionId = lt.LedgerTransactionId;
                        lg.Credit = 0;
                        lg.Debit = SaleOrder.AmountPaid;
                        lg.JournalEntryDate = lt.Date;
                        Context.Ledgergenerals.Add(lg);
                        Context.SaveChanges();

                        LedgerGeneral lg1 = new LedgerGeneral();
                        lg1.LedgerAccountId = 2;
                        lg1.LedgerTransactionId = lt.LedgerTransactionId;
                        lg1.Debit = 0;
                        lg1.Credit = SaleOrder.AmountAfterDiscount;
                        lg1.JournalEntryDate = lt.Date;
                        Context.Ledgergenerals.Add(lg1);
                        Context.SaveChanges();

                        LedgerGeneral lg2 = new LedgerGeneral();
                        lg2.LedgerAccountId = SelectedCustomer.LedgerAccountId;
                        lg2.LedgerTransactionId = lt.LedgerTransactionId;
                        lg2.Credit = 0;
                        lg2.Debit = SaleOrder.Due;
                        lg2.JournalEntryDate = lt.Date;
                        Context.Ledgergenerals.Add(lg2);
                        Context.SaveChanges();
                    }
                    else if (Mode == "IsCredit")
                    {
                        LedgerTransactionDetail ltd = new LedgerTransactionDetail();
                        ltd.Amount = SaleOrder.AmountAfterDiscount;
                        ltd.LedgerTransactionId = lt.LedgerTransactionId;
                        ltd.LedgerAccountId = 2;
                        Context.LedgerTransactionDetails.Add(ltd);
                        Context.SaveChanges();

                        if (SaleOrder.AmountPaid > 0)
                        {
                            LedgerTransactionDetail ltd1 = new LedgerTransactionDetail();
                            ltd1.Amount = SaleOrder.AmountPaid;
                            ltd1.LedgerTransactionId = lt.LedgerTransactionId;
                            ltd1.LedgerAccountId = 1;
                            Context.LedgerTransactionDetails.Add(ltd1);
                            Context.SaveChanges();
                        }

                        LedgerTransactionDetail ltd2 = new LedgerTransactionDetail();
                        ltd2.Amount = SaleOrder.Due;
                        ltd2.LedgerTransactionId = lt.LedgerTransactionId;
                        ltd2.LedgerAccountId = SelectedCustomer.LedgerAccountId;
                        Context.LedgerTransactionDetails.Add(ltd2);
                        Context.SaveChanges();

                        LedgerGeneral lg = new LedgerGeneral();
                        lg.LedgerAccountId = 2;
                        lg.LedgerTransactionId = lt.LedgerTransactionId;
                        lg.Debit = 0;
                        lg.Credit = SaleOrder.AmountAfterDiscount;
                        lg.JournalEntryDate = lt.Date;
                        Context.Ledgergenerals.Add(lg);
                        Context.SaveChanges();

                        if (SaleOrder.AmountPaid > 0)
                        {
                            LedgerGeneral lg1 = new LedgerGeneral();
                            lg1.LedgerAccountId = 1;
                            lg1.LedgerTransactionId = lt.LedgerTransactionId;
                            lg1.Credit = 0;
                            lg1.Debit = SaleOrder.AmountPaid;
                            lg1.JournalEntryDate = lt.Date;
                            Context.Ledgergenerals.Add(lg1);
                            Context.SaveChanges();
                        }

                        LedgerGeneral lg2 = new LedgerGeneral();
                        lg2.LedgerAccountId = SelectedCustomer.LedgerAccountId;
                        lg2.LedgerTransactionId = lt.LedgerTransactionId;
                        lg2.Credit = 0;
                        lg2.Debit = SaleOrder.Due;
                        lg2.JournalEntryDate = lt.Date;
                        Context.Ledgergenerals.Add(lg2);
                        Context.SaveChanges();
                    }
                    else if (Mode == "IsBank")
                    {
                        LedgerTransactionDetail ltd = new LedgerTransactionDetail();
                        ltd.Amount = SaleOrder.AmountAfterDiscount;
                        ltd.LedgerTransactionId = lt.LedgerTransactionId;
                        ltd.LedgerAccountId = 2;
                        Context.LedgerTransactionDetails.Add(ltd);
                        Context.SaveChanges();


                        LedgerTransactionDetail ltd1 = new LedgerTransactionDetail();
                        ltd1.Amount = SaleOrder.AmountPaid;
                        ltd1.LedgerTransactionId = lt.LedgerTransactionId;
                        ltd1.LedgerAccountId = SelectedBankAccount.LedgerAccountId;
                        Context.LedgerTransactionDetails.Add(ltd1);
                        Context.SaveChanges();

                        LedgerTransactionDetail ltd2 = new LedgerTransactionDetail();
                        ltd2.Amount = SaleOrder.Due;
                        ltd2.LedgerTransactionId = lt.LedgerTransactionId;
                        ltd2.LedgerAccountId = SelectedCustomer.LedgerAccountId;
                        Context.LedgerTransactionDetails.Add(ltd2);
                        Context.SaveChanges();




                        LedgerGeneral lg = new LedgerGeneral();
                        lg.LedgerAccountId = 2;
                        lg.LedgerTransactionId = lt.LedgerTransactionId;
                        lg.Credit = SaleOrder.AmountAfterDiscount;
                        lg.Debit = 0;
                        lg.JournalEntryDate = lt.Date;
                        Context.Ledgergenerals.Add(lg);
                        Context.SaveChanges();

                        LedgerGeneral lg1 = new LedgerGeneral();
                        lg1.LedgerAccountId = SelectedBankAccount.LedgerAccountId;
                        lg1.LedgerTransactionId = lt.LedgerTransactionId;
                        lg1.Credit = 0;
                        lg1.Debit = SaleOrder.AmountPaid;
                        lg1.ChequeNumber = ChequeNumber;                       
                        lg1.JournalEntryDate = lt.Date;
                        Context.Ledgergenerals.Add(lg1);
                        Context.SaveChanges();

                        LedgerGeneral lg2 = new LedgerGeneral();
                        lg2.LedgerAccountId = SelectedCustomer.LedgerAccountId;
                        lg2.LedgerTransactionId = lt.LedgerTransactionId;
                        lg2.Debit = SaleOrder.Due;
                        lg2.Credit = 0;
                        lg2.JournalEntryDate = lt.Date;
                        Context.Ledgergenerals.Add(lg2);
                        Context.SaveChanges();
                    }
                }
            }
        }

        public async Task<IEnumerable<SaleOrder>> GetAllSalesOrderAsync()
        {
            return await Context.SaleOrders.ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await Context.Customers.ToListAsync();
        }

        public async Task<IEnumerable<LedgerAccount>> GetAllCustomerAccountsAsync()
        {
            try
            {
                return await Context.LedgerAccounts.Where(a => a.ParentLedgerAccountId == 4).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<LedgerAccount>> GetAllBanksAsync()
        {
            try
            {
                return await Context.LedgerAccounts.Where(a => a.ParentLedgerAccountId == 7).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<Beverage>> GetAllPopularBeveragesAsync()
        {
            return await Context.Beverages.Where(a=>a.IsPopular).ToListAsync();
        }

        public async Task<IEnumerable<BeverageCategory>> GetAllBeverageCategoriesAsync()
        {
            return await Context.BeverageCategories.ToListAsync();
        }

        public async Task<List<Beverage>> GetAllBeveragesByCategoryAsync(int categoryid)
        {
            return await Context.Beverages.Where(a=>a.BeverageCategoryId==categoryid).ToListAsync();
        }
    }
}
