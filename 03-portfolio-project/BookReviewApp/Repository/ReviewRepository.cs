using BookReviewApp.DATA;
using BookReviewApp.Interfaces;

namespace BookReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Models.Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public Models.Review GetReview(int reviewId)
        {
            return _context.Reviews.FirstOrDefault(r => r.Id == reviewId);
        }

        public ICollection<Models.Review> GetReviewsOfBook(int bookId)
        {
            return _context.Reviews
                .Where(r => r.Book.Id == bookId)
                .ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }
    }
}