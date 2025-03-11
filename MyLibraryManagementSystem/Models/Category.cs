namespace MyLibraryManagementSystem.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } // Will map to CategoryType enum
    }

    public enum CategoryType
    {
        Hadith,
        Tafsir,
        Fiqh,
        Seerah,
        Aqeedah,
        ArabicLanguage
    }
}
