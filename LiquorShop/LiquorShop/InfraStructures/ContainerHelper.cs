using LiquorShopService.Implementations;
using LiquorShopService.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace Rms.InfraStructures
{
  public static  class ContainerHelper
    {
       
        static ContainerHelper()
        {
            _Container = new UnityContainer();
          
            _Container.RegisterType<IPurchaseService, PurchaseService>(new ContainerControlledLifetimeManager());
            _Container.RegisterType<ISalesService, SalesService>(new ContainerControlledLifetimeManager());
            _Container.RegisterType<IBeverageService, BeverageService>(new ContainerControlledLifetimeManager());
            _Container.RegisterType<ISupplierService, SupplierService>(new ContainerControlledLifetimeManager());
            _Container.RegisterType<IAccountingService, AccountingService>(new ContainerControlledLifetimeManager());
            _Container.RegisterType<IUserService, UserService>(new ContainerControlledLifetimeManager());
            _Container.RegisterType<ICustomerService, CustomerService>(new ContainerControlledLifetimeManager());
            _Container.RegisterType<IAdministrationService, AdministrationService>(new ContainerControlledLifetimeManager());
        }


        private static IUnityContainer _Container;
        public static IUnityContainer Container
        {
            get { return _Container; }
           
        }

    }
}
