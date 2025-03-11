namespace MyLibraryManagementSystem.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; } // Navigation property
        public int UserId { get; set; }
        public User User { get; set; } // Navigation property
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; } // Null if not returned yet
        public bool IsReturned { get; set; }
    }
}
