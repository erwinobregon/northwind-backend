using Northwind.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Repositories
{
    public interface ISupplierRepository:IRepository<Supplier>
    {
        IEnumerable<Supplier> CustomersPagedList(int page, int rows);

    }
}
