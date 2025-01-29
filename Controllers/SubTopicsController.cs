using Microsoft.AspNetCore.Mvc;
using QuizAPI.Data;
using QuizAPI.Models;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubtopicsController : ControllerBase
    {
        #region Connection
        private readonly SubTopicsRepository _repository;
        public SubtopicsController(SubTopicsRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region GetALL SubTopics
        [HttpGet]
        public IActionResult GetAll()
        {
            var subtopics = _repository.SelectAll();
            return Ok(subtopics);
        }
        #endregion

        #region GetByID SubTopics

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var subtopic = _repository.SelectByPK(id);
            if (subtopic == null)
                return NotFound();
            return Ok(subtopic);
        }
        #endregion

        #region Insert SubTopics

        [HttpPost]
        public IActionResult Create([FromBody] SubTopicsModel subtopic)
        {
            if (_repository.InsertSubtopic(subtopic))
                return Ok(new { Message = "Subtopic created successfully" });
            return BadRequest(new { Message = "Failed to create subtopic" });
        }
        #endregion

        #region Update SubTopics

        [HttpPut("{id}")]
        public IActionResult Update(int id ,[FromBody] SubTopicsModel subtopic)
        {
            if (id != subtopic.SubtopicID)
                return BadRequest();
            if (_repository.UpdateSubtopic(subtopic))
                return Ok(new { Message = "Subtopic updated successfully" });
            return BadRequest(new { Message = "Failed to update subtopic" });
        }
        #endregion

        #region Delete SubTopics

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_repository.DeleteSubtopic(id))
                return Ok(new { Message = "Subtopic deleted successfully" });
            return BadRequest(new { Message = "Failed to delete subtopic" });
        }
        #endregion
    }
}
