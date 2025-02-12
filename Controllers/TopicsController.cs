using Microsoft.AspNetCore.Mvc;
using QuizAPI.Data;
using QuizAPI.Models;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        #region Connection
        private readonly TopicsRepository _repository;
        public TopicsController(IConfiguration configuration)
        {
            _repository = new TopicsRepository(configuration);
        }
        #endregion

        #region GetALL Topics

        [HttpGet]
        public IActionResult GetAllTopics()
        {
            var topics = _repository.SelectAll();
            return Ok(topics);
        }
        #endregion

        #region GetByID Topics

        [HttpGet("{id}")]
        public IActionResult GetTopicByID(int id)
        {
            var topic = _repository.SelectByPK(id);
            if (topic == null)
                return NotFound();
            return Ok(topic);
        }
        #endregion

        #region Insert Topics

        [HttpPost]
        public IActionResult AddTopic([FromBody] TopicsModel topic)
        {
            if (_repository.InsertTopic(topic))
                return Ok(new { message = "Topic added successfully" });
            return BadRequest(new { message = "Failed to add topic" });
        }
        #endregion

        #region Update Topics

        [HttpPut("{id}")]
        public IActionResult UpdateTopic(int id,[FromBody] TopicsModel topic)
        {
            if (id != topic.TopicID)
                return BadRequest();
            if (_repository.UpdateTopic(topic))
                return Ok(new { message = "Topic updated successfully" });
            return BadRequest(new { message = "Failed to update topic" });
        }
        #endregion

        #region Delete Topics

        [HttpDelete("{id}")]
        public IActionResult DeleteTopic(int id)
        {
            if (_repository.DeleteTopic(id))
                return Ok(new { message = "Topic deleted successfully" });
            return BadRequest(new { message = "Failed to delete topic" });
        }
        #endregion

        #region Toggle IsActive Status
        [HttpPut("{id}/toggle")]
        public IActionResult ToggleIsActive(int id)
        {
            var topic = _repository.SelectByPK(id);
            if (topic == null)
                return NotFound();

            bool newStatus = !topic.IsActive; // Toggle the status
            bool isUpdated = _repository.ToggleIsActive(id, topic.IsActive);

            if (isUpdated)
                return Ok(new { message = "Topic status updated successfully" });
            return BadRequest(new { message = "Failed to update topic status" });
        }
        #endregion


    }
}
