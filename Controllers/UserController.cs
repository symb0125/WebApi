using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _iUserService;

        public UserController(IUserService userService)
        {
            _iUserService = userService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetAll()
        {
            return Ok(await _iUserService.GetAllUsers());
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<ServiceResponse<User>>> Get(int id)
        {
            return Ok(await _iUserService.GetUserById(id));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> Add(AddUserDto newUser)
        {
            return Ok(await _iUserService.AddUser(newUser));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<User>>> Update(AddUserDto updatedUser)
        {
            return Ok(await _iUserService.UpdateUser(updatedUser));
        }


        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ServiceResponse<User>>> Delete(int id)
        {
            if (await _iUserService.GetUserById(id) == null) return BadRequest("User Doesn't Exist.");
            return Ok(await _iUserService.DeleteUserById(id));
        }
    }
}