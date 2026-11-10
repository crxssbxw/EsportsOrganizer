using EspotsOrganizer.Api.Data;
using EspotsOrganizer.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EspotsOrganizer.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TicketsController : ControllerBase
	{
		private readonly EsportsDbContext _context;

		public TicketsController(EsportsDbContext context)
		{
			_context = context;
		}

		// POST api/tickets
		[HttpPost]
		public async Task<ActionResult<Ticket>> BuyTicket([FromBody] BuyTicketRequest request)
		{
			var eventItem = await _context.Events.FindAsync(request.EventId);

			if (eventItem == null)
			{
				return NotFound("Event does not founded");
			}

			if (eventItem.Type != EventType.Offline)
			{
				return BadRequest("Tickets available only for offline events.");
			}

			var ticket = new Ticket
			{
				EventId = request.EventId,
				BuyerName = request.BuyerName,
				PurchaseDate = DateTime.UtcNow,
				Status = TicketStatus.Active
			};

			_context.Tickets.Add(ticket);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, ticket);
		}

		// GET api/tickets/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<Ticket>> GetTicketById(int id) 
		{
			var ticket = await _context.Tickets
				.Include(t => t.Event)
				.FirstOrDefaultAsync(t => t.Id == id);

			if (ticket == null)
			{
				return NotFound();
			}

			return Ok(ticket);
		}
	}

	// DTO for purchase request
	public class BuyTicketRequest
	{
		public int EventId { get; set; }
		public string BuyerName { get; set; } = string.Empty;
	}
}
