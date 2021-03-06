﻿using LiquorShop.Classes;
using LiquorShopService.Services;
using Newtonsoft.Json;
using Rms;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace LiquorShop.ViewModels.Settings
{
    public class SettingsViewModel : ViewModelBase
    {
        #region props
        private ObservableCollection<CompanyJson> _CompanyJsons;
        public ObservableCollection<CompanyJson> CompanyJsons
        {
            get
            {
                return _CompanyJsons;
            }
            set
            {
                if (_CompanyJsons != value)
                {
                    _CompanyJsons = value;
                    notify("CompanyJsons");
                }
            }
        }

        private bool _IsCurrentCompany;
        public bool IsCurrentCompany
        {
            get
            {
                return _IsCurrentCompany;
            }
            set
            {
                if (_IsCurrentCompany != value)
                {
                    _IsCurrentCompany = value;
                    notify("IsCurrentCompany");
                }
            }
        }


        private DateTime _FiscalYearStartDate;
        public DateTime FiscalYearStartDate
        {
            get
            {
                return _FiscalYearStartDate;
            }
            set
            {
                if (_FiscalYearStartDate != value)
                {
                    _FiscalYearStartDate = value;
                    notify("FiscalYearStartDate");
                }
            }
        }

        private DateTime _FiscalYearEndDate;
        public DateTime FiscalYearEndDate
        {
            get
            {
                return _FiscalYearEndDate;
            }
            set
            {
                if (_FiscalYearEndDate != value)
                {
                    _FiscalYearEndDate = value;
                    notify("FiscalYearEndDate");
                }
            }
        }

        private bool _UseBarcodeYes;
        public bool UseBarcodeYes
        {
            get
            {
                return _UseBarcodeYes;
            }
            set
            {
                if (_UseBarcodeYes != value)
                {
                    _UseBarcodeYes = value;
                    notify("UseBarcodeYes");
                }
            }
        }

        private bool _UseBarcodeNo;
        public bool UseBarcodeNo
        {
            get
            {
                return _UseBarcodeNo;
            }
            set
            {
                if (_UseBarcodeNo != value)
                {
                    _UseBarcodeNo = value;
                    notify("UseBarcodeNo");
                }
            }
        }

        private ObservableCollection<Company> _Companies;
        public ObservableCollection<Company> Companies
        {
            get
            {
                return _Companies;
            }
            set
            {
                if (_Companies != value)
                {
                    _Companies = value;
                    notify("Companies");
                }
            }
        }

        private Company _SelectedCompanyBackUp;
        public Company SelectedCompanyBackUp
        {
            get
            {
                return _SelectedCompanyBackUp;
            }
            set
            {
                if (_SelectedCompanyBackUp != value)
                {
                    _SelectedCompanyBackUp = value;
                    notify("SelectedCompanyBackUp");
                }
            }
        }


        private Company _SelectedCompany;
        public Company SelectedCompany
        {
            get
            {
                return _SelectedCompany;
            }
            set
            {
                if (_SelectedCompany != value)
                {
                    _SelectedCompany = value;
                    notify("SelectedCompany");
                    if (_SelectedCompany.FiscalYear != null)
                    {
                        FiscalYearStartDate = _SelectedCompany.FiscalYear.StartDate;
                        FiscalYearEndDate = _SelectedCompany.FiscalYear.EndDate;

                    }

                }
            }
        }



        private string _FiscalYear;
        public string FiscalYear
        {
            get { return _FiscalYear; }
            set
            {
                SetProperty<string>(ref _FiscalYear, value);
            }
        }

        private ObservableCollection<string> _fiscalyearlist;
        public ObservableCollection<string> fiscalyearlist
        {
            get
            {
                return _fiscalyearlist;
            }
            set
            {
                if (_fiscalyearlist != value)
                {
                    _fiscalyearlist = value;
                    notify("fiscalyearlist");
                }
            }
        }

        private bool _IsAmount;
        public bool IsAmount
        {
            get
            {
                return _IsAmount;
            }
            set
            {
                if (_IsAmount != value)
                {
                    _IsAmount = value;
                    notify("IsAmount");
                }
            }
        }
        private string _DiscountStyle;
        public string DiscountStyle
        {
            get { return _DiscountStyle; }
            set
            {
                SetProperty<string>(ref _DiscountStyle, value);
                if (value.StartsWith("P"))
                {
                    IsPercent = true; IsAmount = false;
                }
                else
                {
                    IsPercent = false;
                    IsAmount = true;
                }
            }
        }

        private bool _IsPercent;
        public bool IsPercent
        {
            get
            {
                return _IsPercent;
            }
            set
            {
                _IsPercent = value;
                notify("IsPercent");

            }
        }
        private bool _IsDataLoaded;
        public bool IsDataLoaded
        {
            get { return _IsDataLoaded; }
            set
            {
                SetProperty<bool>(ref _IsDataLoaded, value);
            }
        }

        private bool _IsDataNotLoaded;
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

        private string _TaxValue;
        public string TaxValue
        {
            get
            {
                return _TaxValue;
            }
            set
            {
                if (_TaxValue != value)
                {
                    _TaxValue = value;
                    notify("TaxValue");
                }
            }
        }

        private string _ServiceCharge;
        public string ServiceCharge
        {
            get
            {
                return _ServiceCharge;
            }
            set
            {
                if (_ServiceCharge != value)
                {
                    _ServiceCharge = value;
                    notify("ServiceCharge");
                }
            }
        }

        private string _ImageFolder;
        public string ImageFolder
        {
            get
            {
                return _ImageFolder;
            }
            set
            {
                if (_ImageFolder != value)
                {
                    _ImageFolder = value;
                    notify("ImageFolder");
                }
            }
        }

        private string _DocumentFolder;
        public string DocumentFolder
        {
            get
            {
                return _DocumentFolder;
            }
            set
            {
                if (_DocumentFolder != value)
                {
                    _DocumentFolder = value;
                    notify("DocumentFolder");
                }
            }
        }

        private string _BarCodeFolder;
        public string BarCodeFolder
        {
            get
            {
                return _BarCodeFolder;
            }
            set
            {
                if (_BarCodeFolder != value)
                {
                    _BarCodeFolder = value;
                    notify("BarCodeFolder");
                }
            }
        }

        private string _Vat;
        public string Vat
        {
            get
            {
                return _Vat;
            }
            set
            {
                if (_Vat != value)
                {
                    _Vat = value;
                    notify("Vat");
                }
            }
        }

        private bool _AutomateLedgerPostYes;
        public bool AutomateLedgerPostYes
        {
            get
            {
                return _AutomateLedgerPostYes;
            }
            set
            {
                if (_AutomateLedgerPostYes != value)
                {
                    _AutomateLedgerPostYes = value;
                    notify("AutomateLedgerPostYes");
                }
            }
        }

        private bool _AutomateLedgerPostNo;
        public bool AutomateLedgerPostNo
        {
            get
            {
                return _AutomateLedgerPostNo;
            }
            set
            {
                if (_AutomateLedgerPostNo != value)
                {
                    _AutomateLedgerPostNo = value;
                    notify("AutomateLedgerPostNo");
                }
            }
        }

        private bool _isVatEnabled;
        public bool IsVatEnabled
        {
            get
            {
                return _isVatEnabled;
            }
            set
            {
                if (_isVatEnabled != value)
                {
                    _isVatEnabled = value;
                    notify("IsVatEnabled");
                }
            }
        }

        private bool _isVatNotEnabled;
        public bool IsVatNotEnabled
        {
            get
            {
                return _isVatNotEnabled;
            }
            set
            {
                if (_isVatNotEnabled != value)
                {
                    _isVatNotEnabled = value;
                    notify("IsVatNotEnabled");
                }
            }
        }

        private ObservableCollection<Administration> _Administrations;
        public ObservableCollection<Administration> Administrations
        {
            get
            {
                return _Administrations;
            }
            set
            {
                if (_Administrations != value)
                {
                    _Administrations = value;
                    notify("Administrations");
                }
            }
        }

        private AdminSettingsViewModel _AdminSettingsViewModel;
        public AdminSettingsViewModel AdminSettingsViewModel
        {
            get
            {
                return _AdminSettingsViewModel;
            }
            set
            {
                if (_AdminSettingsViewModel != value)
                {
                    _AdminSettingsViewModel = value;
                    notify("AdminSettingsViewModel");
                }
            }
        }
        bool _RsChecked;
        public bool RsChecked
        {
            get
            {
                return _RsChecked;
            }
            set
            {
                if (_RsChecked != value)
                {
                    _RsChecked = value;
                    notify("RsChecked");
                }
            }
        }

        bool _DChecked;
        public bool DChecked
        {
            get
            {
                return _DChecked;
            }
            set
            {
                if (_DChecked != value)
                {
                    _DChecked = value;
                    notify("DChecked");
                }
            }
        }


        #endregion
        #region fields
        private IAdministrationService Service;
        //private FolderBrowserDialog dg = new FolderBrowserDialog();
        #endregion
        public SettingsViewModel(IAdministrationService Service)
        {
            this.Service = Service;
            
        }

        #region functions
        public async void Init()
        {
            try
            {
                FiscalYearStartDate = DateTime.Today;
                FiscalYearEndDate = DateTime.Today;
                AdminSettingsViewModel = new AdminSettingsViewModel();
                Administrations = new ObservableCollection<Administration>(await Service.GetAllAdministrationsAsync());
                PostInit();
            }
            catch (Exception ex)
            {
                var v = ex;
                //Administrations = new ObservableCollection<Administration>(await Service.GetAllAdministrationsAsync().ConfigureAwait(false));
                throw ex;
            }
        }

        public void PostInit()
        {
            try
            {
                Task.Run(() =>
                {
                    GetBarCodeFolder();
                    GetTax();
                    GetImageFolder();
                    GetDocumentFolder();
                    GetVat();
                    GetAutomateLedgerPost();
                    GetSomeMore();
                    GetBarCodeUsage();
                    GetCurrency();
                });
            }
            catch (Exception)
            {
                
            }
        }

        private void GetCurrency()
        {
            var curency = Administrations.Where(a => a.Key == "Currency").FirstOrDefault().value;
            RsChecked = curency == "1" ? true : false;
        }

        private void GetBarCodeUsage()
        {
            var yesno = Administrations.Where(a => a.Key == "UseBarCode").FirstOrDefault().value;
            if (yesno == "1")
            {
                UseBarcodeYes = true;
                UseBarcodeNo = false;
            }
            else
            {
                UseBarcodeYes = false;
                UseBarcodeNo = true;
            }
        }

        private void GetSomeMore()
        {
            try
            {
                fiscalyearlist = new ObservableCollection<string>
            {                       "2074/2075",
                                    "2075/2076",
                                    "2076/2077",
                                    "2077/2078",
                                    "2078/2079",
                                    "2079/2080",
                                    "2080/2081",
                                    "2081/2082",
                                    "2082/2083",
                                    "2083/2084",
                                    "2084/2085",
                                    "2085/2086"
            };
                Companies = new ObservableCollection<Company>(Service.GetAllCompaniesAsync().Result);
                FiscalYear = Administrations.Where(x => x.Key == "FiscalYear").Select(x => x.value).FirstOrDefault();
                SelectedCompany = Companies.Where(a => a.IsActive == true).FirstOrDefault();
                DiscountStyle = Administrations.Where(x => x.Key == "DiscountStyle").Select(x => x.value).FirstOrDefault();
                TescoWineShopSql.FiscalYear v = new FiscalYear();

                if (DiscountStyle == "Percent")
                {
                    IsPercent = true;
                    IsAmount = false;
                }
                else
                {
                    IsAmount = true;
                    IsPercent = false;
                }
            }
            catch (Exception)
            {

                //MessageBox.Show("error contact admin");
            }
        }
        private void GetAutomateLedgerPost()
        {
            var yesno = Administrations.Where(a => a.Key == "AutomateLedgerPost").FirstOrDefault().value;
            if (yesno == "1")
            {
                AutomateLedgerPostYes = true;
                AutomateLedgerPostNo = false;
            }
            else
            {
                AutomateLedgerPostYes = false;
                AutomateLedgerPostNo = true;
            }

        }

        private void GetDocumentFolder()
        {
            DocumentFolder = Administrations.Where(a => a.Key == "DocumentFolder").FirstOrDefault().value;
        }

        private void GetImageFolder()
        {
            ImageFolder = Administrations.Where(a => a.Key == "ImageFolder").FirstOrDefault().value;
        }

        private void GetBarCodeFolder()
        {
            BarCodeFolder = Administrations.Where(a => a.Key == "BarCodeFolder").FirstOrDefault().value;
        }
        private void GetVat()
        {
            Vat = Administrations.Where(a => a.Key == "Vat").FirstOrDefault().value;
            var vatstring = Administrations.Where(a => a.Key == "VatEnabled").FirstOrDefault().value;
            if (vatstring == "0")
            {
                IsVatEnabled = false;
                IsVatNotEnabled = true;
            }
            else
            {
                IsVatEnabled = true;
                IsVatNotEnabled = false;
            }
        }

        private void GetTax()
        {
            TaxValue = Administrations.Where(a => a.Key == "TaxRate").FirstOrDefault().value;
        }

        #endregion

       

       



        #region DeleteDataBaseCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="DeleteDataBaseCommand" />
        /// </summary>
        private RelayCommand _DeleteDataBaseCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand DeleteDataBaseCommand
        {
            get
            {
                if (_DeleteDataBaseCommand == null)
                { _DeleteDataBaseCommand = new RelayCommand(DeleteDataBaseCommand_Execute, DeleteDataBaseCommand_CanExecute); }

                return _DeleteDataBaseCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="DeleteDataBaseCommand" />
        /// </summary>
        private void DeleteDataBaseCommand_Execute(object obj)
        {
            Service.DeleteAndRefreshDatabase();
            //DatabaseSeeder.Seed(Service.GetCurrentContext());
        }

        /// <summary>
        /// Determines if <see cref="DeleteDataBaseCommand" /> is allowed to execute
        /// </summary>
        private Boolean DeleteDataBaseCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion


        /// <summary>
        /// Determines if <see cref="ImportMenuCommand" /> is allowed to execute
        /// </summary>
        private Boolean ImportMenuCommand_CanExecute(object obj)
        {
            return true;
        }

        #region ExportMenuCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="ExportMenuCommand" />
        /// </summary>
        private RelayCommand _ExportMenuCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand ExportMenuCommand
        {
            get
            {
                if (_ExportMenuCommand == null)
                { _ExportMenuCommand = new RelayCommand(ExportMenuCommand_Execute, ExportMenuCommand_CanExecute); }

                return _ExportMenuCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="ExportMenuCommand" />
        /// </summary>
        private void ExportMenuCommand_Execute(object obj)
        {
            //List<Rms.Classes.Menu> ms = new List<Rms.Classes.Menu>();
            //foreach (var item in Service.GetAllItems().ToList())
            //{
            //    ms.Add(new Rms.Classes.Menu { Categories = item.ItemCategory.CategoryName, Item = item.ItemName, Price = item.Price });
            //}
            //EvanDangol.Reporting.ExportToExcel<Rms.Classes.Menu, List<Rms.Classes.Menu>> mxp = new EvanDangol.Reporting.ExportToExcel<Rms.Classes.Menu, List<Rms.Classes.Menu>>();
            //mxp.dataToPrint = ms;
            //mxp.GenerateReport();
        }

        /// <summary>
        /// Determines if <see cref="ExportMenuCommand" /> is allowed to execute
        /// </summary>
        private Boolean ExportMenuCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion




        #region VatYesNoCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="VatYesNoCommand" />
        /// </summary>
        private RelayCommand _VatYesNoCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand VatYesNoCommand
        {
            get
            {
                if (_VatYesNoCommand == null)
                { _VatYesNoCommand = new RelayCommand(VatYesNoCommand_Execute, VatYesNoCommand_CanExecute); }

                return _VatYesNoCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="VatYesNoCommand" />
        /// </summary>
        private void VatYesNoCommand_Execute(object obj)
        {
            string vat = (string)obj;
            switch (vat)
            {
                case "yes":
                    Administrations.Where(a => a.Key == "VatEnabled").FirstOrDefault().value = "1";
                    break;
                case "no":
                    Administrations.Where(a => a.Key == "VatEnabled").FirstOrDefault().value = "0";
                    break;
            }
            Service.SaveBeverageCategoryAsync();
        }

        /// <summary>
        /// Determines if <see cref="VatYesNoCommand" /> is allowed to execute
        /// </summary>
        private Boolean VatYesNoCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion

        #region SaveDiscountStyleCommand Command
        /// <summary>
        /// Private member backing variable for <see cref="SaveDiscountStyleCommand" />
        /// </summary>
        private RelayCommand _SaveDiscountStyleCommand = null;
        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand SaveDiscountStyleCommand
        {
            get
            {
                if (_SaveDiscountStyleCommand == null)
                { _SaveDiscountStyleCommand = new RelayCommand(SaveDiscountStyleCommand_Execute, SaveDiscountStyleCommand_CanExecute); }
                return _SaveDiscountStyleCommand;
            }
        }
        /// <summary>
        /// Implements the execution of <see cref="SaveDiscountStyleCommand" />
        /// </summary>
        private async void SaveDiscountStyleCommand_Execute(object obj)
        {
            if (IsPercent)
            {
                Administrations.Where(a => a.Key == "DiscountStyle").FirstOrDefault().value = "Percent";
            }
            else
            {
                Administrations.Where(a => a.Key == "DiscountStyle").FirstOrDefault().value = "Amount";
            }
            await Service.SaveBeverageCategoryAsync();
        }
        /// <summary>
        /// Determines if <see cref="SaveDiscountStyleCommand" /> is allowed to execute
        /// </summary>
        private Boolean SaveDiscountStyleCommand_CanExecute(object obj)
        {
            return true;
        }
        #endregion

        #region SaveAllCommand Command
        /// <summary>
        /// Private member backing variable for <see cref="SaveAllCommand" />
        /// </summary>
        private RelayCommand _SaveAllCommand = null;
        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand SaveAllCommand
        {
            get
            {
                if (_SaveAllCommand == null)
                { _SaveAllCommand = new RelayCommand(SaveAllCommand_Execute, SaveAllCommand_CanExecute); }
                return _SaveAllCommand;
            }
        }
        /// <summary>
        /// Implements the execution of <see cref="SaveAllCommand" />
        /// </summary>
        private void SaveAllCommand_Execute(object obj)
        {
            // var data = await Service.GetAllAdministrationsAsync();
        }
        /// <summary>
        /// Determines if <see cref="SaveAllCommand" /> is allowed to execute
        /// </summary>
        private Boolean SaveAllCommand_CanExecute(object obj)
        {
            return true;
        }
        #endregion

        #region LoadedCommand Command
        /// <summary>
        /// Private member backing variable for <see cref="LoadedCommand" />
        /// </summary>
        private RelayCommand _LoadedCommand = null;
        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand LoadedCommand
        {
            get
            {
                if (_LoadedCommand == null)
                { _LoadedCommand = new RelayCommand(LoadedCommand_Execute, LoadedCommand_CanExecute); }
                return _LoadedCommand;
            }
        }
        /// <summary>
        /// Implements the execution of <see cref="LoadedCommand" />
        /// </summary>
        private async void LoadedCommand_Execute(object obj)
        {
            IsDataLoaded = false;
            IsDataNotLoaded = true;
            await Task.Run(() => Init()).ContinueWith(a => { IsDataNotLoaded = false; IsDataLoaded = true; });

        }
        /// <summary>
        /// Determines if <see cref="LoadedCommand" /> is allowed to execute
        /// </summary>
        private Boolean LoadedCommand_CanExecute(object obj)
        {
            return true;
        }
        #endregion

        #region SaveFiscalYearCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="SaveFiscalYearCommand" />
        /// </summary>
        private RelayCommand _SaveFiscalYearCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand SaveFiscalYearCommand
        {
            get
            {
                if (_SaveFiscalYearCommand == null)
                { _SaveFiscalYearCommand = new RelayCommand(SaveFiscalYearCommand_Execute, SaveFiscalYearCommand_CanExecute); }

                return _SaveFiscalYearCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="SaveFiscalYearCommand" />
        /// </summary>
        private async void SaveFiscalYearCommand_Execute(object obj)
        {
            Administrations.Where(a => a.Key == "FiscalYear").FirstOrDefault().value = FiscalYear;

            await Service.SaveBeverageCategoryAsync();
        }

        /// <summary>
        /// Determines if <see cref="SaveFiscalYearCommand" /> is allowed to execute
        /// </summary>
        private Boolean SaveFiscalYearCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion



        #region NewCompanyCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="NewCompanyCommand" />
        /// </summary>
        private RelayCommand _NewCompanyCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand NewCompanyCommand
        {
            get
            {
                if (_NewCompanyCommand == null)
                { _NewCompanyCommand = new RelayCommand(NewCompanyCommand_Execute, NewCompanyCommand_CanExecute); }

                return _NewCompanyCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="NewCompanyCommand" />
        /// </summary>
        private void NewCompanyCommand_Execute(object obj)
        {
            SelectedCompany = new Company();
        }

        /// <summary>
        /// Determines if <see cref="NewCompanyCommand" /> is allowed to execute
        /// </summary>
        private Boolean NewCompanyCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion




        #region DeleteCompanyCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="DeleteCompanyCommand" />
        /// </summary>
        private RelayCommand _DeleteCompanyCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand DeleteCompanyCommand
        {
            get
            {
                if (_DeleteCompanyCommand == null)
                { _DeleteCompanyCommand = new RelayCommand(DeleteCompanyCommand_Execute, DeleteCompanyCommand_CanExecute); }

                return _DeleteCompanyCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="DeleteCompanyCommand" />
        /// </summary>
        private void DeleteCompanyCommand_Execute(object obj)
        {
            int a = Service.DeleteCompany(SelectedCompany);
        }

        /// <summary>
        /// Determines if <see cref="DeleteCompanyCommand" /> is allowed to execute
        /// </summary>
        private Boolean DeleteCompanyCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion



    }
}