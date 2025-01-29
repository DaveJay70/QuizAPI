namespace QuizAPI.Models
{
    public class ResultModel
    {
        public int ResultID { get; set; }
        public int? QuizID { get; set; }
        public decimal Score { get; set; }
        public DateTime TimeTaken { get; set; }
    }
}
