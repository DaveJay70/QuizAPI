namespace QuizAPI.Models
{
    public class QuizQuestionsModel
    {
        public int QuizQuestionID { get; set; }
        public int QuizID { get; set; }
        public int QuestionID { get; set; }

        public int? QuestionCount { get; set; } 


        public string? QuizName { get; set; }

        public string? QuestionText { get; set; }


    }
}
