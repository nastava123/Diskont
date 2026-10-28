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
            throw new NotImplementedException();
        }

        public bool Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public User? Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            List<User> lista = new List<User>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
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
            throw new NotImplementedException();
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
