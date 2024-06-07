using Infrastructure.GenericRepository;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>,  IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBookAsync()
        {
            return await _context.Books
                .Include(b => b.Category)
                .Where(b => !b.IsDeleted)
                .ToListAsync();
        }

    }
}
