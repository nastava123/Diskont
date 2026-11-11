using DataLayer.Const;
using DataLayer.Interface;
using Entities;
using Microsoft.Data.SqlClient;

namespace DataLayer.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        public bool Add(Order entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                
                sqlCommand.CommandText = "INSERT INTO Orders(IdUser, TotalPrice, IsCompleted) " +
                    "values(@IdUser, @TotalPrice, @IsCompleted)";
                sqlCommand.Parameters.AddWithValue("@IdUser", entity.IdUser);
                sqlCommand.Parameters.AddWithValue("@TotalPrice", entity.TotalPrice);
                sqlCommand.Parameters.AddWithValue("@IsCompleted", entity.IsCompleted);

                int res = sqlCommand.ExecuteNonQuery();
                return res > 0;
            }
        }

        public bool Delete(Order entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                // prvo stavke, pa porudžbina (strani ključ OrderItems.IdOrder)
                sqlCommand.CommandText = "DELETE FROM OrderItems WHERE IdOrder=@x; " +
                    "DELETE FROM Orders WHERE IdOrder=@x";
                sqlCommand.Parameters.AddWithValue("@x", entity.IdOrder);

                int res = sqlCommand.ExecuteNonQuery();
                return res > 0;
            }
        }

        public Order? Get(int id)
        {
            Order? order = null;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM Orders WHERE IdOrder=@x";
                sqlCommand.Parameters.AddWithValue("@x", id);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    order = new Order();
                    order.IdOrder = sqlDataReader.GetInt32(0);
                    order.IdUser = sqlDataReader.GetInt32(1);
                    order.OrderDate = sqlDataReader.GetDateTime(2);
                    order.TotalPrice = sqlDataReader.GetDecimal(3);
                    order.IsCompleted = sqlDataReader.GetBoolean(4);
                }
            }
            return order;
        }

        public List<Order> GetAll()
        {
            List<Order> lista = new List<Order>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM Orders";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Order order = new Order();
                    order.IdOrder = sqlDataReader.GetInt32(0);
                    order.IdUser = sqlDataReader.GetInt32(1);
                    order.OrderDate = sqlDataReader.GetDateTime(2);
                    order.TotalPrice = sqlDataReader.GetDecimal(3);
                    order.IsCompleted = sqlDataReader.GetBoolean(4);
                    lista.Add(order);
                }
            }
            return lista;
        }

        public List<Order> GetAllByUser(int idUser)
        {
            List<Order> lista = new List<Order>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM Orders WHERE IdUser=@x ORDER BY OrderDate DESC";
                sqlCommand.Parameters.AddWithValue("@x", idUser);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Order order = new Order();
                    order.IdOrder = sqlDataReader.GetInt32(0);
                    order.IdUser = sqlDataReader.GetInt32(1);
                    order.OrderDate = sqlDataReader.GetDateTime(2);
                    order.TotalPrice = sqlDataReader.GetDecimal(3);
                    order.IsCompleted = sqlDataReader.GetBoolean(4);
                    lista.Add(order);
                }
            }
            return lista;
        }

        // Korpa = porudžbina korisnika koja još nije završena (IsCompleted = 0)
        public Order? GetOrderByUser(int idUser)
        {
            Order? order = null;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM Orders WHERE IdUser=@x AND IsCompleted=0";
                sqlCommand.Parameters.AddWithValue("@x", idUser);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    order = new Order();
                    order.IdOrder = sqlDataReader.GetInt32(0);
                    order.IdUser = sqlDataReader.GetInt32(1);
                    order.OrderDate = sqlDataReader.GetDateTime(2);
                    order.TotalPrice = sqlDataReader.GetDecimal(3);
                    order.IsCompleted = sqlDataReader.GetBoolean(4);
                }
            }
            return order;
        }

        public bool Update(Order entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "UPDATE Orders SET IdUser=@IdUser, OrderDate=@OrderDate, " +
                    "TotalPrice=@TotalPrice, IsCompleted=@IsCompleted WHERE IdOrder=@IdOrder";
                sqlCommand.Parameters.AddWithValue("@IdUser", entity.IdUser);
                sqlCommand.Parameters.AddWithValue("@OrderDate", entity.OrderDate);
                sqlCommand.Parameters.AddWithValue("@TotalPrice", entity.TotalPrice);
                sqlCommand.Parameters.AddWithValue("@IsCompleted", entity.IsCompleted);
                sqlCommand.Parameters.AddWithValue("@IdOrder", entity.IdOrder);

                int res = sqlCommand.ExecuteNonQuery();
                return res > 0;
            }
        }
    }
}
