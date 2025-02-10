namespace BajoCero.Api.Models
{
    public class GamePlayer
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int CurrentScore { get; set; } = 50;  // Puntuación inicial
        public int? SeatNumber { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        public Game Game { get; set; }
        public User User { get; set; }
    }
}
