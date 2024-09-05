using Northwind.Repositories;

namespace Northwind.UnitOfWork
{
    public interface IUnityOfWork
    {
        ICustomerRepository Customer { get;} 
        IUserRepository User { get; }
        ISupplierRepository Supplier { get;  }
        IOrderRepository Order { get;  }
    }
}
