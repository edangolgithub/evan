// using LiquorShopService.Services;
// using System;
// using System.Collections.ObjectModel;
// using System.Linq;
// using System.Threading.Tasks;
// using TescoWineShopSql;

// namespace Rms.ViewModels
// {
//     public class RmsDashBoardViewModel : ViewModelBase
//     {
//         ObservableCollection<Administration> _Administrations;
//         public ObservableCollection<Administration> Administrations
//         {
//             get
//             {
//                 return _Administrations;
//             }
//             set
//             {
//                 if (_Administrations != value)
//                 {
//                     _Administrations = value;
//                     notify("Administrations");
//                 }
//             }
//         }
//         IAdministrationService Service;
//         public RmsDashBoardViewModel(IAdministrationService Service)
//         {
            
//             //this.SalesService = salesservice;
//             IsUserLoggedIn = false; ;
//             IsUserNotLoggedIn = true;
//             //IsDataLoaded = false;
//             //IsDataNotLoaded = true;
//             IsAuthenticating = false;
//             IsNotAuthenticating = true;
//             this.Service = Service;
          

//         }
//         public  void Init()
//         {

//           //await  HackLogin();
//             //if (SelectedUser != null)
//             //{
//             //    await AuthenticateUser(SelectedUser);
//             //}
//         }

//         string _BarCodePath;
//         public string BarCodePath
//         {
//             get
//             {
//                 return _BarCodePath;
//             }
//             set
//             {
//                 if (_BarCodePath != value)
//                 {
//                     _BarCodePath = value;
//                     notify("BarCodePath");
//                 }
//             }
//         }

//         bool _IsNotAuth;
//         public bool IsNotAuth
//         {
//             get
//             {
//                 return _IsNotAuth;
//             }
//             set
//             {
//                 if (_IsNotAuth != value)
//                 {
//                     _IsNotAuth = value;
//                     notify("IsNotAuth");
//                 }
//             }
//         }
//         bool _IsUserNotLoggedIn;
//         public bool IsUserNotLoggedIn
//         {
//             get
//             {
//                 return _IsUserNotLoggedIn;
//             }
//             set
//             {
//                 if (_IsUserNotLoggedIn != value)
//                 {
//                     _IsUserNotLoggedIn = value;
//                     notify("IsUserNotLoggedIn");
//                 }
//             }
//         }
//         private bool _IsUserLoggedIn;
//         public bool IsUserLoggedIn
//         {
//             get { return _IsUserLoggedIn; }
//             set
//             {
//                 _IsUserLoggedIn = value;
//                 notify("IsUserLoggedIn");
//             }
//         }
//         private bool _IsAuthenticating;
//         public bool IsAuthenticating
//         {
//             get
//             {
//                 return _IsAuthenticating;
//             }
//             set
//             {
//                 SetProperty<bool>(ref _IsAuthenticating, value);
//                 if (IsAuthenticating)
//                 {
//                     IsNotAuth = false;
//                 }
//                 else
//                 {
//                     IsNotAuth = true;
//                 }
//             }
//         }
//         private bool _IsNotAuthenticating;
//         public bool IsNotAuthenticating
//         {
//             get
//             {
//                 return _IsNotAuthenticating;
//             }
//             set
//             {
//                 SetProperty<bool>(ref _IsNotAuthenticating, value);
//             }
//         }
      
//         private UserVm _UserVm;
//         public UserVm UserVm
//         {
//             get
//             {
//                 if (_UserVm == null)
//                 {
//                     _UserVm = new UserVm();
//                 }
//                 return _UserVm;
//             }
//             set
//             {
//                 SetProperty<UserVm>(ref _UserVm, value);
//             }
//         }
      
//         private User _SelectedUser;
//         public User SelectedUser
//         {
//             get { return _SelectedUser; }
//             set
//             {
//                 SetProperty<User>(ref _SelectedUser, value);
//             }
//         }
//         string _LoginText;
//         public string LoginText
//         {
//             get
//             {
//                 return _LoginText;
//             }
//             set
//             {
//                 if (_LoginText != value)
//                 {
//                     _LoginText = value;
//                     notify("LoginText");
//                 }
//             }
//         }
//         public async Task HackLogin()
//         {
//             try
//             {
//                 IsUserLoggedIn = true;
//                 IsUserNotLoggedIn = false;
//                 // LoadAllMenu();
//                 SelectedUser = await Task.Run(()=> Service.FindUserByUserNameAndPassword("a", "a"));
                
