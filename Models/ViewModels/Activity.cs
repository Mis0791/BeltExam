using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class ActivityViewModel : BaseEntity
    {
        [Required]
        [Display(Name = "Title")]
        [MinLength(2)]
        public string Title { get; set; }
 
        [Required]
        [Display(Name = "Time")]
        public string Time { get; set; }

        [Required]
        [Display(Name = "Date")]
        [MyDate(ErrorMessage = "Date and Time must be in the future!")]
        public DateTime Date { get; set; }
 
        [Required]
        [Display(Name = "Duration")]
        public int Duration { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
   
    }
    public class MyDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d >= DateTime.Now;
        }
    }
}