using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Domain.Entities;
using ProjectManagementApp.Application.DTO;
using ProjectManagementApp.Application.Interfaces.Service;

namespace ProjectManagementApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        // POST: api/project
        [HttpPost]
        [Authorize(Roles = "Manager")] // Only Managers can create projects
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto createProjectDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var project = _mapper.Map<Project>(createProjectDto);
            await _projectService.AddAsync(project);
            var response = _mapper.Map<ProjectResponseDto>(project);
            return CreatedAtAction(nameof(GetById), new { id = project.ProjectId }, response);
        }

        // GET: api/project/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Manager, Employee")] // Both Managers and Employees can get a project by id
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null) return NotFound();
            var response = _mapper.Map<ProjectResponseDto>(project);
            return Ok(response);
        }

        // GET: api/project
        [HttpGet]
        [Authorize(Roles = "Manager, Employee")] // Both roles can get all projects
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var projects = await _projectService.GetAllAsync(pageNumber, pageSize);
            var response = _mapper.Map<List<ProjectResponseDto>>(projects);
            return Ok(response);
        }

        // PUT: api/project/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Manager")] // Only Managers can update projects
        public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectDto updateProjectDto)
        {
            if (id != updateProjectDto.ProjectId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingProject = await _projectService.GetByIdAsync(id);
            if (existingProject == null) return NotFound();

            var projectToUpdate = _mapper.Map(updateProjectDto, existingProject); // Update the existingProject directly
            await _projectService.UpdateAsync(projectToUpdate);
            return NoContent();
        }

        // DELETE: api/project/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")] // Only Managers can delete projects
        public async Task<IActionResult> DeleteProject(int id)
        {
            var existingProject = await _projectService.GetByIdAsync(id);
            if (existingProject == null) return NotFound();
            await _projectService.DeleteAsync(id);
            return NoContent();
        }
    }
}
