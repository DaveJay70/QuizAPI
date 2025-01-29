using QuizAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace QuizAPI.Data
{
    public class LevelsRepository
    {
        #region Connection
        private readonly string _connectionString;
        public LevelsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region GetALL Level
        public IEnumerable<LevelsModel> SelectAll()
        {
            var levels = new List<LevelsModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Levels_SelectAll]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    levels.Add(new LevelsModel
                    {
                        LevelID = Convert.ToInt32(reader["LevelID"]),
                        LevelName = reader["LevelName"].ToString()
                    });
                }
            }
            return levels;
        }
        #endregion

        #region GetByID Level
        public LevelsModel SelectByPK(int LevelID)
        {
            LevelsModel level = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Levels_SelectByPK]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@LevelID", LevelID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    level = new LevelsModel
                    {
                        LevelID = Convert.ToInt32(reader["LevelID"]),
                        LevelName = reader["LevelName"].ToString()
                    };
                }
            }
            return level;
        }
        #endregion

        #region Insert Level
        public bool InsertLevel(LevelsModel level)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Levels_Insert]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@LevelName", level.LevelName);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region UpdateLevel
        public bool UpdateLevel(LevelsModel level)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Levels_Update]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@LevelID", level.LevelID);
                command.Parameters.AddWithValue("@LevelName", level.LevelName);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region DeleteLevel
        public bool Delete(int LevelID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Levels_Delete]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@LevelID", LevelID);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion
    }
}
