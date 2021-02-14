using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VGDBAPI.Models;

namespace VGDBAPI.Data
{
    public class VideoGameDBDAL : IVideoGameDAL
    {
        VideoGameDBContext GameList;
        public static List<Game> PreloadGameList = new List<Game>
        {
            new Game{Title = "Kerbal Space Program", Genre="Sandbox", Image="/icons/ksp.jpg", Platform="PC", Rating="E", ReleaseYear=2011, LoanDate=DateTime.MinValue, LoanedTo= null},
            new Game{Title = "War Thunder", Genre="Action", Image="/icons/wt.jpg", Platform="PC", Rating="T", ReleaseYear=2012, LoanDate=DateTime.Now, LoanedTo=null},
            new Game{Title = "Minecraft", Genre="Sandbox", Image="/icons/mc.jpg", Platform="PC", Rating="E", ReleaseYear=2009, LoanDate= new DateTime(1945,8,15,1,1,1), LoanedTo= "Guy Fieri"},
            new Game{Title = "Team Fortress 2", Genre="Class FPS", Image="/icons/tf2.jpg", Platform="PC", Rating="M", ReleaseYear=2007, LoanDate=DateTime.MinValue, LoanedTo= null},
            new Game{Title = "VRChat", Genre="Having a Stroke in VR", Image="/icons/vr.jpg", Platform="PC", Rating="Not Rated", ReleaseYear=2017, LoanDate=DateTime.MinValue, LoanedTo= null}

        };

        public VideoGameDBDAL(VideoGameDBContext videoGameDBContext) : base()
        {
            GameList = videoGameDBContext;
        }




        public void AddGame(Game game)
        {

            bool isDupe = false;

            foreach (Game g in GameList.games)
            {
                if (game.Title == g.Title && game.Platform == g.Platform && game.Rating == g.Rating && game.ReleaseYear == g.ReleaseYear && game.Genre == g.Genre)
                {
                    isDupe = true;
                }
            }
            if (!isDupe)
            {
                GameList.AddAsync<Game>(game);
                GameList.SaveChangesAsync();
            }
        }

        public void DeleteGame(int index)
        {
            GameList.games.Remove(GameList.games.First(e => e.Index == index));
            GameList.SaveChangesAsync();
        }

        public IEnumerable<Game> FilterCollection(string genre, string platform, string rating)
        {

            List<Game> games = GameList.games.ToList<Game>();

            if (genre != null)
            {
                games.RemoveAll(e => !e.Genre.ToLower().Contains(genre.ToLower()));
            }

            if (platform != null)
            {
                games.RemoveAll(e => !e.Platform.ToLower().Contains(platform.ToLower()));
            }
            if (rating != null)
            {
                games.RemoveAll(e => e.Rating.ToLower() != rating.ToLower());
            }

            return games;
        }

        public IEnumerable<Game> GetCollection()
        {
            if (GameList.games.Count() == 0)
            {
                foreach (Game g in PreloadGameList)
                {
                    GameList.games.Add(g);
                }
                GameList.SaveChanges();
            }
            return GameList.games.Where(e => e != null);
        }
        public Game GetGameByID(int index)
        {
            return GameList.games.First(e => e.Index == index);
        }

        public IEnumerable<Game> SearchForGames(string key)
        {
            return GameList.games.Where(e => e.Title.ToLower().Contains(key.ToLower()));
        }

        public void EditGame(Game g)
        {
            GameList.games.Add(g);
            GameList.SaveChanges();
        }

    }

}

