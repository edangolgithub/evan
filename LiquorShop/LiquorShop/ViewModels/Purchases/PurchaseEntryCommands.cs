using Rms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TescoWineShopSql;

namespace LiquorShop.ViewModels.Purchases
{
    public partial class PurchaseEntryViewModel : ViewModelBase
    {

        #region RemovePurchaseCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="RemovePurchaseCommand" />
        /// </summary>
        private RelayCommand _RemovePurchaseCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand RemovePurchaseCommand
        {
            get
            {
                if (_RemovePurchaseCommand == null)
                { _RemovePurchaseCommand = new RelayCommand(RemovePurchaseCommand_Execute, RemovePurchaseCommand_CanExecute); }

                return _RemovePurchaseCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="RemovePurchaseCommand" />
        /// </summary>
        private void RemovePurchaseCommand_Execute(object obj)
        {
            Purchase currentpurchase = (Purchase)obj;
            
            if(currentpurchase!=null)
            {                
                SelectedPurchaseVm.Purchases.Remove(currentpurchase);
            }
        }

        /// <summary>
        /// Determines if <see cref="RemovePurchaseCommand" /> is allowed to execute
        /// </summary>
        private Boolean RemovePurchaseCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion


        #region SavePurchaseCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="SavePurchaseCommand" />
        /// </summary>
        private RelayCommand _SavePurchaseCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand SavePurchaseCommand
        {
            get
            {
                if (_SavePurchaseCommand == null)
                { _SavePurchaseCommand = new RelayCommand(SavePurchaseCommand_Execute, SavePurchaseCommand_CanExecute); }

                return _SavePurchaseCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="SavePurchaseCommand" />
        /// </summary>
        private void SavePurchaseCommand_Execute(object obj)
        {
            SavePurchaseFunction(obj);
        }

        /// <summary>
        /// Determines if <see cref="SavePurchaseCommand" /> is allowed to execute
        /// </summary>
        private Boolean SavePurchaseCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion

        private void SavePurchaseFunction(object obj)
        {
            try
            {               
                if (NewPurchase != null)
                {
                    if (NewPurchase.Beverage == null)
                    {
                        throw new Exception("No Beverage is selected");
                    }
                    if (NewPurchase.Beverage.Name.Length < 0)
                    {
                        throw new Exception("No beverage is selected");
                    }
                    if (!(NewPurchase.Quantity > 0))
                    {
                        throw new Exception("Check Quantity");
                    }
                    if (!(NewPurchase.Rate > 0))
                    {
                        throw new Exception("Check Rate");
                    }
                    if (NewPurchase.Metric == null)
                    {
                        NewPurchase.Metric = TescoWineShopSql.Metrics.Bottle;
                    }
                    var unitprice = NewPurchase.UnitPrice;
                    var quantity = NewPurchase.Quantity;


                    var totNoVat = (unitprice * quantity) - (NewPurchase.Discount ?? 0);

                    NewPurchase.PurchaseDate = SelectedPurchaseVm.Date;
                 
                    if (NewPurchase.PurchaseDate.Year < 10)
                    {
                        NewPurchase.PurchaseDate = DateTime.Today;
                    }
                   
                    if (SelectedPurchaseVm.Supplier == null)
                    {
                        MessageBox.Show("Select Supplier");
                        return;
                    }
                    PurchaseSubTotal += NewPurchase.LineTotalAmount;

                    SelectedPurchaseVm.Purchases.Add(NewPurchase);

                    //SelectedMetric = null;
                    NewPurchase = new Purchase();
                    RateString = "";
                    //PurchaseSubTotal = 0;
                    UnitQuantity = 0;
                    IsDirty = false;
                }
            }
            catch (Exception ex)
            {
                // throw ex;
                throw ex;
            }

        }


        #region SavePurchasesCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="SavePurchasesCommand" />
        /// </summary>
        private RelayCommand _SavePurchasesCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand SavePurchasesCommand
        {
            get
            {
                if (_SavePurchasesCommand == null)
                { _SavePurchasesCommand = new RelayCommand(SavePurchasesCommand_Execute, SavePurchasesCommand_CanExecute); }

                return _SavePurchasesCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="SavePurchasesCommand" />
        /// </summary>
        private void SavePurchasesCommand_Execute(object obj)
        {
            if(PurchaseSubTotal<0)
            {
                MessageBox.Show("Total Amount Not Entered");
                return;
            }
            voice = new Invoice();
            try
            {
                
                var confirm = MessageBox.Show("Are You Sure You Want to Save This Invoice?\n Please Recheck Your Invoice For Any Mistakes.","Confirmation",MessageBoxButton.YesNoCancel);
                if(confirm==MessageBoxResult.No||confirm==MessageBoxResult.Cancel)
                {
                    return;
                }
               
                if (IsDirty)
                {
                    MessageBox.Show("There may be some transactions left  to add");
                    return;
                }
                var data = SelectedPurchaseVm.Purchases;
                if (data.Count < 1)
                {
                    return;
                }
                decimal taxable = 0;
                foreach (var item in data)
                {
                    taxable += item.LineTotalAmount;
                }
                SelectedPurchaseVm.TaxableAmount = taxable;
                if (SelectedPurchaseVm.Total != PurchaseSubTotal)
                {
                    SelectedPurchaseVm.Total = PurchaseSubTotal;
                }
              
                voice.Date = SelectedPurchaseVm.Date;
                voice.InvoiceNumber = SelectedPurchaseVm.InvoiceNumber;
                voice.Supplier = SelectedPurchaseVm.Supplier;
                SelectedPurchaseVm.Total -= SelectedPurchaseVm.Discount;
                voice.Vat = SelectedPurchaseVm.VatAmount;
                SelectedPurchaseVm.Total += SelectedPurchaseVm.VatAmount;

                voice.TaxableAmount = SelectedPurchaseVm.TaxableAmount;
                voice.Discount = SelectedPurchaseVm.Discount;
                voice.Due = SelectedPurchaseVm.Due;
                voice.AmountPaid = SelectedPurchaseVm.AmountPaid;
                voice.Total = PurchaseSubTotal;
                voice.Purchases = data;
               
                var save = Service.SavePurchases(voice);
               
                    if(PostPurchaseToLedger)
                    {
                    if (save > 0)
                    {
                        var saccount = AccountService.GetLedgerAccountByName(voice.Supplier.SupplierName);                      
                        LedgerTransaction lt = new LedgerTransaction();
                        lt.Date = DateTime.Now;
                        lt.UserId = 1;
                        if (string.IsNullOrEmpty(PurchaseNarration))
                        {
                            lt.Description = "purchases from " + saccount.AccountName;
                        }
                        else
                        {
                            lt.Description = PurchaseNarration;
                        }
                       
                        var result = AccountService.SaveLedgerTransaction(lt);
                        if (result > 0)
                        {
                            ResolvePaymentMode(voice,lt,saccount);
                           
                        }
                    }
                }

               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SelectedPurchaseVm = new LiquorShop.Classes.PurchaseVm();
                RateString = "";
                PurchaseSubTotal = 0;
                UnitQuantity = 0;
                voice = new Invoice();
                PurchaseSubTotal = 0;
               
            }
        }

        private void ResolvePaymentMode(Invoice voice, LedgerTransaction lt,LedgerAccount account)
        {
            if (SelectedPurchaseVm.Due <= 0)
            {
                if (IsCash)
                {
                    LedgerTransactionDetail ltd = new LedgerTransactionDetail();
                    ltd.Amount = voice.Total;
                    ltd.LedgerTransactionId = lt.LedgerTransactionId;
                    ltd.LedgerAccountId = 1;

                    AccountService.SaveLedgerTransactionDetail(ltd);
                    LedgerTransactionDetail ltd1 = new LedgerTransactionDetail();
                    ltd1.Amount = voice.Total;
                    ltd1.LedgerTransactionId = lt.LedgerTransactionId;
                    ltd1.LedgerAccountId = 3;
                    AccountService.SaveLedgerTransactionDetail(ltd1);

                    LedgerGeneral lg = new LedgerGeneral();
                    lg.LedgerAccountId = 1;
                    lg.LedgerTransactionId = lt.LedgerTransactionId;
                    lg.Credit = voice.Total;
                    lg.Debit = 0;
                    lg.JournalEntryDate = lt.Date;
                    AccountService.SaveLedgerGeneral(lg);

                    LedgerGeneral lg1 = new LedgerGeneral();
                    lg1.LedgerAccountId = 3;
                    lg1.LedgerTransactionId = lt.LedgerTransactionId;
                    lg1.Debit = voice.Total;
                    lg1.Credit = 0;
                    lg1.JournalEntryDate = lt.Date;
                    AccountService.SaveLedgerGeneral(lg1);
                }
                else if (IsCredit)
                {
                    LedgerTransactionDetail ltd = new LedgerTransactionDetail();
                    ltd.Amount = voice.Total;
                    ltd.LedgerTransactionId = lt.LedgerTransactionId;
                    ltd.LedgerAccountId = 3;
                    AccountService.SaveLedgerTransactionDetail(ltd);

                    LedgerTransactionDetail ltd1 = new LedgerTransactionDetail();
                    ltd1.Amount = voice.Total;
                    ltd1.LedgerTransactionId = lt.LedgerTransactionId;
                    ltd1.LedgerAccountId = account.LedgerAccountId;
                    AccountService.SaveLedgerTransactionDetail(ltd1);

                    LedgerGeneral lg = new LedgerGeneral();
                    lg.LedgerAccountId = 3;
                    lg.LedgerTransactionId = lt.LedgerTransactionId;
                    lg.Debit = voice.Total;
                    lg.Credit = 0;
                    lg.JournalEntryDate = lt.Date;
                    AccountService.SaveLedgerGeneral(lg);

                    LedgerGeneral lg1 = new LedgerGeneral();
                    lg1.LedgerAccountId = account.LedgerAccountId;
                    lg1.LedgerTransactionId = lt.LedgerTransactionId;
                    lg1.Credit = voice.Total;
                    lg1.Debit = 0;
                    lg1.JournalEntryDate = lt.Date;
                    AccountService.SaveLedgerGeneral(lg1);
                }
                else if (IsBank)
                {
                    LedgerTransactionDetail ltd = new LedgerTransactionDetail();
                    ltd.Amount = voice.Total;
                    ltd.LedgerTransactionId = lt.LedgerTransactionId;
                    ltd.LedgerAccountId = 3;
                    AccountService.SaveLedgerTransactionDetail(ltd);

                    LedgerTransactionDetail ltd1 = new LedgerTransactionDetail();
                    ltd1.Amount = voice.Total;
                    ltd1.LedgerTransactionId = lt.LedgerTransactionId;
                    ltd1.LedgerAccountId = SelectedBankAccount.LedgerAccountId;
                    AccountService.SaveLedgerTransactionDetail(ltd1);

                    LedgerGeneral lg = new LedgerGeneral();
                    lg.LedgerAccountId = 3;
                    lg.LedgerTransactionId = lt.LedgerTransactionId;
                    lg.Debit = voice.Total;
                    lg.Credit = 0;
                    lg.JournalEntryDate = lt.Date;
                    AccountService.SaveLedgerGeneral(lg);

                    LedgerGeneral lg1 = new LedgerGeneral();
                    lg1.LedgerAccountId = SelectedBankAccount.LedgerAccountId;
                    lg1.LedgerTransactionId = lt.LedgerTransactionId;
                    lg1.Credit = voice.Total;
                    lg1.Debit = 0;
                    lg1.JournalEntryDate = lt.Date;
                    AccountService.SaveLedgerGeneral(lg1);
                }
            }
            else
            {
                if (IsCash)
                {
                    LedgerTransactionDetail ltd = new LedgerTransactionDetail();
                    ltd.Amount = voice.AmountPaid;
                    ltd.LedgerTransactionId = lt.LedgerTransactionId;
                    ltd.LedgerAccountId = 1;
                    AccountService.SaveLedgerTransactionDetail(ltd);

                    LedgerTransactionDetail ltd1 = new LedgerTransactionDetail();
                    ltd1.Amount = voice.Total;
                    ltd1.LedgerTransactionId = lt.LedgerTransactionId;
                    ltd1.LedgerAccountId = 3;
                    AccountService.SaveLedgerTransactionDetail(ltd1);

                    LedgerTransactionDetail ltd2 = new LedgerTransactionDetail();
                    ltd2.Amount = voice.Due;
                    ltd2.LedgerTransactionId = lt.LedgerTransactionId;
                    ltd2.LedgerAccountId = account.LedgerAccountId;
                    AccountService.SaveLedgerTransactionDetail(ltd2);

                    LedgerGeneral lg = new LedgerGeneral();
                    lg.LedgerAccountId = 1;
                    lg.LedgerTransactionId = lt.LedgerTransactionId;
                    lg.Credit = voice.AmountPaid;
                    lg.Debit = 0;
                    lg.JournalEntryDate = lt.Date;
                    AccountService.SaveLedgerGeneral(lg);

                    LedgerGeneral lg1 = new LedgerGeneral();
                    lg1.LedgerAccountId = 3;
                    lg1.LedgerTransactionId = lt.LedgerTransactionId;
                    lg1.Debit = voice.Total;
                    lg1.Credit = 0;
                    lg1.JournalEntryDate = lt.Date;
                    AccountService.SaveLedgerGeneral(lg1);

                    LedgerGeneral lg2 = new LedgerGeneral();
                    lg2.LedgerAccountId = account.LedgerAccountId;
                    lg2.LedgerTransactionId = lt.LedgerTransactionId;
                    lg2.Debit = 0;
                    lg2.Credit = voice.Due;
                    lg2.JournalEntryDate = lt.Date;
                    AccountService.SaveLedgerGeneral(lg2);
                }
                //else if (IsCredit)
                //{
                //    LedgerTransactionDetail ltd = new LedgerTransactionDetail();
                //    ltd.Amount = voice.Total;
                //    ltd.LedgerTransactionId = lt.LedgerTransactionId;
                //    ltd.LedgerAccountId = 3;
                //    AccountService.SaveLedgerTransactionDetail(ltd);

                //    LedgerTransactionDetail ltd1 = new LedgerTransactionDetail();
                //    ltd1.Amount = voice.Total;
                //    ltd1.LedgerTransactionId = lt.LedgerTransactionId;
                //    ltd1.LedgerAccountId = account.LedgerAccountId;
                //    AccountService.SaveLedgerTransactionDetail(ltd1);

                //    LedgerGeneral lg = new LedgerGeneral();
                //    lg.LedgerAccountId = 3;
                //    lg.LedgerTransactionId = lt.LedgerTransactionId;
                //    lg.Debit = voice.Total;
                //    lg.Credit = 0;
                //    lg.JournalEntryDate = lt.Date;
                //    AccountService.SaveLedgerGeneral(lg);

                //    LedgerGeneral lg1 = new LedgerGeneral();
                //    lg1.LedgerAccountId = account.LedgerAccountId;
                //    lg1.LedgerTransactionId = lt.LedgerTransactionId;
                //    lg1.Credit = voice.Total;
                //    lg1.Debit = 0;
                //    lg1.JournalEntryDate = lt.Date;
                //    AccountService.SaveLedgerGeneral(lg1);
                //}
                else if (IsBank)
                {
                    LedgerTransactionDetail ltd = new LedgerTransactionDetail();
                    ltd.Amount = voice.Total;
                    ltd.LedgerTransactionId = lt.LedgerTransactionId;
                    ltd.LedgerAccountId = 3;
                    AccountService.SaveLedgerTransactionDetail(ltd);

                    LedgerTransactionDetail ltd1 = new LedgerTransactionDetail();
                    ltd1.Amount = voice.AmountPaid;
                    ltd1.LedgerTransactionId = lt.LedgerTransactionId;
                    ltd1.LedgerAccountId = SelectedBankAccount.LedgerAccountId;
                    AccountService.SaveLedgerTransactionDetail(ltd1);

                    LedgerTransactionDetail ltd2 = new LedgerTransactionDetail();
                    ltd2.Amount = voice.Due;
                    ltd2.LedgerTransactionId = lt.LedgerTransactionId;
                    ltd2.LedgerAccountId = account.LedgerAccountId;
                    AccountService.SaveLedgerTransactionDetail(ltd2);

                   

                    LedgerGeneral lg = new LedgerGeneral();
                    lg.LedgerAccountId = 3;
                    lg.LedgerTransactionId = lt.LedgerTransactionId;
                    lg.Debit = voice.Total;
                    lg.Credit = 0;
                    lg.JournalEntryDate = lt.Date;
                    AccountService.SaveLedgerGeneral(lg);

                    LedgerGeneral lg1 = new LedgerGeneral();
                    lg1.LedgerAccountId = SelectedBankAccount.LedgerAccountId;
                    lg1.LedgerTransactionId = lt.LedgerTransactionId;
                    lg1.Credit = voice.AmountPaid;
                    lg1.Debit = 0;
                    lg1.JournalEntryDate = lt.Date;
                    AccountService.SaveLedgerGeneral(lg1);

                    LedgerGeneral lg2 = new LedgerGeneral();
                    lg2.LedgerAccountId = account.LedgerAccountId;
                    lg2.LedgerTransactionId = lt.LedgerTransactionId;
                    lg2.Credit = voice.Due;
                    lg2.Debit = 0;
                    lg2.JournalEntryDate = lt.Date;
                    AccountService.SaveLedgerGeneral(lg2);
                }
            }
        }

        /// <summary>
        /// Determines if <see cref="SavePurchasesCommand" /> is allowed to execute
        /// </summary>
        private Boolean SavePurchasesCommand_CanExecute(object obj)
        {
            if (SelectedPurchaseVm != null)
            {
                return SelectedPurchaseVm.Purchases.Count > 0;
            }
            return false;
        }

        #endregion


      
      


     
        #region CancelPurchaseCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="CancelPurchaseCommand" />
        /// </summary>
        private RelayCommand _CancelPurchaseCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand CancelPurchaseCommand
        {
            get
            {
                if (_CancelPurchaseCommand == null)
                { _CancelPurchaseCommand = new RelayCommand(CancelPurchaseCommand_Execute, CancelPurchaseCommand_CanExecute); }

                return _CancelPurchaseCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="CancelPurchaseCommand" />
        /// </summary>
        private void CancelPurchaseCommand_Execute(object obj)
        {
            NewPurchase = new Purchase();
            RateString = "";
            UnitQuantity = 0;
            IsDirty = false;
        }

        /// <summary>
        /// Determines if <see cref="CancelPurchaseCommand" /> is allowed to execute
        /// </summary>
        private Boolean CancelPurchaseCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion
    }
}
