using Dapper;
using DataLibrary.Db;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Data
{
    public class OrderData : IOrderData
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionStringData;

        public OrderData(IDataAccess dataAccess, ConnectionStringData connectionStringData)
        {
            _dataAccess = dataAccess;
            _connectionStringData = connectionStringData;
        }

        public async Task<int> CreateOrder(OrderModel orderModel)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("OrderName", orderModel.OrderName);
            p.Add("OrderDate", orderModel.OrderDate);
            p.Add("FoodId", orderModel.FoodId);
            p.Add("Quantity", orderModel.Quantity);
            p.Add("Total", orderModel.Total);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);
            await _dataAccess.SaveData("dbo.spOrders_Insert", p, _connectionStringData.SqlConnectionStringName);
            return p.Get<int>("Id");
        }
        public Task<int> UpdateOrderName(int orderId, string orderName)
        {
            return _dataAccess.SaveData("dbo.spOrders_UpdateName",
                                        new { Id = orderId, OrderName = orderName },
                                        _connectionStringData.SqlConnectionStringName);
        }

        public Task<int> DeleteOrder(int orderId)
        {
            return _dataAccess.SaveData("spOrders_Delete", new { Id = orderId },
                                        _connectionStringData.SqlConnectionStringName);

        }
        public async Task<OrderModel> GetOrderById(int orderId)
        {
            var records = await _dataAccess.LoadData<OrderModel, dynamic>("dbo.spOrders_GetById",
                                                                            new { Id = orderId },
                                                                            _connectionStringData.SqlConnectionStringName);
            return records.FirstOrDefault();
        }
    }
}
