using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Core.Models;
using TicketingSystem.Data.Contracts;
using TicketingSystem.Data.Implementation;

namespace TicketingSystem.Data.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TicketRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Tickets>> GetAllAsync()
        {
            return await _dbContext.tickets.ToListAsync();
        }

        public async Task<Tickets?> GetByIdAsync(Guid Id)
        {
            return await _dbContext.tickets.FindAsync(Id);
        }

        public async Task AddAsync(Tickets tickets)
        {
            await _dbContext.tickets.AddAsync(tickets);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tickets tickets)
        {
            _dbContext.tickets.Update(tickets);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid Id)
        {
            var product = await _dbContext.tickets.FindAsync(Id);
            if (product != null)
            {
                _dbContext.tickets.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
