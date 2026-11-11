using DataLayer.Const;
using DataLayer.Interface;
using Entities;
using Microsoft.Data.SqlClient;

namespace DataLayer.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        public bool Add(Role entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "INSERT INTO Roles(Name) values(@Name)";
                sqlCommand.Parameters.AddWithValue("@Name", entity.Name);

                int res = sqlCommand.ExecuteNonQuery();
                return res > 0;
            }
        }

        public bool Delete(Role entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "DELETE FROM Roles WHERE IdRole=@x";
                sqlCommand.Parameters.AddWithValue("@x", entity.IdRole);

                int res = sqlCommand.ExecuteNonQuery();
                return res > 0;
            }
        }

        public Role? Get(int id)
        {
            Role? role = null;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM Roles WHERE IdRole=@x";
                sqlCommand.Parameters.AddWithValue("@x", id);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    role = new Role();
                    role.IdRole = sqlDataReader.GetInt32(0);
                    role.Name = sqlDataReader.GetString(1);
                }
            }
            return role;
        }

        public List<Role> GetAll()
        {
            List<Role> lista = new List<Role>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM Roles";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Role role = new Role();
                    role.IdRole = sqlDataReader.GetInt32(0);
                    role.Name = sqlDataReader.GetString(1);
                    lista.Add(role);
                }
            }
            return lista;
        }

        public bool Update(Role entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "UPDATE Roles SET Name=@Name WHERE IdRole=@IdRole";
                sqlCommand.Parameters.AddWithValue("@Name", entity.Name);
                sqlCommand.Parameters.AddWithValue("@IdRole", entity.IdRole);

                int res = sqlCommand.ExecuteNonQuery();
                return res > 0;
            }
        }
    }
}
