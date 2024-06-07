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
    public class BorrowRepository : GenericRepository<BookBorrowingRequest>, IBorrowRepositoy
    {
        private readonly LibraryContext _context;

        public BorrowRepository(LibraryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookBorrowingRequest>> GetBorrowingRequestsForMonthAsync(Guid userId, int month, int year)
        {
            return await _context.BookBorrowingRequests
                .Where(r => r.RequestorId == userId &&
                            r.DateRequested.Month == month &&
                            r.DateRequested.Year == year)
                .ToListAsync();
        }

        public async Task<IEnumerable<BookBorrowingRequest>> GetBorrowingRequestsByUserIdAsync(Guid userId)
        {
            return await _context.BookBorrowingRequests
                .Include(r => r.BookBorrowingRequestDetails)
                .Include(r => r.User)
                .Where(r => r.RequestorId == userId)
                .ToListAsync();
        }
    }
}
