using EsportsOrganizer.Api.Data;
using EsportsOrganizer.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsportsOrganizer.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TeamsController : ControllerBase
	{
		private readonly EsportsDbContext _context;
		public TeamsController(EsportsDbContext context)
		{
			_context = context;
		}

		//GET: api/teams
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Team>>> GetAllTeams()
		{
			var teams = await _context.Teams.ToListAsync();
			return Ok(teams);
		}

		//POST: api/teams
		[HttpPost]
		public async Task<ActionResult<Team>> CreateTeam(Team team)
		{
			_context.Teams.Add(team);

			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetTeamById), new { id = team.Id }, team);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Team>> GetTeamById(int id)
		{
			var team = await _context.Teams.FindAsync(id);

			if (team == null)
			{
				return NotFound();
			}

			return Ok(team);
		}
	}
}
