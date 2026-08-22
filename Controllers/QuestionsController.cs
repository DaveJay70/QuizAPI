using QuizAPI.Data;
using QuizAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        #region Connection
        private readonly QuestionsRepository _questionsRepository;

        public QuestionsController(QuestionsRepository questionsRepository)
        {
            _questionsRepository = questionsRepository;
        }
        #endregion

        #region Get All Questions
        [HttpGet]
        public IActionResult GetAllQuestions()
        {
            var questions = _questionsRepository.SelectAll();
            return Ok(questions);
        }
        #endregion

        #region Get Questions Count
        [HttpGet("count")]
        public IActionResult GetQuestionsCount()
        {
            try
            {
                int totalQuestions = _questionsRepository.GetTotalQuestionsCount();
                return Ok(new { totalQuestions });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error", error = ex.Message });
            }
        }
        #endregion

        #region Get Question by ID
        [HttpGet("{id}")]
        public IActionResult GetQuestionById(int id)
        {
            var question = _questionsRepository.SelectByPK(id);
            if (question == null)
            {
                return NotFound(new { Message = "Question not found" });
            }
            return Ok(question);
        }
        #endregion

        #region Insert Question
        [HttpPost]
        public IActionResult InsertQuestion([FromBody] QuestionsModel question)
        {
            if (question == null)
            {
                return BadRequest(new { Error = "Invalid question data" });
            }

            bool isInserted = _questionsRepository.InsertQuestion(question);
            if (isInserted)
            {
                return Ok(new { Message = "Question inserted successfully" });
            }

            return StatusCode(500, new { Error = "An error occurred while inserting the question" });
        }
        #endregion

        #region Update Question
        [HttpPut("{id}")]
        public IActionResult UpdateQuestion(int id, [FromBody] QuestionsModel question)
        {
            if (question == null || id != question.QuestionID)
            {
                return BadRequest(new { Error = "Invalid request data" });
            }

            var isUpdated = _questionsRepository.UpdateQuestion(question);
            if (!isUpdated)
            {
                return NotFound(new { Message = "Question not found" });
            }

            return NoContent();
        }
        #endregion

        #region Delete Question
        [HttpDelete("{id}")]
        public ActionResult DeleteQuestion(int id)
        {
            try
            {
                if (_questionsRepository.Delete(id))
                {
                    return NoContent();
                }

                return NotFound(new { Message = "Question not found" });
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return Conflict(new { Error = "Cannot delete question as it is referenced in quizzes." });
            }
        }
        #endregion

        #region Get Levels
        [HttpGet("levels")]
        public IActionResult GetLevels()
        {
            var levels = _questionsRepository.GetLevels();
            if (!levels.Any())
            {
                return NotFound(new { Message = "No levels found" });
            }
            return Ok(levels);
        }
        #endregion

        #region Get Subtopics
        [HttpGet("subtopics")]
        public IActionResult GetSubtopics()
        {
            var subtopics = _questionsRepository.GetSubtopics();
            if (!subtopics.Any())
            {
                return NotFound(new { Message = "No subtopics found" });
            }
            return Ok(subtopics);
        }
        #endregion

    }
}
