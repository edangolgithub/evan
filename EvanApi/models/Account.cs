
using System.Collections.Generic;

namespace EvanApi.models
{
    public class Accounttype    {
        public string accounttypeid { get; set; } 
        public string accounttype { get; set; } 
        public string id { get; set; } 
    }

    public class Account    {
        public string name { get; set; } 
        public string address { get; set; } 
         public string email { get; set; } 
        public string phone { get; set; } 
        public string accountid { get; set; } 
        public List<string> accounttype { get; set; } 
        public string id { get; set; } 
    }

    public class Transaction    {
        public string accountid { get; set; } 
        public string date { get; set; } 
        public string accounttypeid { get; set; } 
        public string amount { get; set; } 
        public string type { get; set; } 
        public string entry { get; set; } 
        public string id { get; set; } 
    }
}

   