using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RepositoryPattern.AppCode;
using RepositoryPattern.Models;

namespace RepositoryPattern.Repository
{
    public class BookRepository : IBookRepository
    {
        private BookContext _bookContext;

        public BookRepository(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<int> addNewBook(BookModel objBookModel)
        {
            if (objBookModel == null)
                return -1;

            await _bookContext.Books.AddAsync(objBookModel);
            return await _bookContext.SaveChangesAsync();
        }

        public async Task<int> deleteNewBook(int id)
        {
            var itemToDelete = await _bookContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (itemToDelete == null)
                return -1;

            _bookContext.Books.Remove(itemToDelete);
            return await _bookContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookModel>> getAllBooks()
        {
            return await _bookContext.Books.ToListAsync();
        } 

        public async Task<BookModel> getSingleBook(int id)
        {
            return await _bookContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> updateNewBook(BookModel book)
        {
            int result = -1;
            if (book != null && book.Id != 0)
            {
                var itemToUpdate =  await _bookContext.Books.FirstOrDefaultAsync(x => x.Id == book.Id);
                if (itemToUpdate != null)
                {
                    _bookContext.Entry(itemToUpdate).CurrentValues.SetValues(book);
                    await _bookContext.SaveChangesAsync();
                    result = 1;
                }
            }
            return result;
        }
    }
}
