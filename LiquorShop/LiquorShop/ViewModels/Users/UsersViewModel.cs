using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Rms.Data;
using LiquorShopService.Services;
using System.IO;
using System.Windows;
using Rms;
using WpfApp1;
using TescoWineShopSql;

namespace LiquorShop.ViewModels.Users
{
    public class UsersViewModel : ViewModelBase
    {
        #region props       
        private ObservableCollection<string> _MenusList;
        public ObservableCollection<string> MenusList
        {
            get { return _MenusList; }
            set
            {
                SetProperty<ObservableCollection<string>>(ref _MenusList, value);
            }
        }
        private ObservableCollection<UserRole> _UserRoles;
        public ObservableCollection<UserRole> UserRoles
        {
            get
            {
                return _UserRoles;
            }
            set
            {
                SetProperty<ObservableCollection<UserRole>>(ref _UserRoles, value);
            }
        }
        private ObservableCollection<User> _Users;
        public ObservableCollection<User> Users
        {
            get
            {
                return _Users;
            }
            set
            {
                SetProperty<ObservableCollection<User>>(ref _Users, value);
            }
        }
        private User _SelectedUser;
        public User SelectedUser
        {
            get
            {
                return _SelectedUser;
            }
            set
            {

                SetProperty<User>(ref _SelectedUser, value);
                AssignMenu();
            }
        }
        private ObservableCollection<string> _SelectedMenus;
        public ObservableCollection<string> SelectedMenus
        {
            get { return _SelectedMenus; }
            set
            {
                SetProperty<ObservableCollection<string>>(ref _SelectedMenus, value);
            }
        }
        IUserService Service;
        #endregion
        public void AssignMenu()
        {
            SelectedMenus = new ObservableCollection<string>();
            if (SelectedUser != null)
            {
                if (SelectedUser.Menus != null)
                {
                    foreach (var item in SelectedUser.Menus.Split(':'))
                    {
                        SelectedMenus.Add(item);
                    }
                }
            }
        }
        private async void Init()
        {
            try
            {

                UserRoles = new ObservableCollection<UserRole>(await Service.GetAllUserRolesAsync());
                Users = new ObservableCollection<User>(await Service.GetAllUsersAsync());

                Application.Current.Dispatcher.Invoke(() =>
                {
                    //SelectedUser = ((App)Application.Current).CurrentUser;
                    SelectedUser = Users.FirstOrDefault();
                    ((App)Application.Current).CurrentUser = SelectedUser;
                    //if (SelectedUser.UserRole.UserInRole !=TescoWineShopSql.UserRoles.Administrator)
                    //{
                       
                    //    Users = new ObservableCollection<User>(Users.Where(a => a.UserId == SelectedUser.UserId));
                    //    IsAdministrator = false;
                    //}

                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UsersViewModel(IUserService Service)
        {
            this.Service = Service;
            IsDataNotLoaded = true;
            
        }
        #region common
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
            IsAdministrator = true;
            await Task.Run(() => Init()).ContinueWith(a => { IsDataNotLoaded = false;IsDataLoaded = true; });
        }

        /// <summary>
        /// Determines if <see cref="LoadedCommand" /> is allowed to execute
        /// </summary>
        private Boolean LoadedCommand_CanExecute(object obj)
        {
            return true;
        }
        #endregion
        #endregion

        bool _IsAdministrator;
        public bool IsAdministrator
        {
            get
            {
                return _IsAdministrator;
            }
            set
            {
                if (_IsAdministrator != value)
                {
                    _IsAdministrator = value;
                    notify("IsAdministrator");
                }
            }
        }

        string _NewPassword;
        public string NewPassword
        {
            get
            {
                return _NewPassword;
            }
            set
            {
                if (_NewPassword != value)
                {
                    _NewPassword = value;
                    notify("NewPassword");
                }
            }
        }


        #region Commands
        #region NewUserCommand Command
        /// <summary>
        /// Private member backing variable for <see cref="NewUserCommand" />
        /// </summary>
        private RelayCommand _NewUserCommand = null;
        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand NewUserCommand
        {
            get
            {
                if (_NewUserCommand == null)
                { _NewUserCommand = new RelayCommand(NewUserCommand_Execute, NewUserCommand_CanExecute); }
                return _NewUserCommand;
            }
        }
        /// <summary>
        /// Implements the execution of <see cref="NewUserCommand" />
        /// </summary>
        private void NewUserCommand_Execute(object obj)
        {
            var user = ((App)Application.Current).CurrentUser;
            if (user.UserRoleId != 2)
            {               
                    MessageBox.Show("you have no permission");
                    return;
              
            }
            SelectedUser = new User();
        }
        /// <summary>
        /// Determines if <see cref="NewUserCommand" /> is allowed to execute
        /// </summary>
        private Boolean NewUserCommand_CanExecute(object obj)
        {
            return true;
        }
        #endregion
        #region SaveUserCommand Command
        /// <summary>
        /// Private member backing variable for <see cref="SaveUserCommand" />
        /// </summary>
        private RelayCommand _SaveUserCommand = null;
        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand SaveUserCommand
        {
            get
            {
                if (_SaveUserCommand == null)
                { _SaveUserCommand = new RelayCommand(SaveUserCommand_Execute, SaveUserCommand_CanExecute); }
                return _SaveUserCommand;
            }
        }
        /// <summary>
        /// Implements the execution of <see cref="SaveUserCommand" />
        /// </summary>
        private async void SaveUserCommand_Execute(object obj)
        {
            try
            {
                var user = ((App)Application.Current).CurrentUser;
                if (user.UserRoleId!=2)
                {
                    if (SelectedUser.UserId < 1)
                    {
                        MessageBox.Show("you have no permission");
                        return;
                    }
                }
                ResolveMenu();
                SelectedUser.UserCreatedOn = DateTime.Today;  
                if(!string.IsNullOrEmpty(NewPassword))
                {
                    SelectedUser.Password = EvanDangol.Cryptography.CryptoEngine.Encrypt(NewPassword);
                }             
                var v = await Service.SaveUser(SelectedUser);
                if (v > 0)
                {
                    MessageBox.Show("Saved");
                    SelectedUser = null;
                    await Task.Run(() => Init());
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("duplicate"))
                {
                    MessageBox.Show("Username already taken");
                }
                else
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Determines if <see cref="SaveUserCommand" /> is allowed to execute
        /// </summary>
        private Boolean SaveUserCommand_CanExecute(object obj)
        {
            if (SelectedUser != null)
            {
                if (!string.IsNullOrEmpty(SelectedUser.UserName))
                {
                    if (!SelectedUser.HasErrors)
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        #endregion
        #region DeleteUserCommand Command
        /// <summary>
        /// Private member backing variable for <see cref="DeleteUserCommand" />
        /// </summary>
        private RelayCommand _DeleteUserCommand = null;
        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand DeleteUserCommand
        {
            get
            {
                if (_DeleteUserCommand == null)
                { _DeleteUserCommand = new RelayCommand(DeleteUserCommand_Execute, DeleteUserCommand_CanExecute); }
                return _DeleteUserCommand;
            }
        }
        /// <summary>
        /// Implements the execution of <see cref="DeleteUserCommand" />
        /// </summary>
        private async void DeleteUserCommand_Execute(object obj)
        {
            try
            {
                var user = ((App)Application.Current).CurrentUser;
                if (user.UserRole != null)
                {
                    if (user.UserRoleId != 2)
                    {
                        MessageBox.Show("you have no permission");
                        return;
                    }
                }
                var v = await Service.DeleteUser(SelectedUser);
                if (v > 0)
                {
                    MessageBox.Show("Deleted");
                    await Task.Run(() => Init());
                }
                SelectedUser = null;
            }
            catch
            {
                MessageBox.Show("Cannot Delete");
            }
        }
        /// <summary>
        /// Determines if <see cref="DeleteUserCommand" /> is allowed to execute
        /// </summary>
        private Boolean DeleteUserCommand_CanExecute(object obj)
        {
            if (SelectedUser != null)
            {
                if (SelectedUser.UserId > 0)
                {
                    return true;
                }

            }
            return false;
        }
        #endregion
        #endregion
        private void ResolveMenu()
        {

            if (SelectedMenus.Count > 0)
            {
                foreach (var item in SelectedMenus)
                {
                    if (item == SelectedMenus.First())
                    {
                        SelectedUser.Menus = item;
                    }
                    else
                    {
                        SelectedUser.Menus = SelectedUser.Menus + ":" + item;
                    }
                }

            }
        }
    }

}
