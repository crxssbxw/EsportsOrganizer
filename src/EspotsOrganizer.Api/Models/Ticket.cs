namespace EspotsOrganizer.Api.Models
{
	public class Ticket
	{
		public int Id { get; set; }

		public int EventId { get; set; }
		public Event? Event { get; set; }

		// Временное решение с пользователем. В будущем - связь с таблицей Users
		public string BuyerName { get; set; } = string.Empty;

		public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
		public TicketStatus Status { get; set; } = TicketStatus.Active;
	}

	public enum TicketStatus
	{
		Active, // Действителен
		Used, // Использован
		Refounded // Возвращен
	}
}
