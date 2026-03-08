using QuizAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuizAPI.Data
{
    public class UsersRepository
    {
        #region Connection

        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public UsersRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region GetALL Users

        public IEnumerable<UsersModel> SelectAll()
        {
            var users = new List<UsersModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Users_SelectAll]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new UsersModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString(),
                        Role = reader["Role"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                    });
                }
            }
            return users;
        }
        #endregion

        #region GetByID Users

        public UsersModel SelectByPK(int userID)
        {
            UsersModel user = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Users_SelectByPK]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user = new UsersModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString(),
                        Role = reader["Role"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                    };
                }
            }
            return user;
        }
        #endregion

        #region Insert Users
        public bool InsertUser(UsersModel user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Users_Insert]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Role", user.Role);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update Users

        public bool UpdateUser(UsersModel user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Users_Update]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@UserID", user.UserID);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Delete Users
        public bool DeleteUser(int userID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Users_Delete]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Usercount
        public int GetTotalUsersCount()
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Users_Count]", connection);
                connection.Open();
                count = (int)command.ExecuteScalar();
            }
            return count;
        }

        #endregion

        #region RegisterUser
        public bool RegisterUser(UsersModel user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Users_Register]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Usercount
        public int GetTotalUsersCount()
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Users_Count]", connection);
                connection.Open();
                count = (int)command.ExecuteScalar();
            }
            return count;
        }

        #endregion

        #region RegisterUser
        public bool RegisterUser(UsersModel user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Users_Register]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Login
        public UsersModel Login(string email, string password)
        {
            UsersModel user = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Users_CheckLogin", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new UsersModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        Role = reader["Role"].ToString(),
                    };
                }
            }
            return user;
        }

        public string AuthenticateUser(string email, string password)
        {
            var user = Login(email, password);
            if (user == null)
                return null;
            return GenerateJwtToken(user);
        }
        #endregion

        #region Generate JWT Token
        private string GenerateJwtToken(UsersModel user)
        {
            var secretKey = _configuration["Jwt:SecretKey"];
            if (string.IsNullOrEmpty(secretKey) || secretKey.Length < 32)
            {
                throw new Exception("JWT Secret Key must be at least 32 characters long.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserID.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Username", user.Username),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

        
    }
}
