using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.Domain.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int TaskId { get; set; }  // Foreign Key to Task
        public string UserId { get; set; } // Identifier of the user who commented
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Property
        public Tasks Tasks { get; set; }
    }
}
