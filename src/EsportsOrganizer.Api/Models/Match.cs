namespace EsportsOrganizer.Api.Models
{
	public class Match
	{
		public int Id { get; set; }

		// Foreign Keys
		public int EventId { get; set; }
		public int Team1Id { get; set; }
		public int Team2Id { get; set; }

		// Navigation
		public Event? Event { get; set; } 
		public Team? Team1 { get; set; } 
		public Team? Team2 { get; set; } 

		// Match Data
		public int? Team1Score { get; set; }
		public int? Team2Score { get; set; }
		public DateTime MatchTime { get; set; }
		public MatchStatus Status { get; set; } = MatchStatus.Scheduled;
	}

	public enum MatchStatus
	{
		Scheduled, // Запланирован
		Live, // Идет
		Finished // Завершен
	}
}
