using ProjectManagementApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = ProjectManagementApp.Domain.Enums.TaskStatus;

namespace ProjectManagementApp.Domain.Entities
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [StringLength(100)]
        public string TaskName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string AssignedTo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TaskPriority Priority { get; set; }

        public TaskStatus Status { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        // Method for validating task dates
        public bool IsValid(out string validationMessage)
        {
            if (EndDate < StartDate)
            {
                validationMessage = "End date cannot be earlier than start date.";
                return false;
            }
            validationMessage = null;
            return true;
        }

    }

}
