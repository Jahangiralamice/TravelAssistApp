using System.ComponentModel.DataAnnotations;

namespace TravelAssistApp.Models
{
    public class TouristPlace
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Maximum length should be 100 characters")]
        public string Title { get; set; }
        public string Description { get; set; }

        [Display(Name ="Image")]
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Please choose an image to upload.")]
        public string ImagePath { get; set; }

    }
}