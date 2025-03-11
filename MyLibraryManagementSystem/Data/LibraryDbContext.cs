using Microsoft.EntityFrameworkCore;
using MyLibraryManagementSystem.Models;

namespace MyLibraryManagementSystem.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Existing seed data...
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = CategoryType.Hadith.ToString() },
                new Category { Id = 2, Name = CategoryType.Tafsir.ToString() },
                new Category { Id = 3, Name = CategoryType.Fiqh.ToString() },
                new Category { Id = 4, Name = CategoryType.Seerah.ToString() },
                new Category { Id = 5, Name = CategoryType.Aqeedah.ToString() },
                new Category { Id = 6, Name = CategoryType.ArabicLanguage.ToString() }
            );

            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Id = 1, Name = "Darussalam", LogoUrl = "/images/darussalam-logo.png", Location = "Riyadh, Saudi Arabia" },
                new Publisher { Id = 2, Name = "Dar Al Taqwa", LogoUrl = "/images/dar-al-taqwa-logo.png", Location = "London, UK" }
            );

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Imam Bukhari", Bio = "Renowned hadith scholar from Bukhara, compiler of Sahih Bukhari." },
                new Author { Id = 2, Name = "Ibn Kathir", Bio = "Famous historian and exegete, author of Tafsir Ibn Kathir." }
            );

            // Assuming ImageUrl is already in your Book model; if not, remove it here
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Sahih Bukhari",
                    AuthorId = 1,
                    CategoryId = 1,
                    PublisherId = 1,
                    Language = "Arabic",
                    TotalCopies = 5,
                    AvailableCopies = 5,
                    ImageUrl = "/images/sahih-bukhari.jpg"
                },
                new Book
                {
                    Id = 2,
                    Title = "Tafsir Ibn Kathir",
                    AuthorId = 2,
                    CategoryId = 2,
                    PublisherId = 2,
                    Language = "English",
                    TotalCopies = 3,
                    AvailableCopies = 3,
                    ImageUrl = "/images/tafsir-ibn-kathir.jpg"
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Ahmad Khan", Email = "ahmad@example.com", Phone = "123-456-7890" }
            );
        }
    }
}
