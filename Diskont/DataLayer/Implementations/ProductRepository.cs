using DataLayer.Const;
using DataLayer.Interface;
using Entities;
using Microsoft.Data.SqlClient;

namespace DataLayer.Implementations
{
    public class ProductRepository : IProductRepository
    {
        public bool Add(Product entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "INSERT INTO Products(Name, Price, Count, IdCategory) " +
                    "values(@Name, @Price, @Count, @IdCategory)";
                sqlCommand.Parameters.AddWithValue("@Name", entity.Name);
                sqlCommand.Parameters.AddWithValue("@Price", entity.Price);
                sqlCommand.Parameters.AddWithValue("@Count", entity.Count);
                sqlCommand.Parameters.AddWithValue("@IdCategory", entity.IdCategory);

                int res = sqlCommand.ExecuteNonQuery();
                return res > 0;
            }
        }

        public bool Delete(Product entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "DELETE FROM Products WHERE IdProduct=@x";
                sqlCommand.Parameters.AddWithValue("@x", entity.IdProduct);

                int res = sqlCommand.ExecuteNonQuery();
                return res > 0;
            }
        }

        public Product? Get(int id)
        {
            Product? product = null;     // ako red ne postoji, vraća se null
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM Products WHERE IdProduct=@x";
                sqlCommand.Parameters.AddWithValue("@x", id);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    product = new Product();
                    product.IdProduct = sqlDataReader.GetInt32(0);
                    product.Name = sqlDataReader.GetString(1);
                    product.Price = sqlDataReader.GetDecimal(2);
                    product.Count = sqlDataReader.GetInt32(3);
                    product.IdCategory = sqlDataReader.GetInt32(4);
                }
            }
            return product;
        }

        public List<Product> GetAll()
        {
            List<Product> lista = new List<Product>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM Products";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Product product = new Product();
                    product.IdProduct = sqlDataReader.GetInt32(0);
                    product.Name = sqlDataReader.GetString(1);
                    product.Price = sqlDataReader.GetDecimal(2);
                    product.Count = sqlDataReader.GetInt32(3);
                    product.IdCategory = sqlDataReader.GetInt32(4);
                    lista.Add(product);
                }
            }
            return lista;
        }

        public List<Product> GetByCategory(int idCategory)
        {
            List<Product> lista = new List<Product>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM Products WHERE IdCategory=@x";
                sqlCommand.Parameters.AddWithValue("@x", idCategory);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Product product = new Product();
                    product.IdProduct = sqlDataReader.GetInt32(0);
                    product.Name = sqlDataReader.GetString(1);
                    product.Price = sqlDataReader.GetDecimal(2);
                    product.Count = sqlDataReader.GetInt32(3);
                    product.IdCategory = sqlDataReader.GetInt32(4);
                    lista.Add(product);
                }
            }
            return lista;
        }

        public bool Update(Product entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "UPDATE Products SET Name=@Name, Price=@Price, " +
                    "Count=@Count, IdCategory=@IdCategory WHERE IdProduct=@IdProduct";
                sqlCommand.Parameters.AddWithValue("@Name", entity.Name);
                sqlCommand.Parameters.AddWithValue("@Price", entity.Price);
                sqlCommand.Parameters.AddWithValue("@Count", entity.Count);
                sqlCommand.Parameters.AddWithValue("@IdCategory", entity.IdCategory);
                sqlCommand.Parameters.AddWithValue("@IdProduct", entity.IdProduct);

                int res = sqlCommand.ExecuteNonQuery();
                return res > 0;
            }
        }
    }
}
