using Microsoft.AspNetCore.SignalR;

namespace BajoCero.Api.Hubs
{
    public class GameHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "Conexión establecida con el hub.");
            await base.OnConnectedAsync();
        }

        public async Task PlayCard(string gameId, string cardCode)
        {
            // cardCode podría ser algo como "2H" (2 de Corazones) o "KD" (Rey de Diamantes)
            // Aquí iría la lógica de validación y actualización de la partida.
            // Por ejemplo, podrías:
            // 1. Validar que la partida existe y que el jugador forma parte.
            // 2. Registrar la jugada en una estructura en memoria.
            // 3. Notificar a todos los jugadores la jugada (si corresponde).

            await Clients.Group(gameId).SendAsync("ReceivePlay", Context.ConnectionId, cardCode);
        }

        public async Task JoinGame(string gameId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            await Clients.Group(gameId).SendAsync("PlayerJoined", Context.ConnectionId);
        }
    }
}
