using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Core.Models;

namespace TicketingSystem.Data.Contracts
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Tickets>> GetAllAsync();
        Task<Tickets?> GetByIdAsync(Guid Id);
        Task AddAsync(Tickets tickets);
        Task SaveAsync();
        Task UpdateAsync(Tickets tickets);
        Task DeleteAsync(Guid Id);
    }
}
