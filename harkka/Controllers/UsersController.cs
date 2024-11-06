using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using harkka.models;
using harkka.Services;
using Microsoft.AspNetCore.Authorization;

namespace harkka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService service)
        {
            _userService = service;
        }
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return Ok(await _userService.GetusersAsync());
        }

        [HttpGet("{username}")]

        public async Task<ActionResult<UserDTO>> GetUser(string username)
        {
            UserDTO? user = await _userService.GetuserAsync(username);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPut("{username}")]

        public async Task<ActionResult<UserDTO>> PutUser(string username, User user)
        {
            if (username != user.username)
            {
                return BadRequest();
            }

            if (await _userService.UpdateUserAsync(user))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(User user)
        {
            UserDTO? newUser = await _userService.NewuserAsync(user);
            if (newUser == null)
            {
                return Problem("Username not available");
            }

            return CreatedAtAction("GetUser", new { username = user.username }, user);
        }
        [HttpDelete("{username}")]


        public async Task<IActionResult> DeleteUser(string username)
        {
            if (await _userService.DeleteUserAsync(username))
            { return NoContent(); }
            return NotFound();


        }

        public IActionResult Index()
        {
            return View();
        }
    }
}