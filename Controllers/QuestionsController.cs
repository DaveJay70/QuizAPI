using QuizAPI.Data;
using QuizAPI.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult DeleteQuestion(int id)
        {
            var isDeleted = _questionsRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion
    }
}