//                await Task.Run(()=> ResolveAdministrations());

//                 ((App)Application.Current).CurrentUser = SelectedUser;
//                 if (SelectedUser != null)
//                 {
//                     LoginText = "Welcome , " + SelectedUser.UserName + "!";
//                 }
//             }
//             catch(Exception ex)
//             {
//                 throw ex;
//             }
//         }
//         private void ResolveBarCodeUsage()
//         {
//                    }
//         // private async Task ResolveAdministrations()
//         // {
//         //     // try
//         //     // {
//         //     //     Administrations = new ObservableCollection<Administration>(await Service.GetAllAdministrationsAsync().ConfigureAwait(false));
//         //     //    await Task.Run(async () =>
//         //     //     {
//         //     //         // ((App)Application.Current).BarCodeFolder = Administrations.Where(a => a.Key == "BarCodeFolder").FirstOrDefault().value;
//         //     //         // ((App)Application.Current).AutomateLedgerPost = Administrations.Where(a => a.Key == "AutomateLedgerPost").FirstOrDefault().value == "1" ? true : false;
//         //     //         // ((App)Application.Current).UseBarCodeService = Administrations.Where(a => a.Key == "UseBarCode").FirstOrDefault().value == "1" ? true : false;
//         //     //         // ((App)Application.Current).SelectedCompany =await Service.GetSelectedCompanyAsync();


//         //     //     });
//         //     // }
//         //     // catch (Exception ex)
//         //     // {
//         //     //     throw ex;
//         //     // }
//         // }
//         private bool _isOpen;
//         public bool IsOpen
//         {
//             get { return _isOpen; }
//             set
//             {
//                 if (_isOpen == value) return;
//                 _isOpen = value;
//                 notify("IsOpen");
//             }
//         }
//         private async void ProcessLogin(object obj)
//         {
//             try
//             {
               
//                 string p = "";
//                 //char t;
//                 //System.Security.SecureString s;
//                 App.Current.Dispatcher.Invoke(() => {
//                     PasswordBox pb = (PasswordBox)obj;
//                     p = pb.Password;
//                     // t = pb.PasswordChar;
//                     //s = pb.SecurePassword;
//                     pb.Clear();
//                 });
//                 UserVm.password = p;

//                 App.Current.Dispatcher.Invoke(() =>
//                 {
//                     if (UserVm.name == "swaha" && string.IsNullOrEmpty(p))
//                     {
//                         IsOpen = true;
//                         var confirm = //MessageBox.Show("sure??", "Confirm", //MessageBoxButton.YesNoCancel, //MessageBoxImage.Warning);
                       
//                         if (confirm == //MessageBoxResult.Yes)
//                         {
                            
//                             DeleteDataBaseCommand_Execute(null);
                           
//                         }
//                         IsOpen = false;
//                         return;
//                     }
//                 });
//                     var v = await AuthenticateUser(UserVm);

//                 if (v)
//                 {
//                     IsUserLoggedIn = true;
//                     LoginText = "Welcome , " + UserVm.name + "!";
//                     IsUserNotLoggedIn = false;
//                     //((App)Application.Current).SelectedCompany = new Rms.Service.Implementations.AdministrationService().SelectedCompany;
//                     ((App)Application.Current).CurrentUser = SelectedUser;
//                     await  ResolveAdministrations();
//                 }
//                 else
//                 {
//                     App.Current.Dispatcher.Invoke(() =>
//                     {
//                         //MessageBox.Show(((App)Application.Current).MainWindow, "Login UnSuccessful");
//                     });
//                     IsUserLoggedIn = false;
//                     LoginText = "Please Login To continue";
//                     IsUserNotLoggedIn = true;
//                 }

//             }
//             catch (Exception ex)
//             {
//                 throw ex;
//             }
//         }
//         #region DeleteDataBaseCommand Command

//         /// <summary>
//         /// Private member backing variable for <see cref="DeleteDataBaseCommand" />
//         /// </summary>
//         private RelayCommand _DeleteDataBaseCommand = null;

