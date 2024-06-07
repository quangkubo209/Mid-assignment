using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IBorrowRepositoy
    {
       Task<IEnumerable<BookBorrowingRequest>> GetBorrowingRequestsForMonthAsync(Guid userId, int month, int year);

        Task<IEnumerable<BookBorrowingRequest>> GetBorrowingRequestsByUserIdAsync(Guid userId);
    }
}
