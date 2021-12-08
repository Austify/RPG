using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RPG.Data;
using RPG.DTOs.UserDto;
using RPG.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RPG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        // GET: /<controller>/

        private readonly IAuthRepository _auth;

        public AuthController(IAuthRepository auth)
        {
            _auth = auth;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Regsiter(UserRegisterDto user)
        {
            ServiceResponse<int> response = await _auth.Register(
                new User { Username = user.Username }, user.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }
    }
}
