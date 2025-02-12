namespace QuizAPI.Models
{
    public class SubTopicsModel
    {
        public int SubtopicID { get; set; }
        public int TopicID { get; set; }
        public string? TopicName { get; set; }
        public string SubtopicName { get; set; }
        public bool IsActive { get; set; }
    }
    public class SubTopicDropDownModel
    {
        public int SubtopicID { get; set; }
        public string SubtopicName { get; set; }

    }
}
