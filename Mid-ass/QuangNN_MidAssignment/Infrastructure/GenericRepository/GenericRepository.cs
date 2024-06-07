using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;
using Infrastructure.GenericModel;

namespace Infrastructure.GenericRepository
{
    //public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    //{
    //    private readonly LibraryContext _context;
    //    private readonly DbSet<T> _dbSet;

    //    public GenericRepository(LibraryContext context)
    //    {
    //        _context = context;
    //        _dbSet = context.Set<T>();
    //    }

    //    public async Task<IEnumerable<T>> GetAllAsync()
    //    {
    //        return await _dbSet.Where(e => !e.IsDeleted).ToListAsync();
    //    }

    //    public async Task<T> GetByIdAsync(Guid id)
    //    {
    //        return await _dbSet.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
    //    }

    //    public async Task AddAsync(T entity)
    //    {
    //        entity.CreatedAt = DateTime.UtcNow;
    //        await _dbSet.AddAsync(entity);
    //        await _context.SaveChangesAsync();
    //    }

    //    public async Task UpdateAsync(T entity)
    //    {
    //        entity.UpdatedAt = DateTime.UtcNow;
    //        _dbSet.Update(entity);
    //        await _context.SaveChangesAsync();
    //    }

    //    public async Task DeleteAsync(Guid id)
    //    {
    //        var entity = await GetByIdAsync(id);
    //        if (entity != null)
    //        {
    //            entity.DeletedAt = DateTime.UtcNow;
    //            entity.IsDeleted = true;
    //            _dbSet.Update(entity);
    //            await _context.SaveChangesAsync();
    //        }
    //    }

    //    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    //    {
    //        return await _dbSet.Where(predicate).Where(e => !e.IsDeleted).ToListAsync();
    //    }
    //}
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly LibraryContext _context;
        private DbSet<T> _dbset;
        public GenericRepository(LibraryContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbset.Where(e => !e.IsDeleted).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                entity.CreatedAt = DateTime.Now;
                await _dbset.AddAsync(entity);
                int status = await _context.SaveChangesAsync();
                if (status == 0)
                {
                    throw new Exception("No records were saved to the database.");
                }
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Log the exception
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        //public async Task AddAsync(T entity)
        //{
        //    using var transaction = _context.Database.BeginTransaction();
        //    try
        //    {
        //        entity.CreatedAt = DateTime.Now;
        //        await _dbset.AddAsync(entity);
        //        int status = await _context.SaveChangesAsync();
        //        await transaction.CommitAsync();
        //    }
        //    catch (Exception)
        //    {
        //        await transaction.RollbackAsync();
        //    }
        //}

        public virtual async Task DeleteAsync(Guid id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var entity = await _dbset.FirstOrDefaultAsync(x => x.Id == id);
                entity.DeletedAt = DateTime.Now;
                entity.IsDeleted = true;
                int status = await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }

        //public virtual async Task<IEnumerable<T>> GetAllAsync(int? index, int? size)
        //{
        //    if (index.HasValue && size.HasValue)
        //        return await _dbset.Skip((index.Value - 1) * size.Value).Take(size.Value).ToListAsync();
        //    else
        //        return await _dbset.ToListAsync();
        //}

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbset.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task UpdateAsync(T entity)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                entity.UpdatedAt = DateTime.Now;
                _dbset.Update(entity);
                var status = await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {

                await transaction.RollbackAsync();
            }
        }
    }
}
