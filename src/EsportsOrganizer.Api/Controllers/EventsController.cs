using EsportsOrganizer.Api.Data;
using EsportsOrganizer.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsportsOrganizer.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EventsController : ControllerBase
	{
		private readonly EsportsDbContext _context;
		public EventsController(EsportsDbContext context)
		{
			_context = context;
		}

		//GET api/events?type=...
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Event>>> GetAllEvents([FromQuery] EventType? type = null)
		{
			IQueryable<Event> query = _context.Events;

			if (type.HasValue)
			{
				query = query.Where(e => e.Type == type.Value);
			}

			var events = await query.ToListAsync();
			return Ok(events);
		}

		//GET api/events/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<Event>> GetEventById(int id)
		{
			var eventItem = await _context.Events.FindAsync(id);

			if (eventItem == null)
			{
				return NotFound();
			}

			return Ok(eventItem);
		}

		//POST api/events
		[HttpPost]
		public async Task<ActionResult<Event>> CreateEvent(Event eventItem)
		{
			_context.Events.Add(eventItem);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetEventById), new { id = eventItem.Id }, eventItem);
		}
	}
}