//         /// <summary>
//         /// Gets the command which The command's value
//         /// </summary>
//         public RelayCommand DeleteDataBaseCommand
//         {
//             get
//             {
//                 if (_DeleteDataBaseCommand == null)
//                 { _DeleteDataBaseCommand = new RelayCommand(DeleteDataBaseCommand_Execute, DeleteDataBaseCommand_CanExecute); }

//                 return _DeleteDataBaseCommand;
//             }
//         }

//         /// <summary>
//         /// Implements the execution of <see cref="DeleteDataBaseCommand" />
//         /// </summary>
//         private void DeleteDataBaseCommand_Execute(object obj)
//         {
//             Service.DeleteAndRefreshDatabase();
//             System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
           
//             Application.Current.Shutdown();
//         }

//         /// <summary>
//         /// Determines if <see cref="DeleteDataBaseCommand" /> is allowed to execute
//         /// </summary>
//         private Boolean DeleteDataBaseCommand_CanExecute(object obj)
//         {
//             return true;
//         }

//         #endregion
//         private async Task AuthenticateUser(User selectedUser)
//         {
//             UserVm uvm = new ViewModels.UserVm();
//             uvm.name = selectedUser.UserName;
//             uvm.password = selectedUser.Password;
//             await AuthenticateUser(uvm);
//         }
//         private async Task<bool> AuthenticateUser(UserVm uservm)
//         {
//             var uname = uservm.name;
//             var pass = uservm.password;
//             var password = EvanDangol.Cryptography.CryptoEngine.Encrypt(pass);
//             SelectedUser = await Service.FindUserByUserNameAndPassword(uname, password);
//             //if (SelectedUser != null)
//             //{
//             //    if (SelectedUser.UserRole.UserInRole == UserRoles.Administrator)
//             //    {
//             //        LoadAllMenu();
//             //    }
//             //    else
//             //    {
//             //        ResolveMenuAccess();
//             //    }
//             //
//             //}
//             return SelectedUser != null;
//         }


//         #region prop

//         bool _IsDataNotLoaded;
//         public bool IsDataNotLoaded
//         {
//             get
//             {
//                 return _IsDataNotLoaded;
//             }
//             set
//             {
//                 if (_IsDataNotLoaded != value)
//                 {
//                     _IsDataNotLoaded = value;
//                     notify("IsDataNotLoaded");
//                 }
//             }
//         }
       
//         private bool _IsDataLoaded;
//         public bool IsDataLoaded
//         {
//             get
//             {
//                 return _IsDataLoaded;
//             }
//             set
//             {
//                 SetProperty<bool>(ref _IsDataLoaded, value);
//             }
//         }
       
//         #endregion
//         #region Commands
//         #region TileCLickCommand Command
//         /// <summary>
//         /// Private member backing variable for <see cref="TileCLickCommand" />
//         /// </summary>
//         private RelayCommand _TileCLickCommand = null;
//         /// <summary>
//         /// Gets the command which The command's value
//         /// </summary>
//         public RelayCommand TileCLickCommand
//         {
//             get
//             {
//                 if (_TileCLickCommand == null)
//                 { _TileCLickCommand = new RelayCommand(TileCLickCommand_Execute, TileCLickCommand_CanExecute); }
//                 return _TileCLickCommand;
//             }
//         }
//         private bool TileCLickCommand_CanExecute(object obj)
//         {
//             return true;
//         }
//         private void TileCLickCommand_Execute(object obj)
//         {
//             try
//             {
//                 string typename = obj.ToString();
//                 if (typename == "Backup/Restore")
//                 {
//                     InvokeBackUpRestore();
//                     return;
//                 }
//                 string typenamefull = "";
//                 typenamefull = "LiquorShop.Views."+typename+"." + typename + "Window";
//                 var type = Type.GetType(typenamefull);

//                 App.Current.MainWindow.Hide();
//                 App.Current.MainWindow.ShowInTaskbar = false;

//                 Window w = (Window)Activator.CreateInstance(type);
//                 w.ShowDialog();

//                 App.Current.MainWindow.Show();
//                 App.Current.MainWindow.ShowInTaskbar = true;
//             }
//             catch (Exception ex)
//             {
//                 App.Current.MainWindow.Show();
//                 throw ex;
              
