using System.Data.SqlClient;
using UserManagement.Entity;

namespace UserManagement.DAL
{
    public class DataAccessLayer
    {
        private string connectionString = "Server=.;Database=UserMohaymenTest;Trusted_Connection=True;";
        public bool AddUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Users (Username, Password, Status) VALUES (@Username, @Password, @Status)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@Status", user.Status);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0; 
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }
        public User GetUserByUsername(string username)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                Status = reader["Status"].ToString()
                            };
                        }
                    }
                }
            }
            return null; 
        }
        public bool UpdateUserStatus(User user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Users SET Status = @Status WHERE Username = @Username";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", user.Status);
                        cmd.Parameters.AddWithValue("@Username", user.Username);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }
        public List<User> SearchUsersByUsername(string partialUsername)
        {
            List<User> users = new List<User>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE Username LIKE @PartialUsername";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PartialUsername", partialUsername + "%"); 
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                Status = reader["Status"].ToString()
                            };
                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }
        public bool ChangeUserPassword(string username, string oldPassword, string newPassword)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Users SET Password = @NewPassword WHERE Username = @Username AND Password = @OldPassword";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@OldPassword", oldPassword);
                        cmd.Parameters.AddWithValue("@NewPassword", newPassword);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (SqlException ex)
                {

                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }


    }
}
