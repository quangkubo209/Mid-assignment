using System;
using System.Collections.Generic;
using Infrastructure.GenericModel;
using Infrastructure.Models;

namespace Infrastructure.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public RoleEnum Role { get; set; }
        public string Email { get; set; }
        public ICollection<BookBorrowingRequest> BookBorrowingRequests { get; set; }
    }
}
