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
    public class FiscalYear
    {
        public int FiscalYearId { get; set; }
        public string FiscalYearDescription { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public ICollection<LedgerTransaction> LedgerTransactions { get; set; }
    }
    public class LedgerTransaction
    {
        public int LedgerTransactionId { get; set; }
        [MaxLength(100)]
        public string VoucherNumber { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public virtual ICollection<LedgerTransactionDetail> LedgerTransactionDetails { get; set; }
        public virtual ICollection<LedgerGeneral> LedgerGenerals { get; set; }
        public User User { get; set; }
        public int? UserId { get; set; }
        public int? FiscalYearId { get; set; }
        public FiscalYear FiscalYear { get; set; }
    }

    public class LedgerAccount
    {
        // public int AccountNo { get; set; }
        public int LedgerAccountId { get; set; }
       
        [MaxLength(50)]
        public string AccountName { get; set; }
        public string Description { get; set; }
        public int AccountClassId { get; set; }
        public AccountClass AccountClass { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        [ForeignKey("parentLedgerAccount")]
        public int? ParentLedgerAccountId { get; set; }
        public LedgerAccount parentLedgerAccount { get; set; }
        public bool IsSystemLedger { get; set; }
        public int SortOrder { get; set; }
        public bool ShowInChart { get; set; }

        public int? AccountGroupId { get; set; }

        //   public AccountGroup AccountGroup { get; set; }
    }
    public class AccountGroup
    {
        public int AccountGroupId { get; set; }
        public string AccountGroupName { get; set; }
        public int AccountClassId { get; set; }
        // public AccountClass AccountClass { get; set; }
        public ICollection<LedgerAccount> LedgerAccounts { get; set; }
    }
    public class AccountClass
    {
        public int AccountClassId { get; set; }
      
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSystemAccount { get; set; }
        public ICollection<LedgerAccount> LedgerAccounts { get; set; }
        //public ICollection<AccountGroup> AccountGroups { get; set; }
    }
    public class LedgerTransactionDetail
    {
        public int LedgerTransactionDetailId { get; set; }
        public int LedgerTransactionId { get; set; }
        public LedgerTransaction LedgerTransaction { get; set; }
        public int LedgerAccountId { get; set; }
        public LedgerAccount LedgerAccount { get; set; }
        public string Seq { get; set; }
        public decimal Amount { get; set; }

    }

    public class LedgerGeneral
    {
        public int LedgergeneralId { get; set; }
        public LedgerTransaction LedgerTransaction { get; set; }
        public int LedgerTransactionId { get; set; }
        public DateTime JournalEntryDate { get; set; }
        public LedgerAccount LedgerAccount { get; set; }
        public int LedgerAccountId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public string BankName { get; set; }
        public string ChequeNumber { get; set; }

    }
    public class LedgerAccountBalance
    {
        public int LedgerAccountBalanceId { get; set; }
        public LedgerAccount LedgerAccount { get; set; }
        public int LedgerAccountId { get; set; }
        public int PeriodYear { get; set; }
        public decimal BeginingBalance { get; set; }
        public decimal Saldo1 { get; set; }
        public decimal Saldo2 { get; set; }
        public decimal Saldo3 { get; set; }
        public decimal Saldo4 { get; set; }
        public decimal Saldo5 { get; set; }
        public decimal Saldo6 { get; set; }
        public decimal Saldo7 { get; set; }
        public decimal Saldo8 { get; set; }
        public decimal Saldo9 { get; set; }
        public decimal Saldo10 { get; set; }
        public decimal Saldo11 { get; set; }
        public decimal Saldo12 { get; set; }

    }
    public class Company : ModelBase
    {
        public int CompanyId { get; set; }
        [MaxLength(100)]
      
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyPanNumber { get; set; }
        public string CompanyType { get; set; }
        bool _IsActive;
        public bool IsActive
        {
            get
            {
                return _IsActive;
            }
            set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;
                    notify("IsActive");
                }
            }
        }

        public int? FiscalYearId { get; set; }
        public FiscalYear FiscalYear { get; set; }
        public string CompanyPassword { get; set; }
    }
}
