using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class UserActivity 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserActivityId { get; set; }

        public int UserId { get; set; }
        public RegisterUser User { get; set; }

        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

    }
}