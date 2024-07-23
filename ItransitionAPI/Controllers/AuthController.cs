using ItransitionAPI.Interfaces;
using ItransitionAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ItransitionAPI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(model.Email, model.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            if (user.Status == "Blocked")
            {
                return Forbid("Your account is blocked.");
            }

            return Ok(user); // Return the user or a token if implementing JWT
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            if (await _userRepository.GetUserByEmailAndPasswordAsync(model.Email, model.Password) != null)
            {
                return BadRequest("User already exists.");
            }

            var user = new User
            {
                Email = model.Email,
                Password = model.Password,
                Status = "Active" 
            };

            await _userRepository.AddUserAsync(user);
            return Ok(user);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateUserStatus(int id, UpdateStatusModel model)
        {
            try
            {
                await _userRepository.UpdateUserStatusAsync(id, model.Status);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

    }
}
