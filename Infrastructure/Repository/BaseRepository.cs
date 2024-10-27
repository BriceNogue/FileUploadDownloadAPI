using Application.Repository;
using Domain.Common;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DataContext _context;

        public BaseRepository(DataContext context)
        {
            _context = context;
        }

        public async void Create(T entity)
        {
            await _context.AddAsync(entity);
        }

        public Task<T> Get(int id, CancellationToken cancellationToken)
        {
            return _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<List<T>> GetAll(/*CancellationToken cancellationToken*/)
        {
            return _context.Set<T>().ToListAsync(/*cancellationToken*/);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public void Delete(T entity)
        {
            entity.DeletedDate = DateTimeOffset.UtcNow;
            entity.IsDeleted = true;
            _context.Update(entity);
        }
    }
}
