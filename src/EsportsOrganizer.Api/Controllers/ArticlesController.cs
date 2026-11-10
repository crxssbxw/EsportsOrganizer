using EsportsOrganizer.Api.Data;
using EsportsOrganizer.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsportsOrganizer.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ArticlesController : ControllerBase
	{
		private readonly EsportsDbContext _context;
		public ArticlesController(EsportsDbContext context)
		{
			_context = context;
		}

		// GET api/articles
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Article>>> GetAllArticles()
		{
			var articles = await _context.Articles
				.Include(a => a.Author)
				.Include(a => a.Event)
				.OrderByDescending(a => a.PublishedAt)
				.ToListAsync();

			return Ok(articles);
		}

		// POST api/articles
		[HttpPost]
		public async Task<ActionResult<Article>> CreateArticle(
			[FromBody] CreateArticleRequest request,
			[FromHeader(Name = "X-User-Role")] string? userRole)
		{
			if (userRole != "Journalist" && userRole != "Admin")
			{
				return StatusCode(403, "Acces denied. Only journalists and admin can create an article.");
			}

			var author = await _context.Users.FindAsync(request.AuthorId);
			if (author == null || (author.Role != UserRole.Journalist && author.Role != UserRole.Admin))
			{
				return BadRequest("Author was not founded or has not access.");
			}

			var article = new Article
			{
				Title = request.Title,
				Content = request.Content,
				ImageUrl = request.ImageUrl,
				AuthorId = author.Id,
				EventId = request.EventId,
				PublishedAt = DateTime.UtcNow
			};

			_context.Articles.Add(article);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetArticleById), new { id = article.Id }, article);
		}

		// GET api/articles/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<Article>> GetArticleById(int id)
		{
			var article = await _context.Articles
				.Include(a => a.Author)
				.Include(a => a.Event)
				.FirstOrDefaultAsync(a => a.Id == id);

			if (article == null)
			{
				return NotFound();
			}

			return Ok(article);
		}
	}

	public class CreateArticleRequest
	{
		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public string? ImageUrl { get; set; }
		public int AuthorId { get; set; }
		public int? EventId { get; set; }
 	}
}
