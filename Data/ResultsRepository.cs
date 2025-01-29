using QuizAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace QuizAPI.Data
{
    public class ResultsRepository
    {
        #region Connection
        private readonly string _connectionString;
        public ResultsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region GetALL Results
        public IEnumerable<ResultModel> SelectAll()
        {
            var results = new List<ResultModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Results_SelectAll]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new ResultModel
                    {
                        ResultID = Convert.ToInt32(reader["ResultID"]),
                        QuizID = reader["QuizID"] as int?,
                        Score = Convert.ToDecimal(reader["Score"]),
                        TimeTaken = Convert.ToDateTime(reader["TimeTaken"])
                    });
                }
            }
            return results;
        }
        #endregion

        #region GetByID Results
        public ResultModel SelectByPK(int ResultID)
        {
            ResultModel result = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Results_SelectByPK]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResultID", ResultID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    result = new ResultModel
                    {
                        ResultID = Convert.ToInt32(reader["ResultID"]),
                        QuizID = reader["QuizID"] as int?,
                        Score = Convert.ToDecimal(reader["Score"]),
                        TimeTaken = Convert.ToDateTime(reader["TimeTaken"])
                    };
                }
            }
            return result;
        }
        #endregion

        #region Insert Results
        public bool InsertResult(ResultModel result)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Results_Insert]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@QuizID", result.QuizID ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Score", result.Score);
                command.Parameters.AddWithValue("@TimeTaken", result.TimeTaken);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update Results
        public bool UpdateResult(ResultModel result)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Results_Update]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResultID", result.ResultID);
                command.Parameters.AddWithValue("@QuizID", result.QuizID ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Score", result.Score);
                command.Parameters.AddWithValue("@TimeTaken", result.TimeTaken);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Delete Results
        public bool DeleteResult(int ResultID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Results_Delete]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ResultID", ResultID);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion
    }
}
