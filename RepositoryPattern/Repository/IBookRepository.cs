using RepositoryPattern.Models;

namespace RepositoryPattern.Repository
{
    public interface IBookRepository
    {
        public Task<IEnumerable<BookModel>> getAllBooks();
        public Task<BookModel> getSingleBook(int bookId);
        public Task<int> addNewBook(BookModel objBookModel);
        public Task<int> updateNewBook(BookModel objBookModel);
        public Task<int> deleteNewBook(int bookId);
    }                  
}
