using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class Activity : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ActivityId { get; set; }
        public string Title { get; set; }
        public string Time { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }

        public List<UserActivity> UserActivity { get; set; }

        public Activity()
        {
            UserActivity = new List<UserActivity>();
        }

    }
}