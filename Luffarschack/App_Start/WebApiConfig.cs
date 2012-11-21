using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Luffarschack
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
               name: "Join Game Route",
               routeTemplate: "api/games/{gameId}/players",
               defaults: new { controller = "Games", action = "JoinGame" }
           );

            config.Routes.MapHttpRoute(
                name: "Game Move Route",
                routeTemplate: "api/games/{gameId}/moves",
                defaults: new { controller = "Games", action = "GameMove" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
