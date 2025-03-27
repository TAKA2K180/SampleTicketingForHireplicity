using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Core.Models;
using TicketingSystem.Data.Contracts;

namespace TicketingSystem.Web.Controllers.v1
{
    [Route("api/v1/Tickets")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        public TicketsController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tickets tickets)
        {
            try
            {
                await _ticketRepository.AddAsync(tickets);
                await _ticketRepository.SaveAsync();
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tickets = await _ticketRepository.GetAllAsync();
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            try
            {
                var tickets = await _ticketRepository.GetByIdAsync(Id);
                if (tickets == null)
                    return NotFound();

                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Tickets updatedTicket)
        {
            try
            {
                var existingTicket = await _ticketRepository.GetByIdAsync(id);
                if (existingTicket == null)
                    return NotFound();

                existingTicket.BuildingCode = updatedTicket.BuildingCode;
                existingTicket.Description = updatedTicket.Description;
                existingTicket.CurrentStatus = updatedTicket.CurrentStatus;
                existingTicket.LastModifiedBy = updatedTicket.LastModifiedBy;
                existingTicket.LastModifiedDate = DateTime.UtcNow;

                await _ticketRepository.UpdateAsync(existingTicket);
                return Ok(existingTicket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var ticket = await _ticketRepository.GetByIdAsync(id);
                if (ticket == null)
                    return NotFound();

                await _ticketRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
