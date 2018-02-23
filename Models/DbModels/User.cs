using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class RegisterUser : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
 
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<UserActivity> UserActivity { get; set; }

        public RegisterUser()
        {
            UserActivity = new List<UserActivity>();
        }

    }
}