using QuizAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace QuizAPI.Data
{
    public class QuizzesRepository
    {
        #region Connection
        private readonly string _connectionString;
        public QuizzesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region GetALL Quizzes
        public IEnumerable<QuizzesModel> SelectAll()
        {
            var quizzes = new List<QuizzesModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Quizzes_SelectAll]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    quizzes.Add(new QuizzesModel
                    {
                        QuizID = Convert.ToInt32(reader["QuizID"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        QuizName = reader["QuizName"].ToString(),
                        LevelID = Convert.ToInt32(reader["LevelID"]),
                        LevelName = reader["LevelName"].ToString(),
                        SubtopicID = Convert.ToInt32(reader["SubtopicID"]),
                        SubtopicName = reader["SubtopicName"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        Time = Convert.ToInt32(reader["Time"])
                    });
                }
            }
            return quizzes;
        }
        #endregion

        #region GetByID Quizzes
        public QuizzesModel SelectByPK(int QuizID)
        {
            QuizzesModel quiz = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Quizzes_SelectByPK]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@QuizID", QuizID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    quiz = new QuizzesModel
                    {
                        QuizID = Convert.ToInt32(reader["QuizID"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        QuizName = reader["QuizName"].ToString(),
                        LevelID = Convert.ToInt32(reader["LevelID"]),
                        SubtopicID = Convert.ToInt32(reader["SubtopicID"]),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        Time = Convert.ToInt32(reader["Time"])
                    };
                }
            }
            return quiz;
        }
        #endregion

        #region Insert Quizzes
        public bool InsertQuiz(QuizzesModel quiz)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Quizzes_Insert]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@UserID", quiz.UserID);
                command.Parameters.AddWithValue("@QuizName", quiz.QuizName);
                command.Parameters.AddWithValue("@LevelID", quiz.LevelID);
                command.Parameters.AddWithValue("@SubtopicID", quiz.SubtopicID);
                command.Parameters.AddWithValue("@Time", quiz.Time);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update Quizzes
        public bool UpdateQuiz(QuizzesModel quiz)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Quizzes_Update]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@QuizID", quiz.QuizID);
                command.Parameters.AddWithValue("@UserID", quiz.UserID);
                command.Parameters.AddWithValue("@QuizName", quiz.QuizName);
                command.Parameters.AddWithValue("@LevelID", quiz.LevelID);
                command.Parameters.AddWithValue("@SubtopicID", quiz.SubtopicID);
                command.Parameters.AddWithValue("@CreatedAt", quiz.CreatedAt);
                command.Parameters.AddWithValue("@Time", quiz.Time);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Delete Quizzes
        public bool DeleteQuiz(int QuizID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Quizzes_Delete]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@QuizID", QuizID);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region QuizCount
        public int GetTotalQuizzesCount()
        {
            int count = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("[dbo].[PR_Quizzes_Count]", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    connection.Open();
                    count = (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching total quizzes: {ex.Message}");
            }
            return count;
        }

        #endregion

        #region GetLevel
        public IEnumerable<LevelsModel> GetLevels()
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

        #region GetUsers
        public IEnumerable<UserDropDownModel> GetUsers()
        {
            var users = new List<UserDropDownModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_User_DropDown]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new UserDropDownModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Username = reader["Username"].ToString()
                    });
                }
            }
            return users;
        }
        #endregion

        #region GetSubtopics
        public IEnumerable<SubTopicDropDownModel> GetSubtopics()
        {
            var subtopic = new List<SubTopicDropDownModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Subtopics_DropDown]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    subtopic.Add(new SubTopicDropDownModel
                    {
                        SubtopicID = Convert.ToInt32(reader["SubtopicID"]),
                        SubtopicName = reader["SubtopicName"].ToString()
                    });
                }
            }
            return subtopic;
        }
        #endregion


    }
}
