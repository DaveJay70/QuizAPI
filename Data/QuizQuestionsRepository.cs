using QuizAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace QuizAPI.Data
{
    public class QuizQuestionsRepository
    {
        #region Connection
        private readonly string _connectionString;
        public QuizQuestionsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region GetALL QuizQuestions
        public IEnumerable<QuizQuestionsModel> SelectAll()
        {
            var quizQuestions = new List<QuizQuestionsModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_QuizQuestions_SelectAll]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    quizQuestions.Add(new QuizQuestionsModel
                    {
                        QuizQuestionID = Convert.ToInt32(reader["QuizQuestionID"]),
                        QuizID = Convert.ToInt32(reader["QuizID"]),
                        QuestionID = Convert.ToInt32(reader["QuestionID"]),
                        QuestionText = reader["QuestionText"].ToString(),
                        QuizName = reader["QuizName"].ToString()
                    });
                }
            }
            return quizQuestions;
        }
        #endregion

        #region GetByID QuizQuestions
        public QuizQuestionsModel SelectByPK(int QuizQuestionID)
        {
            QuizQuestionsModel quizQuestion = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_QuizQuestions_SelectByPK]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@QuizQuestionID", QuizQuestionID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    quizQuestion = new QuizQuestionsModel
                    {
                        QuizQuestionID = Convert.ToInt32(reader["QuizQuestionID"]),
                        QuizID = Convert.ToInt32(reader["QuizID"]),
                        QuestionID = Convert.ToInt32(reader["QuestionID"])
                    };
                }
            }
            return quizQuestion;
        }
        #endregion

        #region GetQuizQuestionByQuizID
        public QuizQuestionsModel GetQuizQuestionByQuizID(int quizId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_QuizQuestions_SelectByQuizID]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@QuizID", quizId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new QuizQuestionsModel
                    {
                        QuizQuestionID = Convert.ToInt32(reader["QuizQuestionID"]),
                        QuizID = Convert.ToInt32(reader["QuizID"]),
                        QuestionID = Convert.ToInt32(reader["QuestionID"]),
                        QuizName = reader["QuizName"].ToString(),
                        QuestionCount = Convert.ToInt32(reader["QuestionCount"]) // ✅ Added QuestionCount
                    };
                }
            }
            return null; // 🔹 Return null if no record found
        }
        #endregion

        #region InsertQuizQuestions
        public int InsertQuizQuestion(QuizQuestionsModel quizQuestion)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Check if QuestionID exists before inserting
                SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM Questions WHERE QuestionID = @QuestionID", connection);
                checkCommand.Parameters.AddWithValue("@QuestionID", quizQuestion.QuestionID);

                connection.Open();
                int questionExists = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (questionExists == 0)
                {
                    throw new Exception($"QuestionID {quizQuestion.QuestionID} does not exist.");
                }

                // ✅ Define stored procedure command
                SqlCommand command = new SqlCommand("[dbo].[PR_QuizQuestions_Insert]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@QuizID", quizQuestion.QuizID);
                command.Parameters.AddWithValue("@QuestionID", quizQuestion.QuestionID);

                // ✅ Add output parameter to get inserted ID
                SqlParameter outputIdParam = new SqlParameter("@InsertedID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputIdParam);

                command.ExecuteNonQuery();
                connection.Close();

                return (int)outputIdParam.Value; // ✅ Return inserted ID
            }
        }

        #endregion

        #region UpdateQuizQuestions
        public bool UpdateQuizQuestion(QuizQuestionsModel quizQuestion)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_QuizQuestions_Update]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@QuizQuestionID", quizQuestion.QuizQuestionID);
                command.Parameters.AddWithValue("@QuizID", quizQuestion.QuizID);
                command.Parameters.AddWithValue("@QuestionID", quizQuestion.QuestionID);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region DeleteQuizQuestions
        public bool DeleteQuizQuestion(int QuizQuestionID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_QuizQuestions_Delete]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@QuizQuestionID", QuizQuestionID);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region QuizDropDown
        public IEnumerable<QuizDropDownModel> GetQuiz()
        {
            var quiz = new List<QuizDropDownModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Quizzes_DropDown]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    quiz.Add(new QuizDropDownModel
                    {
                        QuizID = Convert.ToInt32(reader["QuizID"]),
                        QuizName = reader["QuizName"].ToString()
                    });
                }
            }
            return quiz;
        }
        #endregion

        #region SelectByQuizId
        public IEnumerable<QuizQuestionsModel> SelectByQuizId(int quizId)
        {
            var quizQuestions = new List<QuizQuestionsModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_QuizQuestions_SelectByQuizID]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@QuizID", quizId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    quizQuestions.Add(new QuizQuestionsModel
                    {
                        QuizQuestionID = Convert.ToInt32(reader["QuizQuestionID"]),
                        QuizID = Convert.ToInt32(reader["QuizID"]),
                        QuestionID = Convert.ToInt32(reader["QuestionID"]),
                        QuizName = reader["QuizName"].ToString()
                    });
                }
            }
            return quizQuestions;
        }
        #endregion

        #region Get Question Count By QuizID
        public int GetQuestionCountByQuizId(int quizId)
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_QuizQuestions_CountByQuizID]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@QuizID", quizId);
                connection.Open();
                count = Convert.ToInt32(command.ExecuteScalar());
            }
            return count;
        }
        #endregion

    }
}
