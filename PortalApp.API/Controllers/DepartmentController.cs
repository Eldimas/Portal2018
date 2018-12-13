using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalApp.API.Data;
using PortalApp.API.Data.Repo.DepartmentRepo;
using PortalApp.API.Dtos;
using PortalApp.API.Models;

namespace PortalApp.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IPortalRepository _repo;
        private readonly IDepartmentRepository _depRepo;
        private readonly IMapper _mapper;

        public DepartmentController(IPortalRepository repo, 
        IDepartmentRepository depRepo, 
        IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
            _depRepo = depRepo;
        }

        
        [HttpGet("getDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            // if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            //     return Unauthorized();

            var departments = await _depRepo.GetDepartments();
            

            if (departments == null)
                return NotFound();

            return Ok(departments);
        }

        [HttpGet("getDepartment/{id}")]
        public async Task<IActionResult> GetDepartment(Guid id)
        {
            // var isCurrentUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) == id;
            // var isCurrentUser = false;

            var department = await _depRepo.GetDepartment(id);

            // var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            // return Ok(userToReturn);
            return Ok(department);
        }


        [HttpGet("getDepartmentVs")]
        public async Task<IActionResult> GetDepartmentVs()
        {
            // if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            //     return Unauthorized();

            var departmentVs = await _depRepo.GetDepartmentVs();

            if (departmentVs == null)
                return NotFound();

            return Ok(departmentVs);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(Guid id, DepartmentForUpdateDto departmentForUpdateDto)
        {
            var depFromRepo = await _depRepo.GetDepartment(id);

            _mapper.Map(departmentForUpdateDto, depFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating department {id} failed on save");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("addDepartment")]
        public async Task<IActionResult> AddDepartment(DepartmentForUpdateDto departmentForUpdateDto)
        {
            var depToCreate = _mapper.Map<Department>(departmentForUpdateDto);
            _repo.Add(depToCreate);

            if (await _repo.SaveAll())
            {
                return Ok(depToCreate);
            }
                


           return BadRequest();
            //return Ok();
        }


        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDepartment(Guid id)
        {
            var regionToRemove = await _depRepo.GetDepartment(id);
            _repo.Delete(regionToRemove);

            if (await _repo.SaveAll())
            {
                return Ok();
            }
                


           return BadRequest();
            //return Ok();
        }


        /////////////////////DepV
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("depv/update/{id}")]
        public async Task<IActionResult> UpdateDepV(Guid id, DepartmentVForUpdateDto departmentVForUpdateDto)
        {
            var depVFromRepo = await _depRepo.GetDepartmentV(id);

            _mapper.Map(departmentVForUpdateDto, depVFromRepo);

            try
            {
                 if (await _repo.SaveAll())
                return NoContent();

            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
           
            throw new Exception($"Updating departmentV {id} failed on save");
            
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("depv/add")]
        public async Task<IActionResult> AddDepV(DepartmentVForUpdateDto departmentVForUpdateDto)
        {

            var depToCreate = _mapper.Map<DepartmentV>(departmentVForUpdateDto);
            depToCreate.Id = Guid.NewGuid();
            depToCreate.RegionId = new Guid("EE8D5BB2-99C4-4071-887E-84F4CAF01CF7");
            depToCreate.Created = DateTime.Now;
            _repo.Add(depToCreate);

            if (await _repo.SaveAll())
            {
                return Ok(depToCreate);
            }
                


           return BadRequest();
            //return Ok();
        }
    }
}