using ProjectManagementApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.Domain.Entities
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProjectName { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Budget { get; set; }

        public string Owner { get; set; }
        public bool IsDeleted { get; set; }

        public ProjectStatus Status { get; set; } = ProjectStatus.NotStarted;

        public ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();

        // Additional validation for EndDate not earlier than StartDate
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
