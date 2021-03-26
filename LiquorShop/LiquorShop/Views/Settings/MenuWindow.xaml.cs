using LiquorShopService.Services;
using Rms;
using Rms.InfraStructures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TescoWineShopSql;
using Unity;
using WpfApp1;

namespace LiquorShop.Views.Settings
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        DataTable _d;
        IAdministrationService Service;
        public DataTable d
        {
            get
            {
                return _d;
            }
            set
            {
                if (_d != value)
                {
                    _d = value;
                    notify("d");
                }
            }
        }

        public MenuWindow(DataTable d,IAdministrationService Service)
        {
            
           
           
            InitializeComponent();
            DataContext = this;

            this.Service = Service;
            App.Current.Dispatcher.Invoke(async () =>
            {
                IsDataLoaded = false;
                IsDataNotLoaded = true;
               await Task.Run(() => ReadExcelSaveMenu(d));
            });
           
            //  Task.Run(() => ReadExcelSaveMenu(d)).ContinueWith( a => IsDataNotLoaded = false, IsDataLoaded = true });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void notify(string p)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }
  
        bool _IsDataLoaded;
        public bool IsDataLoaded
        {
            get
            {
                return _IsDataLoaded;
            }
            set
            {
                if (_IsDataLoaded != value)
                {
                    _IsDataLoaded = value;
                    notify("IsDataLoaded");
                }
            }
        }

        bool _IsDataNotLoaded;
        public bool IsDataNotLoaded
        {
            get
            {
                return _IsDataNotLoaded;
            }
            set
            {
                if (_IsDataNotLoaded != value)
                {
                    _IsDataNotLoaded = value;
                    notify("IsDataNotLoaded");
                }
            }
        }


        private async Task ReadExcelSaveMenu(DataTable d)
        {
           try
            {
                List<Rms.Classes.MenuItem> MenuItems = new List<Rms.Classes.MenuItem>();
                foreach (var item in d.AsEnumerable())
                {
                    var itemflag = item.IsNull("item");
                    var typeflag = item.IsNull("type");
                    var volflag = item.IsNull("volume");
                    var priceflag = item.IsNull("price");
                    var popflag = item.IsNull("popular");
                    var imflag = item.IsNull("image");
                    var counflag = item.IsNull("country");
                    var chartflag = item.IsNull("chart");


                    //if (typeflag || itemflag)
                    //{
                    //    // throw new Exception("Empty menu or category detected please check your excel file");
                    //    continue;
                    //}
                    string type = Convert.ToString(item["Type"]);
                    string bev = Convert.ToString(item["item"]);
                    string pr = Convert.ToString(item["price"]).Trim();
                    string vol = Convert.ToString(item["Volume"]);
                    string popular = Convert.ToString(item["popular"]);
                    string im = Convert.ToString(item["price"]).Trim();
                    string country = Convert.ToString(item["Country"]);
                    string Chart = Convert.ToString(item["Chart"]).Trim();
                    decimal price = 0;

                    
                    
                    if (!itemflag)
                         {
                        TextInfo t = new CultureInfo("en-US", false).TextInfo;
                        type = t.ToTitleCase(type);
                        bev = t.ToTitleCase(bev);

                        Rms.Classes.MenuItem m = new Rms.Classes.MenuItem();
                        if (imflag)
                        {
                            im = "jack.jpg";
                        }
                        m.beverage = bev;
                        if (!typeflag)
                        {
                            m.MenuCategoryName = type;
                        }
                        else
                        {
                            m.MenuCategoryName = "Others";
                        }
                        if (!priceflag)
                        {
                            decimal.TryParse(pr,out price);
                            m.Price = price;
                        }
                        else
                        {
                            m.Price = 0;
                        }
                        m.Country = country;
                        m.Image = im;
                        if (!string.IsNullOrEmpty(Chart) || Chart == "1")
                        {
                            m.ShowInChart = true;
                        }
                        else
                        {
                            m.ShowInChart = false;
                        }
                        if (!string.IsNullOrEmpty(popular) || popular == "1")
                        {
                            m.IsPopular = true;
                        }
                        else
                        {
                            m.IsPopular = false;
                        }
                        if (!volflag)
                        {
                            m.Volume = vol;
                        }
                        else
                        {
                            m.Volume = "0";
                        }

                        MenuItems.Add(m);
                    }
                }
                int ret = await Service.SaveBeverageAsync(MenuItems);              

             
                App.Current.Dispatcher.Invoke(() =>
                {
                    IsDataLoaded = true;// not working
                    IsDataNotLoaded = false;// not working
                    yes.Visibility = Visibility.Visible;
                    no.Visibility = Visibility.Hidden;
                    dg.ItemsSource = d.DefaultView;
                });
             

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private static void ReadWordDoc()
        //{
        //    Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
        //    // File Path

        //    string strFilePath = "E:\\new menu beverage11.docx";

        //    // Create obj filename to pass it as paremeter in open 
        //    object objFile = strFilePath;
        //    object objNull = System.Reflection.Missing.Value;

        //    object objReadOnly = true;//Open Document
        //    Microsoft.Office.Interop.Word.Document Doc = wordApp.Documents.Open(ref objFile, ref objNull, ref objReadOnly, ref objNull, ref objNull, ref objNull, ref objNull, ref objNull, ref objNull, ref objNull, ref objNull, ref objNull, ref objNull, ref objNull, ref objNull, ref objNull);

        //    try
        //    {
        //        // To read each line consider each line as paragraph Docuemnt

        //        string menucategoryname = "";
        //        List<string> menus = new List<string>();
        //        List<string> headers = new List<string>();

        //        Dictionary<string, decimal> menuitem = new Dictionary<string, decimal>();
        //        Dictionary<string, Dictionary<string, decimal>> main = new Dictionary<string, Dictionary<string, decimal>>();

        //        List<MenuCategory> MenuCategories = new List<MenuCategory>();
        //        List<Rms.Classes.MenuItem> MenuItems = new List<Rms.Classes.MenuItem>();
        //        int MenuCategoryid = 0;
        //        foreach (Microsoft.Office.Interop.Word.Paragraph objParagraph in Doc.Paragraphs)
        //        {
        //            string pricestring = "";
        //            decimal price = 0;
        //            var item = objParagraph.Range.Text;
        //            // Console.WriteLine(objParagraph.Range.Bold);
        //            if (objParagraph.Range.Bold == -1 && !string.IsNullOrWhiteSpace(objParagraph.Range.Text))
        //            {
        //                MenuCategoryid++;
        //                var category = new string(item.Where(a => char.IsLetter(a)).ToArray());
        //                menucategoryname = category;
        //                //headers.Add(objParagraph.Range.Text);
        //                ////Console.WriteLine(objParagraph.Range.Text);
        //                MenuCategory mc = new MenuCategory();
        //                mc.MenuCategoryId = MenuCategoryid;
        //                mc.MenyCategoryName = category;
        //                MenuCategories.Add(mc);
        //            }
        //            else
        //            {

        //                if (string.IsNullOrWhiteSpace(item))
        //                {
        //                    continue;
        //                }
        //                if (item.ToLower().Contains("8848") || item.Contains("vat 69") || item.ToLower().Contains("b52"))
        //                {

        //                }
        //                pricestring = Regex.Match(item, @"\d+").Value;
        //                var menu = new string(item.Where(a => char.IsLetter(a)).ToArray());
        //                if (decimal.TryParse(pricestring, out price) && menu.Length < 1)
        //                {
        //                    throw new Exception("In valid file " + "\nafter ");
        //                }
        //                //menuitem.Add(menu, price);
        //                //main.Add(headers.Last(),menuitem);
        //                Rms.Classes.MenuItem m = new Rms.Classes.MenuItem();
        //                m.MenuCategoryId = MenuCategoryid;
        //                m.MenuItemName = menu;
        //                m.MenuCategoryName = menucategoryname;
        //                m.Price = price;
        //                MenuItems.Add(m);
        //            }

        //        }
        //        //   string previousmenu = "";
        //        // int j = 0;
        //        //foreach (var item in menus)
        //        //{

        //        //    if (string.IsNullOrWhiteSpace(item))
        //        //    {
        //        //        continue;
        //        //    }
        //        //    pricestring = Regex.Match(item, @"\d+").Value;
        //        //    var v = new string(item.Where(a => char.IsLetter(a)).ToArray());
        //        //    if (decimal.TryParse(pricestring, out price) && v.Length < 1)
        //        //    {
        //        //        throw new Exception("In valid file " + "\nafter " + previousmenu);
        //        //    }
        //        //    menuitem.Add(v, price);
        //        //    previousmenu = item;
        //        //    main.Add(headers[j], menuitem);
        //        //    j++;
        //        //}
        //        //var v1 = main;

        //        List<MenuCategory> mainmenu = new List<MenuCategory>();
        //        foreach (var item in MenuItems.GroupBy(a => a.MenuCategoryName).Distinct())
        //        {


        //            var cat = item.Key;
        //            Service.Implementations.DbService.Context = new RmsContext();
        //            var flag = Service.Implementations.DbService.Context.ItemCategories.Where(a => a.CategoryName == cat).FirstOrDefault();
        //            if (flag == null)
        //            {
        //                ItemCategory itemcat = new ItemCategory();
        //                itemcat.CategoryName = cat;
        //                itemcat.CategoryImage = "nocat.jpg";

        //                Service.Implementations.DbService.Context.ItemCategories.Add(itemcat);
        //                Service.Implementations.DbService.Context.SaveChanges();
        //                flag = itemcat;
        //                MessageBox.Show(itemcat.ItemCategoryId.ToString());
        //            }

        //            foreach (var i in item)
        //            {
        //                var menuname = i.MenuItemName;
        //                var price = i.Price;
        //                var flag1 = Service.Implementations.DbService.Context.Items.Where(a => a.ItemName == menuname).FirstOrDefault();
        //                Item thisitem = new Item();
        //                thisitem.ItemName = menuname;
        //                thisitem.Price = price;
        //                thisitem.ItemCategoryId = flag.ItemCategoryId;
        //                if (flag1 == null)
        //                {

        //                    thisitem.ItemImage = "nopic1.jpg";
        //                    Service.Implementations.DbService.Context.Items.Add(thisitem);
        //                }
        //                else
        //                {

        //                    flag1.ItemImage = thisitem.ItemName;
        //                    flag1.Price = thisitem.Price;

        //                    Service.Implementations.DbService.Context.Entry(flag1).State = EntityState.Modified;
        //                }
        //                Service.Implementations.DbService.Context.SaveChanges();


        //            }


        //        }

        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        SqlException innerException = ex.InnerException.InnerException as SqlException;
        //        if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
        //        {
        //            MessageBox.Show("Menu already exists. please check your menu file and remove duplicate entries\nOr Delete old menus");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex is DbEntityValidationException)
        //        {
        //            HandleException(ex as DbEntityValidationException);
        //        }

        //    }
        //    finally
        //    {
        //        Doc.Close();
        //        wordApp.Quit();
        //        wordApp = null;
        //        GC.Collect();
        //    }
        //}
        public static void HandleException(DbEntityValidationException ex)
        {
            // Retrieve the error messages as a list of strings.
            var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

            // Join the list to a single string.
            var fullErrorMessage = string.Join(" ", errorMessages);

            // Combine the original exception message with the new one.
            var exceptionMessage = string.Concat(ex.Message, "  The validation errors are: ", fullErrorMessage);

            // Throw a new DbEntityValidationException with the improved exception message.
            throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
        }

        private void cancelimport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void doneimportbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}