using EsportsOrganizer.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EsportsOrganizer.Api.Data
{
	public class EsportsDbContext : DbContext
	{
		public EsportsDbContext(DbContextOptions<EsportsDbContext> options) : base(options)
		{

		}

		public DbSet<Team> Teams { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<Match> Matches { get; set; }
		public DbSet<Ticket> Tickets { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Article> Articles { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
