using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApplication.DTOs;
using MovieApplication.Respones;
using MovieApplication.Services;

namespace MovieApplication.EndPoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly IuserServices _userServices;

        public usersController(IuserServices services)
        {
            _userServices = services;
        }

        [HttpPost("register")]
        public async Task<IActionResult> PostUsers(UserDTO userDto)
        {
            try
            {
                var user = await _userServices.UserRegister(userDto);
                if(user)
                {
                    var respones = new GenericRespones<object>(200, "Sucess", user, null);
                    return Ok(respones);
                }
                else
                {
                    return BadRequest(user);    
                }
            }
            catch(Exception ex)
            {
                var errorRespones = new GenericRespones<object>(500, "Internal server error", null, ex.Message);
                return StatusCode(500, errorRespones);
            }
        }

    }
}
