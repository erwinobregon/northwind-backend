using Dapper;
using Northwind.Models;
using Northwind.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Northwind.DataAccess
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(string connectionString) : base(connectionString)
        {

        }

        public IEnumerable<OrderList> getPaginatedOrder(int page, int row)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@page", page);
            parameters.Add("@rows", row);

            using (var connection = new SqlConnection(_connectionString))
            {
                var reader = connection.QueryMultiple("dbo.get_paginated_orders",
                  parameters, commandType: CommandType.StoredProcedure);

                var orderList = reader.Read<OrderList>().ToList();
                var orderItemList = reader.Read<OrderItemList>().ToList();

                foreach (var item in orderList)
                {
                    item.SetDetail(orderItemList);
                }

                return orderList;
            };
        }
    }
}
