using DocuStorageAD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace DocuStorageAD.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class BackupsController : ControllerBase
    {
        private IBackupDocumentsService _backupservice;
        public BackupsController(IBackupDocumentsService backupservice) 
        {
            _backupservice = backupservice;
        }

        [HttpGet("{category}")]
        public async Task<IActionResult> GetBackups(string category) 
        {
            try 
            {
                var list = await _backupservice.GetDocuments(category);
                return Ok(list);
            } 
            catch (Exception e) 
            {
                return BadRequest(e.Message);
            }            
        }
    }
}
