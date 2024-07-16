using EVillaAgency.BusinessLayer.Abstract;
using EVillaAgency.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.BusinessLayer.Concrete
{
    public class GenericRepository<T> : IGenericService<T> where T : class
    {
        private readonly AppDbContext dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> GetByIDAsync(int id)
        {
            try
            {
                var entity = await dbContext.Set<T>().FindAsync(id);

                if (entity == null)
                {
                    throw new Exception($"Entity with ID {id} not found.");
                }

                return entity;
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                throw new Exception($"Error getting entity by ID: {ex.Message}", ex);
            }
        }

        public async Task<List<T>> GetListAsync()
        {
            try
            {
                return await dbContext.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                throw new Exception($"Error getting entity list: {ex.Message}", ex);
            }
        }

        public async Task<List<T>> GetListByFilterAsync(Expression<Func<T, bool>> filter)
        {
            try
            {
                return await dbContext.Set<T>().Where(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                throw new Exception($"Error getting entity list by filter: {ex.Message}", ex);
            }
        }

        public async Task InsertAsync(T entity)
        {
            try
            {
                await dbContext.Set<T>().AddAsync(entity);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                throw new Exception($"Error inserting entity: {ex.Message}", ex);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                dbContext.Set<T>().Update(entity);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                throw new Exception($"Error updating entity: {ex.Message}", ex);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                dbContext.Set<T>().Remove(entity);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                throw new Exception($"Error deleting entity: {ex.Message}", ex);
            }
        }
    }
}