public class QuizzesModel
{
    public int? QuizID { get; set; }
    public int UserID { get; set; }
    public string QuizName { get; set; }
    public int LevelID { get; set; }
    public int SubtopicID { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Time { get; set; }

    // Only for GetAll, not used in Insert or Update
    public string? UserName { get; set; }
    public string? LevelName { get; set; }
    public string? SubtopicName { get; set; }
}

public class QuizDropDownModel
{
    public int QuizID { get; set; }
    public string QuizName { get; set; }
}


