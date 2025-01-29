namespace QuizAPI.Models
{
    public class QuestionsModel
    {
        public int QuestionID { get; set; }
        public int SubtopicID { get; set; }
        public int LevelID { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public string Options1 { get; set; }
        public string Options2 { get; set; }
        public string Options3 { get; set; }
        public string Options4 { get; set; }
        public string Options5 { get; set; }
        public int CorrectOption { get; set; }
        public string CorrectAnswer { get; set; }
        public decimal Mark { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
