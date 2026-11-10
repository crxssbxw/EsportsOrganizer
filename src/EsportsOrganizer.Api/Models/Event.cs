namespace EsportsOrganizer.Api.Models
{
	public class Event
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public EventType Type { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string? Location { get; set; }
		public decimal? TicketPrice { get; set; }

	}

	public enum EventType 
	{
		Online,
		Offline
	}

}
