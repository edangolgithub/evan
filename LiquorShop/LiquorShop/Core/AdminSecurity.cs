using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace EvanDangol.Core
{
    public delegate void EventHandler<IsAdminEventArgs>(object sender, IsAdminEventArgs e);
    //-----------------------------------------------------------------------------------------------------
    public class IsAdminEventArgs : EventArgs
    {
        public bool IsAdmin { get; set; }
    }
    //-----------------------------------------------------------------------------------------------------
    public class AdminSecurity
    {
        public event EventHandler<IsAdminEventArgs> LoginChanged;
        //-----------------------------------------------------------------------------------------------------
        protected virtual void BroadcastLoginChanged()
        {
            EventHandler<IsAdminEventArgs> handler = LoginChanged;
            if (handler != null)
            {
                IsAdminEventArgs e = new IsAdminEventArgs();
                e.IsAdmin = this.IsAdmin;
                handler(this, e);
            }
        }
        private bool _IsAdmin;
        //-----------------------------------------------------------------------------------------------------
        public bool IsAdmin
        {
            get { return _IsAdmin; }
            set
            {
                _IsAdmin = value;
                BroadcastLoginChanged();
            }
        }
    }
}


