namespace QuizAPI.Models
{
    public class TopicsModel
    {
        public int TopicID { get; set; }
        public string TopicName { get; set; }
        public bool IsActive { get; set; }
    }
    public class TopicDropDownModel
    {
        public int TopicID { get; set; }
        public string TopicName { get; set; }

    }
}

