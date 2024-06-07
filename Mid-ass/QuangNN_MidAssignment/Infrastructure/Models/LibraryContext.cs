using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;

namespace Infrastructure.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookBorrowingRequest> BookBorrowingRequests { get; set; }
        public DbSet<BookBorrowingRequestDetails> BookBorrowingRequestDetails { get; set; }

        // Phương thức OnConfiguring này sẽ chỉ được gọi nếu không có DbContextOptions được cung cấp
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=KuboNguyenPC;Database=LibraryManagement;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
        .Property(u => u.Role)
        .HasConversion<int>();

            modelBuilder.Entity<BookBorrowingRequest>(entity =>
            {
                entity.HasOne(e => e.User)
                      .WithMany(d => d.BookBorrowingRequests)
                      .HasForeignKey(x => x.RequestorId);
            });

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId);

            modelBuilder.Entity<BookBorrowingRequestDetails>(entity =>
            {
                entity.HasOne(e => e.Book)
                      .WithMany(d => d.BookBorrowRequestDetails)
                      .HasForeignKey(x => x.BookId);

                entity.HasOne(e => e.BookBorrowingRequest)
                      .WithMany(d => d.BookBorrowingRequestDetails)
                      .HasForeignKey(x => x.BookBorrowingRequestId);
            });
        }
    }
}