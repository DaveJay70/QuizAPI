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

        #region GetQuizQuestion
        [HttpGet("{id}")]
        public ActionResult<QuizQuestionsModel> GetQuizQuestion(int id)
        {
            var quizQuestion = _quizQuestionsRepository.SelectByPK(id); // ✅ Fetch from repository

            if (quizQuestion == null)
            {
                return NotFound(new { message = "Quiz question not found." });
            }

            return Ok(quizQuestion);
        }
        #endregion

        #region Create Question
        [HttpPost]
        public ActionResult CreateQuizQuestion([FromBody] QuizQuestionsModel quizQuestion)
        {
            try
            {
                // Validate input
                if (quizQuestion.QuizID <= 0 || quizQuestion.QuestionID <= 0)
                {
                    return BadRequest(new { message = "Invalid QuizID or QuestionID." });
                }

                // Check if the question already exists for the selected quiz
                var existingQuizQuestions = _quizQuestionsRepository.SelectByQuizId(quizQuestion.QuizID);
                if (existingQuizQuestions.Any(q => q.QuestionID == quizQuestion.QuestionID))
                {
                    return BadRequest(new { message = "This question already exists in the selected quiz." });
                }

                // Insert into database
                int insertedId = _quizQuestionsRepository.InsertQuizQuestion(quizQuestion);

                if (insertedId > 0)
                {
                    return CreatedAtAction(nameof(GetQuizQuestion), new { id = insertedId }, quizQuestion);
                }

                return BadRequest("Insertion failed.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
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

        #region GetQuiz
        [HttpGet("quizzes")]
        public IActionResult GetQuiz()
        {
            var quiz = _quizQuestionsRepository.GetQuiz();
            if (!quiz.Any())
            {
                return NotFound("No Quiz Found");
            }
            return Ok(quiz);
        }
        #endregion

        #region GetQuizQuestionsByQuizId

        [HttpGet("quiz/{quizId}")]
        public ActionResult<IEnumerable<QuizQuestionsModel>> GetQuizQuestionsByQuizId(int quizId)
        {
            var quizQuestions = _quizQuestionsRepository.SelectByQuizId(quizId);
            if (quizQuestions == null || !quizQuestions.Any())
                return NotFound("No questions found for this quiz.");

            return Ok(quizQuestions);
        }
        #endregion

        #region GetQuestionCountByQuizId

        [HttpGet("count/{quizId}")]
        public ActionResult<int> GetQuestionCountByQuizId(int quizId)
        {
            int count = _quizQuestionsRepository.GetQuestionCountByQuizId(quizId);
            return Ok(count);
        }
        #endregion

    }
}
