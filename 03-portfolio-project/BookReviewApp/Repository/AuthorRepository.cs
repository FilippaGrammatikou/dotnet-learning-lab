using BookReviewApp.DATA;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;

namespace BookReviewApp.Repository
{
    public class AuthorRepository 
    {
        private readonly DataContext _context;
        public AuthorRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Author> GetAllAuthors()
        {
            return _context.Authors.ToList();
        }

        public Author GetAuthor(int authorId)
        {
            return _context.Authors.Where(a => a.Id == authorId).FirstOrDefault();
        }

        public ICollection<Book> GetBooksByAuthor(int authorId)
        {
            return _context.BookAuthors
                .Where(ba => ba.AuthorId == authorId)
                .Select(ba => ba.Book)
                .ToList();
        }

        public bool AuthorsExists(int authorId)
        {
            return _context.Authors.Any(a => a.Id == authorId);
        }
    }
}
