namespace EspotsOrganizer.Api.Models
{
	public class Team
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? LogoUrl { get; set; }
		public string? Region { get; set; }

	}
}
