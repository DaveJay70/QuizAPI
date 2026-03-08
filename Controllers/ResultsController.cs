using Microsoft.AspNetCore.Mvc;
using QuizAPI.Data;
using QuizAPI.Models;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        #region Connection
        private readonly ResultsRepository _repository;
        public ResultsController(ResultsRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region GetALL Results
        [HttpGet]
        public IActionResult GetAll()
        {
            var results = _repository.SelectAll();
            return Ok(results);
        }
        #endregion

        #region GetALL_Count
        [HttpGet("count")]
        public IActionResult GetAllCount()
        {
            int result = _repository.GetTotalResultsCount();
            return Ok(new { Result_Count = result });

        }
        #endregion

        #region GetByID Results

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _repository.SelectByPK(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        #endregion

        #region Insert Results
        [HttpPost]
        public IActionResult CreateResult([FromBody] ResultModel result)
        {
            if (result == null)
            {
                return BadRequest("Invalid result data.");
            }

            bool isCreated =_repository.InsertResult(result);
            if (isCreated)
            {
                return Ok("Result created successfully.");
            }

            return StatusCode(500, "An error occurred while creating the result.");
        }


        #endregion

        #region Update Results

        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] ResultModel result)
        {
            if (id != result.ResultID)
                return BadRequest();
            if (_repository.UpdateResult(result))
                return Ok(new { Message = "Result updated successfully" });
            return BadRequest(new { Message = "Failed to update result" });
        }
        #endregion

        #region Delete Results

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_repository.DeleteResult(id))
                return Ok(new { Message = "Result deleted successfully" });
            return BadRequest(new { Message = "Failed to delete result" });
        }
        #endregion

        #region GetQuiz
        [HttpGet("quizzes")]
        public IActionResult GetQuiz()
        {
            var quiz = _repository.GetQuiz();
            if (!quiz.Any())
            {
                return NotFound("No Quiz Found");
            }
            return Ok(quiz);
        }
        #endregion
    }
}
