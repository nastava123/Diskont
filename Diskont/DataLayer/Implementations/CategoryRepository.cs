using DataLayer.Const;
using DataLayer.Interface;
using Entities;
using Microsoft.Data.SqlClient;

namespace DataLayer.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        public bool Add(Category entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "INSERT INTO Categories(Name) values(@Name)";
                sqlCommand.Parameters.AddWithValue("@Name", entity.Name);

                int res = sqlCommand.ExecuteNonQuery();
                return res > 0;
            }
        }

        public bool Delete(Category entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "DELETE FROM Categories WHERE IdCategory=@x";
                sqlCommand.Parameters.AddWithValue("@x", entity.IdCategory);

                int res = sqlCommand.ExecuteNonQuery();
                return res > 0;
            }
        }

        public Category? Get(int id)
        {
            Category? category = null;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM Categories WHERE IdCategory=@x";
                sqlCommand.Parameters.AddWithValue("@x", id);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    category = new Category();
                    category.IdCategory = sqlDataReader.GetInt32(0);
                    category.Name = sqlDataReader.GetString(1);
                }
            }
            return category;
        }

        public List<Category> GetAll()
        {
            List<Category> lista = new List<Category>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM Categories";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Category category = new Category();
                    category.IdCategory = sqlDataReader.GetInt32(0);
                    category.Name = sqlDataReader.GetString(1);
                    lista.Add(category);
                }
            }
            return lista;
        }

        public Category? GetByName(string name)
        {
            Category? category = null;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM Categories WHERE Name=@name";
                sqlCommand.Parameters.AddWithValue("@name", name);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    category = new Category();
                    category.IdCategory = sqlDataReader.GetInt32(0);
                    category.Name = sqlDataReader.GetString(1);
                }
            }
            return category;
        }

        public bool Update(Category entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "UPDATE Categories SET Name=@Name WHERE IdCategory=@IdCategory";
                sqlCommand.Parameters.AddWithValue("@Name", entity.Name);
                sqlCommand.Parameters.AddWithValue("@IdCategory", entity.IdCategory);

                int res = sqlCommand.ExecuteNonQuery();
                return res > 0;
            }
        }
    }
}
