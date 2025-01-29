using QuizAPI.Data;
using QuizAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelsController : ControllerBase
    {
        #region Connection
        private readonly LevelsRepository _levelsRepository;

        public LevelsController(LevelsRepository levelsRepository)
        {
            _levelsRepository = levelsRepository;
        }
        #endregion

        #region GetALL Level
        [HttpGet]
        public IActionResult SelectAllLevels()
        {
            var levels = _levelsRepository.SelectAll();
            return Ok(levels);
        }
        #endregion

        #region GetByID Level

        [HttpGet("{id}")]
        public IActionResult GetLevelById(int id)
        {
            var level = _levelsRepository.SelectByPK(id);
            if (level == null)
            {
                return NotFound();
            }
            return Ok(level);
        }
        #endregion

        #region Insert Level

        [HttpPost]
        public IActionResult InsertLevel([FromBody] LevelsModel level)
        {
            if (level == null)
            {
                return BadRequest();
            }
            bool isInserted = _levelsRepository.InsertLevel(level);
            if (isInserted)
            {
                return Ok(new { Message = "Level Inserted" });
            }
            return StatusCode(500, "An error occurred while inserting the level");
        }
        #endregion

        #region UpdateLevel

        [HttpPut("{id}")]
        public IActionResult UpdateLevel(int id, [FromBody] LevelsModel level)
        {
            if (level == null || id != level.LevelID)
            {
                return BadRequest();
            }
            var isUpdated = _levelsRepository.UpdateLevel(level);
            if (!isUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region DeleteLevel

        [HttpDelete("{id}")]
        public IActionResult DeleteLevel(int id)
        {
            var isDeleted = _levelsRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion
    }
}
