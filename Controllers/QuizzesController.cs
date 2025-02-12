using Microsoft.AspNetCore.Mvc;
using QuizAPI.Data;
using QuizAPI.Models;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        #region Connection
        private readonly QuizzesRepository _repository;
        public QuizzesController(QuizzesRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region GetALL Quizzes
        [HttpGet]
        public IActionResult GetAll()
        {
            var quizzes = _repository.SelectAll();
            return Ok(quizzes);
        }
        #endregion

        #region GetALL_Count
        [HttpGet("count")]
        public IActionResult GetQuizesCount()
        {
            try
            {
                int totalQuizes = _repository.SelectAll().Count();
                return Ok(new { totalQuizes });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error", error = ex.Message });
            }
        }

        #endregion

        #region GetByID Quizzes
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var quiz = _repository.SelectByPK(id);
            if (quiz == null)
                return NotFound();
            return Ok(quiz);
        }
        #endregion

        #region Insert Quizzes
        [HttpPost]
        public IActionResult Create([FromBody] QuizzesModel quiz)
        {
            if (quiz == null)
                return BadRequest(new { Message = "Invalid quiz data" });

            var insertQuiz = new QuizzesModel
            {
                UserID = quiz.UserID,
                QuizName = quiz.QuizName,
                LevelID = quiz.LevelID,
                SubtopicID = quiz.SubtopicID,
                Time = quiz.Time
            };

            if (_repository.InsertQuiz(insertQuiz))
                return Ok(new { Message = "Quiz created successfully" });

            return BadRequest(new { Message = "Failed to create quiz" });
        }
        #endregion

        #region Update Quizzes

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] QuizzesModel quiz)
        {
            if (id != quiz.QuizID)
                return BadRequest(new { Message = "Quiz ID mismatch" });

            var updatedQuiz = new QuizzesModel
            {
                QuizID = quiz.QuizID,
                UserID = quiz.UserID,
                QuizName = quiz.QuizName,
                LevelID = quiz.LevelID,
                SubtopicID = quiz.SubtopicID,
                CreatedAt = quiz.CreatedAt,
                Time = quiz.Time
            };

            if (_repository.UpdateQuiz(updatedQuiz))
                return Ok(new { Message = "Quiz updated successfully" });

            return BadRequest(new { Message = "Failed to update quiz" });
        }

        #endregion

        #region Delete Quizzes

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_repository.DeleteQuiz(id))
                return Ok(new { Message = "Quiz deleted successfully" });
            return BadRequest(new { Message = "Failed to delete quiz" });
        }
        #endregion

        #region GetLevels
        [HttpGet("levels")]
        public IActionResult GetLevels()
        {
            var levels = _repository.GetLevels();
            if (!levels.Any())
            {
                return NotFound("No Levels Found");
            }
            return Ok(levels);
        }
        #endregion

        #region GetUsers
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _repository.GetUsers();
            if (!users.Any())
            {
                return NotFound("No Users Found");
            }
            return Ok(users);
        }
        #endregion

        #region GetSubtopics
        [HttpGet("subtopic")]
        public IActionResult GetSubtopics()
        {
            var subtopic = _repository.GetSubtopics();
            if (!subtopic.Any())
            {
                return NotFound("No Subtopic Found");
            }
            return Ok(subtopic);
        }
        #endregion
    }
}
