using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortalApp.API.Data;
using PortalApp.API.Data.Repo.DepartmentRepo;
using PortalApp.API.Models;
using System.Collections.Generic;

namespace PortalApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    

    public class DocumentController
    {
        private readonly IDocumentRepository _documentRepo;

        public DocumentController(IDocumentRepository documentRepo)
        {
            _documentRepo = documentRepo;
        }

        public async Task SaveDocumentConfig(DocumentConfig conf)
        {
            await _documentRepo.SaveDocumentConfig(conf);
        }

        public async Task<DocumentConfig> GetDocumentConfig(string documentType)
        {
            return await _documentRepo.GetDocumentConfig(documentType);
        }

        public async Task<ICollection<DocumentConfig>> GetDocumentConfigs()
        {
            return await _documentRepo.GetDocumentConfigs();
        }

    }
}