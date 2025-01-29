using QuizAPI.Models;
using QuizAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizQuestionsController : ControllerBase
    {
        #region Connection
        private readonly QuizQuestionsRepository _quizQuestionsRepository;
        public QuizQuestionsController(QuizQuestionsRepository quizQuestionsRepository)
        {
            _quizQuestionsRepository = quizQuestionsRepository;
        }
        #endregion

        #region GetALL QuizQuestions
        [HttpGet]
        public ActionResult<IEnumerable<QuizQuestionsModel>> GetQuizQuestions()
        {
            return Ok(_quizQuestionsRepository.SelectAll());
        }
        #endregion

        #region GetByID QuizQuestions

        [HttpGet("{id}")]
        public ActionResult<QuizQuestionsModel> GetQuizQuestion(int id)
        {
            var quizQuestion = _quizQuestionsRepository.SelectByPK(id);
            if (quizQuestion == null)
                return NotFound();
            return Ok(quizQuestion);
        }
        #endregion

        #region InsertQuizQuestions
        [HttpPost]
        public ActionResult CreateQuizQuestion([FromBody] QuizQuestionsModel quizQuestion)
        {
            if (_quizQuestionsRepository.InsertQuizQuestion(quizQuestion))
                return CreatedAtAction(nameof(GetQuizQuestion), new { id = quizQuestion.QuizQuestionID }, quizQuestion);
            return BadRequest();
        }
        #endregion

        #region UpdateQuizQuestions

        [HttpPut("{id}")]
        public ActionResult UpdateQuizQuestion(int id, [FromBody] QuizQuestionsModel quizQuestion)
        {
            if (id != quizQuestion.QuizQuestionID)
                return BadRequest();
            if (_quizQuestionsRepository.UpdateQuizQuestion(quizQuestion))
                return NoContent();
            return NotFound();
        }
        #endregion

        #region DeleteQuizQuestions

        [HttpDelete("{id}")]
        public ActionResult DeleteQuizQuestion(int id)
        {
            if (_quizQuestionsRepository.DeleteQuizQuestion(id))
                return NoContent();
            return NotFound();
        }
        #endregion
    }
}
