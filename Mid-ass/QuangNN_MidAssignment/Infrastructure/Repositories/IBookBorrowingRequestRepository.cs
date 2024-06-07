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
    public interface IBookBorrowingRequestRepository : IGenericRepository<BookBorrowingRequest>
    {
        Task<List<BookBorrowingRequest>> GetAllBorrowingRequestsWithDetailsAsync();
    }
}
