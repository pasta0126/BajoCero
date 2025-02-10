namespace BajoCero.Api.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int HostUserId { get; set; }
        public string? Status { get; set; }  // "Waiting", "InProgress", "Finished"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? FinishedAt { get; set; }

        public List<GamePlayer> Players { get; set; }
    }
}
