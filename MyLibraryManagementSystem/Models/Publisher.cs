using System.ComponentModel.DataAnnotations;

namespace MyLibraryManagementSystem.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Publisher name is required.")]
        [StringLength(100, ErrorMessage = "Publisher name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Logo URL cannot exceed 200 characters.")]
        public string LogoUrl { get; set; }
    }
}
