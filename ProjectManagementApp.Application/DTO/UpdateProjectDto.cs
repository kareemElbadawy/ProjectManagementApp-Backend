using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.Application.DTO
{
    public class UpdateProjectDto
    {
        [Required]
        public int ProjectId { get; set; } // Identifier for the project being updated

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string ProjectName { get; set; } // Name of the project

        [StringLength(500)]
        public string Description { get; set; } // Description of the project

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } // Start date of the project

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } // End date of the project

        [Range(0, double.MaxValue)]
        public decimal Budget { get; set; } // Budget for the project

        public int Owner { get; set; } // User ID of the project owner
    }
}