//             }
//         }
//         private void InvokeBackUpRestore()
//         {
//             EvanDangol.Data.DataBackupWindow dbb = new EvanDangol.Data.DataBackupWindow("WineShop");
//             dbb.ShowDialog();
//         }
//         /// <summary>
//         /// Implements the execution of <see cref="TileCLickCommand" />
//         /// </summary>
//         private void TileCLickCommand_Execute()
//         {
//         }
//         /// <summary>
//         /// Determines if <see cref="TileCLickCommand" /> is allowed to execute
//         /// </summary>
//         private Boolean TileCLickCommand_CanExecute()
//         {
//             return true;
//         }
//         #endregion

//         #region LogOutCommand
//         /// <summary>
//         /// Private member backing variable for <see cref="LogOutCommand" />
//         /// </summary>
//         private RelayCommand _LogOutCommand = null;
//         /// <summary>
//         /// Gets the command which a
//         /// </summary>
//         public RelayCommand LogOutCommand
//         {
//             get
//             {
//                 if (_LogOutCommand == null)
//                 { _LogOutCommand = new RelayCommand(LogOutCommand_Execute, LogOutCommand_CanExecute); }
//                 return _LogOutCommand;
//             }
//         }
//         private bool LogOutCommand_CanExecute(object obj)
//         {
//             return true;
//         }
//         private void LogOutCommand_Execute(object obj)
//         {
//             IsUserLoggedIn = false;
//             IsUserNotLoggedIn = true;
//             UserVm = null;
//             SelectedUser = null;
//             ((App)Application.Current).CurrentUser = null;
//             //DisableAllMenu();
//         }
//         ///// <summary>
//         ///// Implements the execution of <see cref="LogOutCommand" />
//         ///// </summary>
//         //private void LogOutCommand_Execute()
//         //{
//         //}
//         ///// <summary>
//         ///// Determines if <see cref="LogOutCommand" /> is allowed to execute
//         ///// </summary>
//         //private Boolean LogOutCommand_CanExecute()
//         //{
//         //    return true;
//         //}
//         #endregion
//         #region LoginCommand Command
//         /// <summary>
//         /// Private member backing variable for <see cref="LoginCommand" />
//         /// </summary>
//         private RelayCommand _LoginCommand = null;
//         /// <summary>
//         /// Gets the command which The command's value
//         /// </summary>
//         public RelayCommand LoginCommand
//         {
//             get
//             {
//                 if (_LoginCommand == null)
//                 { _LoginCommand = new RelayCommand(LoginCommand_Execute, LoginCommand_CanExecute); }
//                 return _LoginCommand;
//             }
//         }
//         /// <summary>
//         /// Implements the execution of <see cref="LoginCommand" />
//         /// </summary>
//         private async void LoginCommand_Execute(object obj)
//         {
//             IsNotAuthenticating = false;
//             IsAuthenticating = true;
//             await Task.Run(() => ProcessLogin(obj)).ContinueWith(a => { IsNotAuthenticating = true; IsAuthenticating = false; });
//         }
//         /// <summary>
//         /// Determines if <see cref="LoginCommand" /> is allowed to execute
//         /// </summary>
//         private Boolean LoginCommand_CanExecute(object obj)
//         {
//             return true;
//         }
//         #endregion

//         #endregion
//         #region common
//         #region LoadedCommand Command
//         /// <summary>
//         /// Private member backing variable for <see cref="LoadedCommand" />
//         /// </summary>
//         private RelayCommand _LoadedCommand = null;
//         /// <summary>
//         /// Gets the command which The command's value
//         /// </summary>
//         public RelayCommand LoadedCommand
//         {
//             get
//             {
//                 if (_LoadedCommand == null)
//                 { _LoadedCommand = new RelayCommand(LoadedCommand_ExecuteAsync, LoadedCommand_CanExecute); }
//                 return _LoadedCommand;
//             }
//         }
//         /// <summary>
//         /// Implements the execution of <see cref="LoadedCommand" />
//         /// </summary>
//         private async void LoadedCommand_ExecuteAsync(object obj)
//         {
//          await  Task.Run(() => Init()).ContinueWith(a => { IsDataNotLoaded = false; IsDataLoaded = true; });
//         }

      

//         /// <summary>
//         /// Determines if <see cref="LoadedCommand" /> is allowed to execute
//         /// </summary>
//         private Boolean LoadedCommand_CanExecute(object obj)
//         {
//             return true;
//         }
//         #endregion
//         #endregion
      
//     }
//     public class UserVm
//     {
//         public string name { get; set; }
//         public string password { get; set; }
//     }
// }
