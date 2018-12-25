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
    [Authorize]
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

        public async Task<IActionResult> GetDocumentConfig(string documentType)
        {
            var documentConfig = await _documentRepo.GetDocumentConfig(documentType);
            return Ok(documentConfig);
        }

        public async Task<IActionResult> GetDocumentConfigs()
        {
            var configs =  await _documentRepo.GetDocumentConfigs();
            return Ok(configs);
        }

        public async Task<IActionResult> CreateEditableDocument(Guid id, string docType)
        {
            var config = await _documentRepo.GetDocumentConfig(docType);
            var editable = new DocumentEditable();
            var currentUser = await _userRepo.GetCurrentUser(User.Identity.Name);
            editable.Author = currentUser;
            editable.Created = DateTime.Now;
            editable.DocumentConfigVsId = config.Id;
            editable.WfConfigs = config.WfConfigsSerialized;
            editable.Content = new DocumentContent();
            editable.Title = config.Title;
            foreach(var wfconfig in editable.WfConfigs)
            {
                if(!String.IsNullOrEmpty(wfconfig.Computed)){
                    switch(wfconfig.Computed){
                        case "MyDepartment":
                            var user = _userRepo.GetCurrentUserProfile(User.Identity.Name);
                            break;
                        case "MyAuthor":
                            
                            break;
                    }
                }
            }
        }

    }
}