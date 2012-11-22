using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Luffarschack.Models;

namespace Luffarschack.Controllers
{
    public class GamesController : ApiController
    {
        private static List<Game> games = new List<Game>
            {
                new Game {Id = 1, Board = new [] {0,0,0,0,0,0,0,0,0}, HSize = 3, VSize = 3, Players = new List<int> {1,2}, Winner = -1, CurrentPlayer = 1},
                new Game {Id = 2, Board = new [] {0,0,0,0,0,0,0,0,0}, HSize = 3, VSize = 3, Players = new List<int> {1}, Winner = -1, CurrentPlayer = 1}
            };

        // GET api/games
        public IEnumerable<Game> Get()
        {
            return games;
        }

        // GET api/games/5
        public HttpResponseMessage Get(int id)
        {
            var game = games.FirstOrDefault(g => g.Id == id);
            if (game == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid id: " + id);

            return Request.CreateResponse(HttpStatusCode.OK, game);
        }

        // POST api/games
        public HttpResponseMessage Post(BoardSize size)
        {
            if (ModelState.IsValid)
            {
                var game = new Game
                    {
                        Id = games.Count + 1,
                        Winner = -1,
                        Board = new int[size.Width * size.Height],
                        HSize = size.Width,
                        VSize = size.Height,
                        Players = new List<int> {1},
                        CurrentPlayer = 1
                    };
                games.Add(game);
                var response = Request.CreateResponse(HttpStatusCode.Created, new { Player = 1, GameId = game.Id });
                var uri = Url.Link("DefaultAPI", new {id = game.Id});
                response.Headers.Location = new Uri(uri);

                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "bad input");
        }

        // POST api/games/3/moves {player: 1, x: 3, y: 2 }
        [HttpPost]
        public HttpResponseMessage GameMove(int gameId, Move move)
        {
            var game = games.FirstOrDefault(g => g.Id == gameId);
            if (game == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid id: " + gameId);

            if (ValidMove(move, game))
            {
                game.Board[game.HSize * move.Y + move.X] = move.Player;
                game.CurrentPlayer = game.CurrentPlayer % 2 + 1;

                CheckBoardForWinner(game);

                return Request.CreateResponse(HttpStatusCode.OK, "Sweet");
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid move");
        }

        private void CheckBoardForWinner(Game game)
        {
            var bconv = new BoardConverter();
            var bchk = new Boardchecker();

            var board = bconv.ConvertToPlayerArray(game.Board, game.VSize);
            var winner = bchk.DetermineWinner(board);
            if ((int) winner > 0)
                game.Winner = (int) winner;
            else if (bchk.FullBoard(board))
                game.Winner = 0;
        }

        private bool ValidMove(Move move, Game game)
        {
            return ModelState.IsValid && game.Board[game.HSize * move.Y + move.X] == 0 && game.CurrentPlayer == move.Player && game.Winner == -1;
        }

        // POST api/games/3/players
        [HttpPost]
        public HttpResponseMessage JoinGame(int gameId)
        {
            var game = games.FirstOrDefault(g => g.Id == gameId);
            if (game == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid id: " + gameId);

            if(game.Players.Count == 2)
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Game is full");

            game.Players.Add(2);

            return Request.CreateResponse(HttpStatusCode.Created, new {Game = game, PlayerId = 2});
        }
    }
}
