using BookReviewApp.Models;
namespace BookReviewApp.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int categoryId);
        ICollection<Book> GetBooksByCategory(int categoryId);
        bool UpdateCategory(Category category);
        bool CategoryExists(int categoryId);
        bool CreateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
    }
}
