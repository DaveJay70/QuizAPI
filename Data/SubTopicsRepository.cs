using QuizAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace QuizAPI.Data
{
    public class SubTopicsRepository
    {
        #region Connection
        private readonly string _connectionString;
        public SubTopicsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region GetALL SubTopics
        public IEnumerable<SubTopicsModel> SelectAll()
        {
            var subtopics = new List<SubTopicsModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("[dbo].[PR_Subtopics_SelectAll]", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        subtopics.Add(new SubTopicsModel
                        {
                            SubtopicID = Convert.ToInt32(reader["SubtopicID"]),
                            TopicID = Convert.ToInt32(reader["TopicID"]),
                            TopicName = reader["TopicName"].ToString(),
                            SubtopicName = reader["SubtopicName"].ToString(),
                            IsActive = Convert.ToBoolean(reader["IsActive"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SelectAll(): {ex.Message}");
            }
            return subtopics;
        }

        #endregion

        #region GetByID SubTopics
        public SubTopicsModel SelectByPK(int SubtopicID)
        {
            SubTopicsModel subtopic = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Subtopics_SelectByPK]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@SubtopicID", SubtopicID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    subtopic = new SubTopicsModel
                    {
                        SubtopicID = Convert.ToInt32(reader["SubtopicID"]),
                        TopicID = Convert.ToInt32(reader["TopicID"]),
                        SubtopicName = reader["SubtopicName"].ToString(),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    };
                }
            }
            return subtopic;
        }
        #endregion

        #region Insert SubTopics
        public bool InsertSubtopic(SubTopicsModel subtopic)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("[dbo].[PR_Subtopics_Insert]", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@TopicID", subtopic.TopicID);
                    command.Parameters.AddWithValue("@SubtopicName", subtopic.SubtopicName);
                    command.Parameters.AddWithValue("@IsActive", subtopic.IsActive);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in InsertSubtopic(): {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Update SubTopics
        public bool UpdateSubtopic(SubTopicsModel subtopic)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Subtopics_Update]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@SubtopicID", subtopic.SubtopicID);
                command.Parameters.AddWithValue("@TopicID", subtopic.TopicID);
                command.Parameters.AddWithValue("@SubtopicName", subtopic.SubtopicName);
                command.Parameters.AddWithValue("@IsActive", subtopic.IsActive);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Delete SubTopics

        public bool DeleteSubtopic(int SubtopicID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Subtopics_Delete]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@SubtopicID", SubtopicID);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region GetTopics
        public IEnumerable<TopicDropDownModel> GetTopics()
        {
            var subtopic = new List<TopicDropDownModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Topic_DropDown]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    subtopic.Add(new TopicDropDownModel
                    {
                        TopicID = Convert.ToInt32(reader["TopicID"]),
                        TopicName = reader["TopicName"].ToString()
                    });
                }
            }
            return subtopic;
        }
        #endregion

        public bool ToggleIsActive(int SubtopicID, bool isActive)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Subtopics_ToggleIsActive]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@SubtopicID", SubtopicID);
                command.Parameters.AddWithValue("@IsActive", isActive);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
