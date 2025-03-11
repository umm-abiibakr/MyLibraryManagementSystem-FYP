using System.ComponentModel.DataAnnotations;

namespace MyLibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public required string Title { get; set; }
        public int? AuthorId { get; set; } // Nullable
        public Author? Author { get; set; }
        public int? CategoryId { get; set; } // Nullable
        public Category? Category { get; set; }
        public int? PublisherId { get; set; } // Nullable
        public Publisher? Publisher { get; set; }
        [Required(ErrorMessage = "Language is required.")]
        [StringLength(50, ErrorMessage = "Language cannot exceed 50 characters.")]
        public required string Language { get; set; }
        [Required(ErrorMessage = "Total copies is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Total copies must be at least 1.")]
        public int TotalCopies { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Available copies cannot be negative.")]
        public int AvailableCopies { get; set; }
        [StringLength(200, ErrorMessage = "Image URL cannot exceed 200 characters.")]
        public required string ImageUrl { get; set; }
    }
}
