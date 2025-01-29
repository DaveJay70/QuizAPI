using QuizAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace QuizAPI.Data
{
    public class TopicsRepository
    {
        #region Connection
        private readonly string _connectionString;
        public TopicsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region GetALL Topics
        public IEnumerable<TopicsModel> SelectAll()
        {
            var topics = new List<TopicsModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Topics_SelectAll]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    topics.Add(new TopicsModel
                    {
                        TopicID = Convert.ToInt32(reader["TopicID"]),
                        TopicName = reader["TopicName"].ToString(),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    });
                }
            }
            return topics;
        }
        #endregion

        #region GetByID Topics
        public TopicsModel SelectByPK(int topicID)
        {
            TopicsModel topic = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Topics_SelectByPK]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@TopicID", topicID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    topic = new TopicsModel
                    {
                        TopicID = Convert.ToInt32(reader["TopicID"]),
                        TopicName = reader["TopicName"].ToString(),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    };
                }
            }
            return topic;
        }
        #endregion

        #region Insert Topics
        public bool InsertTopic(TopicsModel topic)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Topics_Insert]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@TopicName", topic.TopicName);
                command.Parameters.AddWithValue("@IsActive", topic.IsActive);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update Topics
        public bool UpdateTopic(TopicsModel topic)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Topics_Update]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@TopicID", topic.TopicID);
                command.Parameters.AddWithValue("@TopicName", topic.TopicName);
                command.Parameters.AddWithValue("@IsActive", topic.IsActive);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Delete Topics

        public bool DeleteTopic(int topicID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("[dbo].[PR_Topics_Delete]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@TopicID", topicID);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion
    }
}
