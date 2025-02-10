using Microsoft.EntityFrameworkCore;
using BajoCero.Api.Data;
using BajoCero.Api.Models; 

namespace BajoCero.Api.Endpoints
{
    public static class GameEndpoints
    {
        public static IEndpointRouteBuilder MapGameEndpoints(this IEndpointRouteBuilder routes)
        {
            var api = routes.MapGroup("/api");

            // Endpoint para crear una partida
            api.MapPost("/games", async (ApplicationDbContext db, int hostUserId) =>
            {
                var game = new Game
                {
                    HostUserId = hostUserId,
                    Status = "Waiting",
                    Players = []
                };

                game.Players.Add(new GamePlayer { UserId = hostUserId });

                db.Games.Add(game);
                await db.SaveChangesAsync();
                return Results.Created($"/api/games/{game.Id}", game);
            });

            // Endpoint para unirse a una partida
            api.MapPost("/games/{gameId}/join", async (int gameId, int userId, ApplicationDbContext db) =>
            {
                var game = await db.Games
                                   .Include(g => g.Players)
                                   .FirstOrDefaultAsync(g => g.Id == gameId);
                if (game is null)
                {
                    return Results.NotFound("Partida no encontrada.");
                }
                if (game.Status != "Waiting")
                {
                    return Results.BadRequest("La partida ya está en curso o finalizada.");
                }

                game.Players.Add(new GamePlayer { UserId = userId });
                await db.SaveChangesAsync();
                return Results.Ok(game);
            });

            return routes;
        }
    }
}
