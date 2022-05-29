using DocuStorageAD.Models;
using DocuStorageAD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace DocuStorageAD.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class DocumentsController : ControllerBase
    {
        private IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_documentService.GetAll());
        }

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

        [HttpGet("download/{documentId}/{name}")]
        public IActionResult Download(int documentId, string name)
        {
            var document = _documentService.Get(documentId);
            return new FileContentResult(document.Content, "application/octet-stream");
        }

    }
}
