namespace DocuStorage.Controllers;

using Microsoft.AspNetCore.Mvc;
using DocuStorage.Services;
using DocuStorage.Models;
using DocuStorage.Helpers;
using DocuStorage.Common.Data.Model;
using DocuStorage.Common.Data.Services;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        var response = _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }


    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public IActionResult Create(UserRequest model)
    {
        if (!ModelState.IsValid) 
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        var user = _userService.Create(new User()
        {
            Username = model.Username,
            Password = model.Password,
            Role = model.Role
        });
        return Ok(user);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPut]
    public IActionResult Update(UserRequest model)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        var response = _userService.Update(new User()
        {
            Id = model.Id,
            Username = model.Username,
            Password = model.Password,
            Role = model.Role
        });

        if (response)
        {
            return Ok();
        }
        else 
        {
            return BadRequest("User not updated");
        }
        
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{userid}")]
    public IActionResult Delete(int userId) 
    {
        _userService.Delete(userId);
        return Ok();
    }

}



