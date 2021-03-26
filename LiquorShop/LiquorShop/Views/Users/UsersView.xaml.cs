using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;
using Rms.InfraStructures;
using LiquorShop.ViewModels.Users;

namespace LiquorShop.Views.Users
{
    /// <summary>
    /// Interaction logic for UsersControl.xaml
    /// </summary>
    public partial class UsersView : UserControl
    {
        UsersViewModel vm;
        public UsersView() 
        {
            InitializeComponent();
            vm = ContainerHelper.Container.Resolve<UsersViewModel>();
            DataContext = vm;
        }

        private void usernametextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if(((UsersControlViewModel)DataContext).SelectedUser==null)
            //{
            //    MessageBox.Show("Please Click New Button To Add New User");
               
            //    newuserbtn.Focus();
            //}
        }
    }
}
