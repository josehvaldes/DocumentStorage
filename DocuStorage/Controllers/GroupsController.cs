namespace DocuStorage.Controllers;
using Microsoft.AspNetCore.Mvc;
using DocuStorage.Models;
using DocuStorage.Services;
using DocuStorage.Helpers;



[ApiController]
[Route("[controller]")]
public class GroupsController : ControllerBase
{
    private IGroupService _groupService;

    public GroupsController(IGroupService groupService) 
    {
        _groupService = groupService;
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll() 
    {
        return Ok(_groupService.GetAll());
    }


    [Authorize]
    [HttpGet("user/{userid}")]
    public IActionResult GetGroupsByUser(int userid) 
    {
        return Ok(_groupService.GetByUser(userid));
    }


    [Authorize(Roles = Roles.Admin)]
    [HttpPost("assigntouser/{userid}")]
    public IActionResult AssignToGroup(int userid, int[] groups)
    {
        _groupService.AssignToUser(userid, groups);
        return Ok();
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("create")]
    public IActionResult Create(GroupRequest request)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        try
        {
            var group = _groupService.Create(request);
            return Ok(group);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

