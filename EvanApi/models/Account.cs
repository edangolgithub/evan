
using System.Collections.Generic;

namespace EvanApi.models
{
    public class AccountType
    {
        public string accounttypeid { get; set; }
        public string accounttype { get; set; }
        public string id { get; set; }
    }

    public class Account
    {
        public string accountname { get; set; }
        public string citizenshipno { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string phone1 { get; set; }
        public string accountid { get; set; }
        public string id { get; set; }        
        public string created { get; set; }      
        public string isenabled { get; set; }
     

    }

    public class Transaction
    {
        public string accountid { get; set; }
        public string created { get; set; }
        public string accounttypeid { get; set; }
        public string type { get; set; }
        public string entry { get; set; }
        public double amount { get; set; }
        public double interest { get; set; }
        public double balance { get; set; }
        public string id { get; set; }
        public string accounttype { get; set; }
        public string islatest { get; set; }
        public string accountname { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string isenabled { get; set; }        
 
    }
}

