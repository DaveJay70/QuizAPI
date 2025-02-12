using QuizAPI.Data;
using QuizAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

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

        #region GetALL Questions
        [HttpGet]
        public IActionResult SelectAllQuestions()
        {
            var questions = _questionsRepository.SelectAll();
            return Ok(questions);
        }
        #endregion

        #region GetByID Questions
        [HttpGet("{id}")]
        public IActionResult GetQuestionById(int id)
        {
            var question = _questionsRepository.SelectByPK(id);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }
        #endregion

        #region Insert Questions
        [HttpPost]
        public IActionResult InsertQuestion([FromBody] QuestionsModel question)
        {
            if (question == null)
            {
                return BadRequest();
            }
            bool isInserted = _questionsRepository.InsertQuestion(question);
            if (isInserted)
            {
                return Ok(new { Message = "Question Inserted" });
            }
            return StatusCode(500, "An error occurred while inserting the question");
        }
        #endregion

        #region Update Questions
        [HttpPut("{id}")]
        public IActionResult UpdateQuestion(int id, [FromBody] QuestionsModel question)
        {
            if (question == null || id != question.QuestionID)
            {
                return BadRequest();
            }
            var isUpdated = _questionsRepository.UpdateQuestion(question);
            if (!isUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region Delete Questions

        [HttpDelete("{id}")]
        public ActionResult DeleteQuestion(int id)
        {
            try
            {
                if (_questionsRepository.Delete(id))
                    return NoContent();

                return NotFound("Question not found.");
            }
            catch (SqlException ex) when (ex.Number == 547) 
            {
                return Conflict(new { Error = "Cannot delete question because it is used in quizzes.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while deleting the question.", Details = ex.Message });
            }
        }

        #endregion

        #region GetLevels
        [HttpGet("levels")]
        public IActionResult GetLevels()
        {
            var levels = _questionsRepository.GetLevels();
            if (!levels.Any())
            {
                return NotFound("No Levels Found");
            }
            return Ok(levels);
        }
        #endregion

        #region GetSubtopics
        [HttpGet("subtopic")]
        public IActionResult GetSubtopics()
        {
            var subtopic = _questionsRepository.GetSubtopics();
            if (!subtopic.Any())
            {
                return NotFound("No Subtopic Found");
            }
            return Ok(subtopic);
        }
        #endregion

        #region GetALL_Count
        [HttpGet("count")]
        public IActionResult GetAllCount()
        {
            try
            {
                int questions = _questionsRepository.GetTotalQuestionsCount();
                return Ok(new { Questions_Count = questions });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to fetch questions count", Details = ex.Message });
            }
        }

        #endregion
    }
}
