using Microsoft.AspNetCore.Mvc;
using QuizAPI.Data;
using QuizAPI.Models;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Connection
        private readonly UsersRepository _repository;
        public UsersController(IConfiguration configuration)
        {
            _repository = new UsersRepository(configuration);
        }
        #endregion

        #region GetALL Users

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _repository.SelectAll();
            return Ok(users);
        }
        #endregion

        #region GetByID Users

        [HttpGet("{id}")]
        public IActionResult GetUserByID(int id)
        {
            var user = _repository.SelectByPK(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
        #endregion

        #region Insert Users

        [HttpPost]
        public IActionResult AddUser([FromBody] UsersModel user)
        {
            if (_repository.InsertUser(user))
                return Ok(new { message = "User added successfully" });
            return BadRequest(new { message = "Failed to add user" });
        }
        #endregion

        #region Update Users

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id,[FromBody] UsersModel user)
        {
            if (id != user.UserID)
                return BadRequest();
            if (_repository.UpdateUser(user))
                return Ok(new { message = "User updated successfully" });
            return BadRequest(new { message = "Failed to update user" });
        }
        #endregion

        #region Delete Users

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (_repository.DeleteUser(id))
                return Ok(new { message = "User deleted successfully" });
            return BadRequest(new { message = "Failed to delete user" });
        }
        #endregion
    }
}
