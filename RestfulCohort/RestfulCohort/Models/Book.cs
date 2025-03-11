using System.ComponentModel.DataAnnotations;

namespace RestfulCohort.Models
{
    public class Book
    {
        public int Id{get; set;}

        [Required]
        public string Title {get; set;}

        [Required]
        public string Author {get; set;}

        [Range(50, int.MaxValue, ErrorMessage ="Page number must be greater than 50!")]
        public int PageNumber {get; set;}

        public bool IsAvailable {get; set;} = true;
    }
}
