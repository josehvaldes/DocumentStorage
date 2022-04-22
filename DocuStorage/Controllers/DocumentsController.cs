namespace DocuStorage.Controllers;
using Microsoft.AspNetCore.Mvc;
using DocuStorage.Services;

using DocuStorage.Models;
using DocuStorage.Helpers;

[ApiController]
[Route("[controller]")]
public class DocumentsController : ControllerBase
{

    private IDocumentService _documentService;

    public DocumentsController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_documentService.GetAll());
    }

    /// <summary>
    /// All roles can download documents
    /// //(Role = Roles.Admin | Roles.Manager | Roles.Root| | Roles.User )
    /// </summary>
    /// <param name="documentId"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    [Authorize()] 
    [HttpGet("download/{documentId}/{name}")]
    public IActionResult Download(int documentId, string name)
    {
        var document = _documentService.Get(documentId);
        return new FileContentResult(document.Content, "application/octet-stream");
    }

    [Authorize(Roles = Roles.Admin| Roles.Manager)]
    [HttpPost("upload")]
    public IActionResult Upload([FromForm] DocumentRequest file)
    {
        try
        {
            var document = _documentService.Create(file);
            return Ok(document);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [Authorize]
    [HttpGet("ingroupsbyuser/{userId}")]
    public IActionResult GetInGroups(int userId)
    {
        return Ok(_documentService.GetInGroupsByUser(userId));
    }


    [Authorize]
    [HttpGet("allbyuser/{userId}")]
    public IActionResult GetAllByUser(int userId)
    {
        return Ok(_documentService.GetAllAvailableUser(userId));
    }

    [Authorize]
    [HttpGet("user/{userId}")]
    public IActionResult GetByUser(int userId)
    {
        return Ok(_documentService.GetByUserId(userId));
    }

    [Authorize]
    [HttpGet("group/{groupId}")]
    public IActionResult GetByGroup(int groupId)
    {
        return Ok(_documentService.GetByGroupId(groupId));
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("assigntouser/{userId}")]
    public IActionResult AssignToUser(int userId, int[] documents)
    {
        _documentService.AssignToUser(userId, documents);
        return Ok();
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("assigntogroup/{groupid}")]
    public IActionResult AssignToGroup(int groupid, int[] documents)
    {
        _documentService.AssignToGroup(groupid, documents);
        return Ok();
    }


    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id) 
    {
        try 
        {
            _documentService.Delete(id);
            return Ok();
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }        
    }
}

