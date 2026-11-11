using DataLayer.Const;
using DataLayer.Interface;
using Entities;
using Microsoft.Data.SqlClient;

namespace DataLayer.Implementations
{
    public class UserRepository : IUserRepository
    {
        public bool Add(User entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
               
                sqlCommand.CommandText = "INSERT INTO Users(FirstName, LastName, Email, PasswordHash, Status, IdRole) " +
                    "values(@FirstName, @LastName, @Email, @PasswordHash, @Status, @IdRole)";
                sqlCommand.Parameters.AddWithValue("@FirstName", entity.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", entity.LastName);
                sqlCommand.Parameters.AddWithValue("@Email", entity.Email);
                sqlCommand.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
                sqlCommand.Parameters.AddWithValue("@Status", entity.Status);
                sqlCommand.Parameters.AddWithValue("@IdRole", entity.IdRole);

                int res = sqlCommand.ExecuteNonQuery();
                return res > 0;
            }
        }

        public bool Delete(User entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "DELETE FROM Users WHERE IdUser=@x";
                sqlCommand.Parameters.AddWithValue("@x", entity.IdUser);

                int res = sqlCommand.ExecuteNonQuery();
                return res > 0;
            }
        }

        public User? Get(int id)
        {
            User? user = null;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM Users WHERE IdUser=@x";
                sqlCommand.Parameters.AddWithValue("@x", id);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    user = new User();
                    user.IdUser = sqlDataReader.GetInt32(0);
                    user.FirstName = sqlDataReader.GetString(1);
                    user.LastName = sqlDataReader.GetString(2);
                    user.Email = sqlDataReader.GetString(3);
                    user.PasswordHash = sqlDataReader.GetString(4);
                    user.Status = sqlDataReader.GetBoolean(5);
                    user.CreatedDate = sqlDataReader.GetDateTime(6);
                    user.IdRole = sqlDataReader.GetInt32(7);
                }
            }
            return user;
        }

        public List<User> GetAll()
        {
            List<User> lista = new List<User>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM Users";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    User user = new User();
                    user.IdUser = sqlDataReader.GetInt32(0);
                    user.FirstName = sqlDataReader.GetString(1);
                    user.LastName = sqlDataReader.GetString(2);
                    user.Email = sqlDataReader.GetString(3);
                    user.PasswordHash = sqlDataReader.GetString(4);
                    user.Status = sqlDataReader.GetBoolean(5);
                    user.CreatedDate = sqlDataReader.GetDateTime(6);
                    user.IdRole = sqlDataReader.GetInt32(7);
                    lista.Add(user);
                }
            }
            return lista;
        }

        public User? GetByEmail(string email)
        {
            User? user = null;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM Users WHERE Email=@email";
                sqlCommand.Parameters.AddWithValue("@email", email);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    user = new User();
                    user.IdUser = sqlDataReader.GetInt32(0);
                    user.FirstName = sqlDataReader.GetString(1);
                    user.LastName = sqlDataReader.GetString(2);
                    user.Email = sqlDataReader.GetString(3);
                    user.PasswordHash = sqlDataReader.GetString(4);
                    user.Status = sqlDataReader.GetBoolean(5);
                    user.CreatedDate = sqlDataReader.GetDateTime(6);
                    user.IdRole = sqlDataReader.GetInt32(7);
                }
            }
            return user;
        }

        public bool Update(User entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "UPDATE Users SET FirstName=@FirstName, LastName=@LastName, " +
                    "Email=@Email, PasswordHash=@PasswordHash, Status=@Status, IdRole=@IdRole " +
                    "WHERE IdUser=@IdUser";
                sqlCommand.Parameters.AddWithValue("@FirstName", entity.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", entity.LastName);
                sqlCommand.Parameters.AddWithValue("@Email", entity.Email);
                sqlCommand.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
                sqlCommand.Parameters.AddWithValue("@Status", entity.Status);
                sqlCommand.Parameters.AddWithValue("@IdRole", entity.IdRole);
                sqlCommand.Parameters.AddWithValue("@IdUser", entity.IdUser);

                int res = sqlCommand.ExecuteNonQuery();
                return res > 0;
            }
        }
    }
}
