using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TescoWineShopSql
{
    public enum Drinktype { Popular,Whisky, Beer,SoftDrink,Wine,Liquor,Brandy,Vodka,Rum,Gin }
    public class Beverage
    {
        public int BeverageId { get; set; }
       public string Name { get; set; }
        public Drinktype? DrinkType { get; set; }
        public int? Volume { get; set; }
        public string Country { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }
        public bool ShowInChart { get; set; }
        public bool IsPopular { get; set; }
        public int BeverageCategoryId { get; set; }
        public BeverageCategory BeverageCategory { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
    public class BeverageCategory
    {      
        public int BeverageCategoryId { get; set; }
        public string BeverageCategoryName { get; set; }
        public ICollection<Beverage> Beverages { get; set; }
    }
}
