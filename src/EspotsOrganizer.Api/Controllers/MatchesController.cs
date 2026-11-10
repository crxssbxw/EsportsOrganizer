using EspotsOrganizer.Api.Data;
using EspotsOrganizer.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EspotsOrganizer.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MatchesController : ControllerBase
	{
		private readonly EsportsDbContext _context;

		public MatchesController(EsportsDbContext context)
		{
			_context = context;
		}

		// GET api/mathces
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Match>>> GetAllMatches()
		{
			var matches = await _context.Matches
				.Include(m => m.Team1)
				.Include(m => m.Team2)
				.Include(m => m.Event)
				.ToListAsync();

			return Ok(matches);
		}

		// GET api/mathces/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<Match>> GetMatchById(int id)
		{
			var match = await _context.Matches
				.Include(m => m.Team1)
				.Include(m => m.Team2)
				.Include(m => m.Event)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (match == null)
			{
				return NotFound();
			}

			return Ok(match);
		}

		// POST api/matches
		[HttpPost]
		public async Task<ActionResult<Match>> CreateMatch(Match match)
		{
			var team1Exists = await _context.Teams.AnyAsync(t => t.Id == match.Team1Id);
			var team2Exists = await _context.Teams.AnyAsync(t => t.Id == match.Team2Id);
			var eventExists = await _context.Teams.AnyAsync(e => e.Id == match.EventId);

			// Существуют ли команды и событие
			if (!team1Exists || !team2Exists || !eventExists)
			{
				return BadRequest("One of more referenced entities do not exist.");
			}

			// Команда не может играть сама с собой
			if (match.Team1Id == match.Team2Id)
			{
				return BadRequest("A team cannot play against itself.");
			}

			_context.Matches.Add(match);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetMatchById), new { id = match.Id }, match);
		}

		// PATCH: api/matches/{id}/score
		// Эндпоинт для обновления счета матча
		[HttpPatch("{id}/score")]
		public async Task<IActionResult> UpdateMatchScore(int id, [FromBody] UpdateScoreDto scoreDto)
		{
			var match = await _context.Matches.FindAsync(id);

			if (match == null)
			{
				return NotFound();
			}

			// Обновление счета
			match.Team1Score = scoreDto.Team1Score;
			match.Team2Score = scoreDto.Team2Score;

			// Если обе команды имеют счет, то матч заверешен
			if (scoreDto.Team1Score.HasValue && scoreDto.Team2Score.HasValue)
			{
				match.Status = MatchStatus.Finished;
			}
			else
			{
				match.Status = MatchStatus.Live;
			}

			await _context.SaveChangesAsync();
			return NoContent();
		}

		// DTO - Data Transfer Object
		public class UpdateScoreDto
		{
			public int? Team1Score { get; set; }
			public int? Team2Score { get; set; }
		}
	}
}
