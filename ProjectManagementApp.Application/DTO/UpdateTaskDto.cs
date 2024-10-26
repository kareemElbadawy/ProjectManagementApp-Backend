using ProjectManagementApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = ProjectManagementApp.Domain.Enums.TaskStatus;

namespace ProjectManagementApp.Application.DTO
{
    public class UpdateTaskDto
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public int? AssignedTo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Priority { get; set; }
        public TaskStatus Status { get; set; }
    }
}
