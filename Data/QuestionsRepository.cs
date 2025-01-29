using QuizAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace QuizAPI.Data
{
    public class QuestionsRepository
    {
        #region Connection
        private readonly string _connectionString;
        public QuestionsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region GetALL Questions
        public IEnumerable<QuestionsModel> SelectAll()
        {
            var questions = new List<QuestionsModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Questions_SelectAll]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    questions.Add(new QuestionsModel
                    {
                        QuestionID = Convert.ToInt32(reader["QuestionID"]),
                        SubtopicID = Convert.ToInt32(reader["SubtopicID"]),
                        LevelID = Convert.ToInt32(reader["LevelID"]),
                        QuestionText = reader["QuestionText"].ToString(),
                        QuestionType = reader["QuestionType"].ToString(),
                        Options1 = reader["Options1"].ToString(),
                        Options2 = reader["Options2"].ToString(),
                        Options3 = reader["Options3"].ToString(),
                        Options4 = reader["Options4"].ToString(),
                        Options5 = reader["Options5"].ToString(),
                        CorrectOption = Convert.ToInt32(reader["CorrectOption"]),
                        CorrectAnswer = reader["CorrectAnswer"].ToString(),
                        Mark = Convert.ToInt32(reader["Mark"]),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                    });
                }
            }
            return questions;
        }
        #endregion

        #region GetByID Questions
        public QuestionsModel SelectByPK(int QuestionID)
        {
            QuestionsModel question = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Questions_SelectByPK]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@QuestionID", QuestionID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    question = new QuestionsModel
                    {
                        QuestionID = Convert.ToInt32(reader["QuestionID"]),
                        SubtopicID = Convert.ToInt32(reader["SubtopicID"]),
                        LevelID = Convert.ToInt32(reader["LevelID"]),
                        QuestionText = reader["QuestionText"].ToString(),
                        QuestionType = reader["QuestionType"].ToString(),
                        Options1 = reader["Options1"].ToString(),
                        Options2 = reader["Options2"].ToString(),
                        Options3 = reader["Options3"].ToString(),
                        Options4 = reader["Options4"].ToString(),
                        Options5 = reader["Options5"].ToString(),
                        CorrectOption = Convert.ToInt32(reader["CorrectOption"]),
                        CorrectAnswer = reader["CorrectAnswer"].ToString(),
                        Mark = Convert.ToInt32(reader["Mark"]),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                    };
                }
            }
            return question;
        }
        #endregion

        #region Insert Questions
        public bool InsertQuestion(QuestionsModel question)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Questions_Insert]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@SubtopicID", question.SubtopicID);
                command.Parameters.AddWithValue("@LevelID", question.LevelID);
                command.Parameters.AddWithValue("@QuestionText", question.QuestionText);
                command.Parameters.AddWithValue("@QuestionType", question.QuestionType);
                command.Parameters.AddWithValue("@Options1", question.Options1);
                command.Parameters.AddWithValue("@Options2", question.Options2);
                command.Parameters.AddWithValue("@Options3", question.Options3);
                command.Parameters.AddWithValue("@Options4", question.Options4);
                command.Parameters.AddWithValue("@Options5", question.Options5);
                command.Parameters.AddWithValue("@CorrectOption", question.CorrectOption);
                command.Parameters.AddWithValue("@CorrectAnswer", question.CorrectAnswer);
                command.Parameters.AddWithValue("@Mark", question.Mark);
                command.Parameters.AddWithValue("@CreatedAt", question.CreatedAt);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update Questions
        public bool UpdateQuestion(QuestionsModel question)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Questions_Update]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@QuestionID", question.QuestionID);
                command.Parameters.AddWithValue("@SubtopicID", question.SubtopicID);
                command.Parameters.AddWithValue("@LevelID", question.LevelID);
                command.Parameters.AddWithValue("@QuestionText", question.QuestionText);
                command.Parameters.AddWithValue("@QuestionType", question.QuestionType);
                command.Parameters.AddWithValue("@Options1", question.Options1);
                command.Parameters.AddWithValue("@Options2", question.Options2);
                command.Parameters.AddWithValue("@Options3", question.Options3);
                command.Parameters.AddWithValue("@Options4", question.Options4);
                command.Parameters.AddWithValue("@Options5", question.Options5);
                command.Parameters.AddWithValue("@CorrectOption", question.CorrectOption);
                command.Parameters.AddWithValue("@CorrectAnswer", question.CorrectAnswer);
                command.Parameters.AddWithValue("@Mark", question.Mark);
                command.Parameters.AddWithValue("@CreatedAt", question.CreatedAt);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Delete Questions
        public bool Delete(int QuestionID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Questions_Delete]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@QuestionID", QuestionID);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion
    }
}
