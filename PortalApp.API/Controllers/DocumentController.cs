using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalApp.API.Data;
using PortalApp.API.Dtos;
using PortalApp.API.Models;
namespace PortalApp.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]

    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepo;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;
        public DocumentController(IDocumentRepository documentRepo, IMapper mapper, IUserRepository userRepo)
        {
            _documentRepo = documentRepo;
            _mapper = mapper;
            _userRepo = userRepo;
        }

        public async Task<IActionResult> SaveDocumentConfig(DocumentConfig conf)
        {
            await _documentRepo.SaveDocumentConfig(conf);
            return Ok();
        }

        [HttpGet("getDocumentConfigById/{id}")]
        public async Task<IActionResult> GetDocumentConfigById(Guid id)
        {
            var documentConfig = await _documentRepo.GetDocumentConfigById(id);
            return Ok(documentConfig);
        }

        [HttpGet("getDocumentConfigByDocType/{documentType}")]
        public async Task<IActionResult> GetDocumentConfig(string documentType)
        {
            var documentConfig = await _documentRepo.GetDocumentConfigVs(documentType);
            return Ok(documentConfig);
        }

        [HttpGet("getDocumentConfigs")]
        public async Task<IActionResult> GetDocumentConfigs()
        {
            var configs =  await _documentRepo.GetDocumentConfigs();
            return Ok(configs);
        }

        public async Task<IActionResult> CreateEditableDocument(Guid id, string docType)
        {
            var config = await _documentRepo.GetDocumentConfigVs(docType);
            var editable = new DocumentEditable();
            var currentUser = await _userRepo.GetCurrentUser(User.Identity.Name);
            editable.Author = currentUser;
            editable.Created = DateTime.Now;
            editable.DocumentConfigVsId = config.Id;
            editable.WfConfigs = config.WfConfigsSerialized;
            editable.Content = new DocumentContent();
            editable.Title = config.Title;
            editable.Id = Guid.NewGuid();
            editable.Status = DocumentStatus.Draft;
            foreach(var wfconfig in editable.WfConfigs)
            {
                if(!String.IsNullOrEmpty(wfconfig.Computed))
                {
                    switch(wfconfig.Computed)
                    {
                        case "MyDepartment":
                            var dep = await _userRepo.GetCurrentUsersDepartment(User.Identity.Name);
                            wfconfig.Ous.Add(dep);
                            break;
                        case "MyAuthor":
                            var user = await _userRepo.GetCurrentUser(User.Identity.Name);
                            wfconfig.Ous.Add(user);
                            break;
                    }
                }
            }
            return Ok(editable);
        }
        
        [HttpPost("docConfV/update")]
        public async Task<IActionResult> UpdateDocumentConfigV(DocumentConfigVForUpdate documentConfigVForUpdate)
        {
            var documentConfigVFromRepo = new DocumentConfigVs();
            if(documentConfigVForUpdate.Id.ToString() == "00000000-0000-0000-0000-000000000000")
            { 
                documentConfigVFromRepo.Id = Guid.NewGuid();
                documentConfigVFromRepo.Title = documentConfigVForUpdate.Title;
                documentConfigVFromRepo.TitleGeneration = documentConfigVForUpdate.TitleGeneration;
                documentConfigVFromRepo.CreateControlcardFunction = documentConfigVForUpdate.CreateControlcardFunction;
                documentConfigVFromRepo.CopyDocumentFunction = documentConfigVForUpdate.CopyDocumentFunction;
                documentConfigVFromRepo.CloseDocumentFunction = documentConfigVForUpdate.CloseDocumentFunction;
                documentConfigVFromRepo.Created = DateTime.Now;
                documentConfigVFromRepo.Author = documentConfigVForUpdate.Author;
                documentConfigVFromRepo.Category = documentConfigVForUpdate.Category;
                documentConfigVFromRepo.NeedRegister = true;
                documentConfigVFromRepo.ReadOnly = false;
                documentConfigVFromRepo.DocumentConfigId = documentConfigVForUpdate.DocumentConfigId;
                var documentConfigFromRepo = await _documentRepo.GetDocumentConfigById(documentConfigVForUpdate.DocumentConfigId);
                documentConfigFromRepo.DocumentConfigVs.Add(documentConfigVFromRepo);
                documentConfigVFromRepo.WfConfigsSerialized = documentConfigVForUpdate.WfConfigsSerialized;
                documentConfigVFromRepo.ContentConfigsSerialized = documentConfigVForUpdate.ContentConfigsSerialized;
                
            }
            else
            {
                documentConfigVFromRepo = await _documentRepo.GetDocumentConfigVsById(documentConfigVForUpdate.Id);
                documentConfigVFromRepo.Title = documentConfigVForUpdate.Title;
                documentConfigVFromRepo.TitleGeneration = documentConfigVForUpdate.TitleGeneration;
                documentConfigVFromRepo.CreateControlcardFunction = documentConfigVForUpdate.CreateControlcardFunction;
                documentConfigVFromRepo.CopyDocumentFunction = documentConfigVForUpdate.CopyDocumentFunction;
                documentConfigVFromRepo.CloseDocumentFunction = documentConfigVForUpdate.CloseDocumentFunction;
                documentConfigVFromRepo.Author = documentConfigVForUpdate.Author;
                documentConfigVFromRepo.Category = documentConfigVForUpdate.Category;
                documentConfigVFromRepo.WfConfigsSerialized = documentConfigVForUpdate.WfConfigsSerialized;
                documentConfigVFromRepo.ContentConfigsSerialized = documentConfigVForUpdate.ContentConfigsSerialized;
            }

            try
            {
                 if (await _documentRepo.SaveAll())
                return NoContent();

            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
           
            throw new Exception($"Updating documentConfigV {documentConfigVForUpdate.Id} failed on save");
            
        }
        [HttpPost("docConfV/add")]
        public async Task<IActionResult> AddDocumentConfigV(DocumentConfigVForUpdate documentConfigVForUpdate)
        {
            var documentConfigVFromRepo = new DocumentConfigVs();
            documentConfigVFromRepo.Id = Guid.NewGuid();
            documentConfigVFromRepo.Title = documentConfigVForUpdate.Title;
            documentConfigVFromRepo.TitleGeneration = documentConfigVForUpdate.TitleGeneration;
            documentConfigVFromRepo.CreateControlcardFunction = documentConfigVForUpdate.CreateControlcardFunction;
            documentConfigVFromRepo.CopyDocumentFunction = documentConfigVForUpdate.CopyDocumentFunction;
            documentConfigVFromRepo.CloseDocumentFunction = documentConfigVForUpdate.CloseDocumentFunction;
            documentConfigVFromRepo.Created = DateTime.Now;
            documentConfigVFromRepo.NeedRegister = true;
            documentConfigVFromRepo.ReadOnly = false;
            documentConfigVFromRepo.DocumentConfigId = documentConfigVForUpdate.DocumentConfigId;
            var documentConfigFromRepo = await _documentRepo.GetDocumentConfigById(documentConfigVForUpdate.DocumentConfigId);
            documentConfigFromRepo.DocumentConfigVs.Add(documentConfigVFromRepo);
            try
            {
                 if (await _documentRepo.SaveAll())
                return NoContent();

            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
           
            throw new Exception($"Updating documentConfigV {documentConfigVForUpdate.Id} failed on save");
            
        }


    }
}