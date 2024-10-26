using ProjectManagementApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = ProjectManagementApp.Domain.Enums.TaskStatus;

namespace ProjectManagementApp.Application.DTO
{
    public class CreateTaskDto
    {
        [Required]
        [StringLength(100)]
        public string TaskName { get; set; }

        public string Description { get; set; }

        public string AssignedTo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TaskPriority Priority { get; set; }

        public TaskStatus Status { get; set; }

        public int ProjectId { get; set; }
    }
}
