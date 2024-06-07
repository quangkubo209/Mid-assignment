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
    public class BookBorrowingRequestRepository : GenericRepository<BookBorrowingRequest>, IBookBorrowingRequestRepository
    {
        private readonly LibraryContext _context;

        public BookBorrowingRequestRepository(LibraryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<BookBorrowingRequest>> GetAllBorrowingRequestsWithDetailsAsync()
        {
            var requestsWithDetails = await _context.BookBorrowingRequests
                .Include(r => r.User)
                .Include(r => r.BookBorrowingRequestDetails)
                .ToListAsync();

            return requestsWithDetails;
        }

    }
}
