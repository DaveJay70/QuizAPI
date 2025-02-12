namespace QuizAPI.Models
{
    public class ResultModel
    {
        public int ResultID { get; set; }
        public decimal Score { get; set; }
        public int UserID { get; set; }
        public string? UserName { get; set; }

        public string? QuizName { get; set; }
        public int? QuizID { get; set; }  // Optional, if using QuizID

    }
}
