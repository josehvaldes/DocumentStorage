namespace DocuStorageAD.Controllers;
using DocuStorageAD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;


[Authorize]
[Route("[controller]")]
[ApiController]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class CategoriesController : ControllerBase
{
    private IDocumentService _documentService;
    public CategoriesController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpGet]
    public IActionResult GetAll() 
    {
        //TODO Get list from database
        var result = new List<string>() { "text","jpg","png" };
        return Ok(result);
    }
}
