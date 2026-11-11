using DataLayer.Const;
using DataLayer.Interface;
using Entities;
using Microsoft.Data.SqlClient;

namespace DataLayer.Implementations
{
    public class OrderItemRepository : IOrderItemRepository
    {
        public bool Add(OrderItem entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "INSERT INTO OrderItems(IdOrder, IdProduct, Quantity, UnitPrice) " +
                    "values(@IdOrder, @IdProduct, @Quantity, @UnitPrice)";
                sqlCommand.Parameters.AddWithValue("@IdOrder", entity.IdOrder);
                sqlCommand.Parameters.AddWithValue("@IdProduct", entity.IdProduct);
                sqlCommand.Parameters.AddWithValue("@Quantity", entity.Quantity);
                sqlCommand.Parameters.AddWithValue("@UnitPrice", entity.UnitPrice);

                int res = sqlCommand.ExecuteNonQuery();
                return res > 0;
            }
        }

        public bool Delete(OrderItem entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "DELETE FROM OrderItems WHERE IdOrderItem=@x";
                sqlCommand.Parameters.AddWithValue("@x", entity.IdOrderItem);

                int res = sqlCommand.ExecuteNonQuery();
                return res > 0;
            }
        }

        public OrderItem? Get(int id)
        {
            OrderItem? item = null;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM OrderItems WHERE IdOrderItem=@x";
                sqlCommand.Parameters.AddWithValue("@x", id);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    item = new OrderItem();
                    item.IdOrderItem = sqlDataReader.GetInt32(0);
                    item.IdOrder = sqlDataReader.GetInt32(1);
                    item.IdProduct = sqlDataReader.GetInt32(2);
                    item.Quantity = sqlDataReader.GetInt32(3);
                    item.UnitPrice = sqlDataReader.GetDecimal(4);
                }
            }
            return item;
        }

        public List<OrderItem> GetAll()
        {
            List<OrderItem> lista = new List<OrderItem>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM OrderItems";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    OrderItem item = new OrderItem();
                    item.IdOrderItem = sqlDataReader.GetInt32(0);
                    item.IdOrder = sqlDataReader.GetInt32(1);
                    item.IdProduct = sqlDataReader.GetInt32(2);
                    item.Quantity = sqlDataReader.GetInt32(3);
                    item.UnitPrice = sqlDataReader.GetDecimal(4);
                    lista.Add(item);
                }
            }
            return lista;
        }

        public List<OrderItem> GetByOrder(int idOrder)
        {
            List<OrderItem> lista = new List<OrderItem>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM OrderItems WHERE IdOrder=@x";
                sqlCommand.Parameters.AddWithValue("@x", idOrder);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    OrderItem item = new OrderItem();
                    item.IdOrderItem = sqlDataReader.GetInt32(0);
                    item.IdOrder = sqlDataReader.GetInt32(1);
                    item.IdProduct = sqlDataReader.GetInt32(2);
                    item.Quantity = sqlDataReader.GetInt32(3);
                    item.UnitPrice = sqlDataReader.GetDecimal(4);
                    lista.Add(item);
                }
            }
            return lista;
        }

        public OrderItem? GetByOrderAndProduct(int idOrder, int idProduct)
        {
            OrderItem? item = null;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM OrderItems WHERE IdOrder=@idOrder AND IdProduct=@idProduct";
                sqlCommand.Parameters.AddWithValue("@idOrder", idOrder);
                sqlCommand.Parameters.AddWithValue("@idProduct", idProduct);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    item = new OrderItem();
                    item.IdOrderItem = sqlDataReader.GetInt32(0);
                    item.IdOrder = sqlDataReader.GetInt32(1);
                    item.IdProduct = sqlDataReader.GetInt32(2);
                    item.Quantity = sqlDataReader.GetInt32(3);
                    item.UnitPrice = sqlDataReader.GetDecimal(4);
                }
            }
            return item;
        }

        public bool Update(OrderItem entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "UPDATE OrderItems SET IdOrder=@IdOrder, IdProduct=@IdProduct, " +
                    "Quantity=@Quantity, UnitPrice=@UnitPrice WHERE IdOrderItem=@IdOrderItem";
                sqlCommand.Parameters.AddWithValue("@IdOrder", entity.IdOrder);
                sqlCommand.Parameters.AddWithValue("@IdProduct", entity.IdProduct);
                sqlCommand.Parameters.AddWithValue("@Quantity", entity.Quantity);
                sqlCommand.Parameters.AddWithValue("@UnitPrice", entity.UnitPrice);
                sqlCommand.Parameters.AddWithValue("@IdOrderItem", entity.IdOrderItem);

                int res = sqlCommand.ExecuteNonQuery();
                return res > 0;
            }
        }
    }
}
