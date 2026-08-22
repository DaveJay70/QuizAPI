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

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SubTopicsModel subtopic)
        {
            if (subtopic == null)
                return BadRequest(new { Message = "Invalid request body" });

            if (id != subtopic.SubtopicID)
                return BadRequest(new { Message = "ID mismatch" });

            bool isUpdated = _repository.UpdateSubtopic(subtopic);

            if (isUpdated)
                return Ok(new { Message = "Subtopic updated successfully" });

            return BadRequest(new { Message = "Failed to update subtopic" });
        }
        #region Delete SubTopics

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_repository.DeleteSubtopic(id))
                return Ok(new { Message = "Subtopic deleted successfully" });
            return BadRequest(new { Message = "Failed to delete subtopic" });
        }
        #endregion

        #region GetSubtopicsByTopicId
        [HttpGet("ByTopic/{topicId}")]
        public IActionResult GetByTopicId(int topicId)
        {
            var subtopics = _repository.SelectByTopicId(topicId);
            if (!subtopics.Any())
            {
                return Ok(new List<SubTopicsModel>());
            }
            return Ok(subtopics);
        }
        #endregion

        #region GetSubtopics
        [HttpGet("topic")]
        public IActionResult GetTopics()
        {
            var topic = _repository.GetTopics();
            if (!topic.Any())
            {
                return NotFound("No Topic Found");
            }
            return Ok(topic);
        }
        #endregion

        [HttpPut("Toggle/{id}")]
        public IActionResult ToggleIsActive(int id)
        {
            var subtopic = _repository.SelectByPK(id);
            if (subtopic == null)
                return NotFound();

            bool newStatus = !subtopic.IsActive;
            if (_repository.ToggleIsActive(id, newStatus))
                return Ok(new { Message = "Subtopic status updated successfully" });

            return BadRequest(new { Message = "Failed to update subtopic status" });
        }
    }
}
