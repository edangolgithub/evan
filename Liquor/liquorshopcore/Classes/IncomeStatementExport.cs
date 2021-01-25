using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquorShop.Classes
{
    public class IncomeStatementExport
    {
        public string AccountType { get; set; }
        public string Account { get; set; }
        public string Balance { get; set; }
        public string TotalHeading { get; set; }
        public string Total { get; set; }
        public string Date { get; set; }
    }
}
