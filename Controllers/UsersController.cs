using Microsoft.AspNetCore.Mvc;
using QuizAPI.Data;
using QuizAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        #region Connection
        private readonly UsersRepository _repository;
        private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
            _repository = new UsersRepository(configuration);
            _configuration = configuration;
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

        #region GetALL_Count
        [HttpGet("count")]
        public IActionResult GetCustomerCount()
        {
            try
            {
                int totalUsers = _repository.SelectAll().Count();
                return Ok(new { totalUsers });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error", error = ex.Message });
            }
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
        public IActionResult UpdateUser(int id, [FromBody] UsersModel user)
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

        #region Register User
        [HttpPost("register")]
        public IActionResult Register_User([FromBody] UsersModel user)
        {
            if (_repository.RegisterUser(user))
                return Ok(new { message = "User Register successfully" });
            return BadRequest(new { message = "Failed to register user" });
        }
        #endregion

        #region Login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
                return BadRequest("Email and Password are required.");

            var user = _repository.Login(login.Email, login.Password);
            if (user == null)
                return Unauthorized("Invalid email or password.");

            var token = GenerateJwtToken(user);

            return Ok(new
            {
                message = "Login successfully",
                UserID = user.UserID,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                Token = token
            });
        }
        #endregion

        #region Generate JWT Token
        private string GenerateJwtToken(UsersModel user)
        {
            var secretKey = _configuration["Jwt:SecretKey"];
            if (string.IsNullOrEmpty(secretKey) || secretKey.Length < 32)
            {
                throw new Exception("JWT Secret Key must be at least 32 characters long.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserID.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Username", user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

    }
}
