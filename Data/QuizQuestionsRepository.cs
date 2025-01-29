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
                        QuestionID = Convert.ToInt32(reader["QuestionID"])
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

        #region InsertQuizQuestions
        public bool InsertQuizQuestion(QuizQuestionsModel quizQuestion)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_QuizQuestions_Insert]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@QuizID", quizQuestion.QuizID);
                command.Parameters.AddWithValue("@QuestionID", quizQuestion.QuestionID);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
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
    }
}
