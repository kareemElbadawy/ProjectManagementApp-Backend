using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProjectManagementApp.Application.DTO;
using ProjectManagementApp.Application.Interfaces.Services;

namespace ProjectManagementApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        // POST: api/task
        [HttpPost]
        [Authorize(Roles = "Manager")] // Only Managers can create tasks
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto createTaskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = _mapper.Map<Tasks>(createTaskDto);
            await _taskService.AddAsync(task);
            var response = _mapper.Map<TaskResponseDto>(task);
            return CreatedAtAction(nameof(GetById), new { id = task.TaskId }, response);
        }

        // GET: api/task/project/{projectId}
        [HttpGet("project/{projectId}")]
        [Authorize(Roles = "Manager, Employee")] // Both Managers and Employees can get tasks by project
        public async Task<IActionResult> GetAll(int projectId, int pageNumber = 1, int pageSize = 10)
        {
            var tasks = await _taskService.GetAllAsync(projectId, pageNumber, pageSize);
            var response = _mapper.Map<List<TaskResponseDto>>(tasks);
            return Ok(response);
        }

        // GET: api/task/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Manager, Employee")] // Both Managers and Employees can get a task by id
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null) return NotFound();
            var response = _mapper.Map<TaskResponseDto>(task);
            return Ok(response);
        }

        // PUT: api/task/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Manager, Employee")] // Both roles can update a task
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDto updateTaskDto)
        {
            if (id != updateTaskDto.TaskId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingTask = await _taskService.GetByIdAsync(id);
            if (existingTask == null) return NotFound();

            var taskToUpdate = _mapper.Map(updateTaskDto, existingTask); // Update the existingTask directly
            await _taskService.UpdateAsync(taskToUpdate);
            return NoContent();
        }

        // DELETE: api/task/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")] // Only Managers can delete tasks
        public async Task<IActionResult> DeleteTask(int id)
        {
            var existingTask = await _taskService.GetByIdAsync(id);
            if (existingTask == null) return NotFound();
            await _taskService.DeleteAsync(id);
            return Ok(new { message = "Task deleted successfully!" });
        }

        // GET: api/task/overdue
        [HttpGet("overdue")]
        [Authorize(Roles = "Manager, Employee")] // Both roles can get overdue tasks
        public async Task<IActionResult> GetOverdueTasks()
        {
            var overdueTasks = await _taskService.GetOverdueTasksAsync();
            var response = _mapper.Map<List<TaskResponseDto>>(overdueTasks);
            return Ok(response);
        }

        // GET: api/task/high-priority
        [HttpGet("high-priority")]
        [Authorize(Roles = "Manager, Employee")] // Both roles can get high priority tasks
        public async Task<IActionResult> GetHighPriorityTasks()
        {
            var highPriorityTasks = await _taskService.GetHighPriorityTasksAsync();
            var response = _mapper.Map<List<TaskResponseDto>>(highPriorityTasks);
            return Ok(response);
        }
    }
}
