namespace EsportsOrganizer.Api.Models
{
	public class Article
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public string? ImageUrl { get; set; } // Ссылка на фото (пока просто строка)

		public int AuthorId { get; set; } // Автор статьи
		public User? Author { get; set; }

		public int? EventId { get; set; } // Статья может быть привязана к ивенту
		public Event? Event { get; set; }

		public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
	}
}
