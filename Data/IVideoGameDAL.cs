using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VGDBAPI.Models;

namespace VGDBAPI.Data
{
    public interface IVideoGameDAL
    {
        IEnumerable<Game> GetCollection();
        IEnumerable<Game> SearchForGames(string key);
        IEnumerable<Game> FilterCollection(string genre, string platform, string rating);
        void AddGame(Game game);
        void DeleteGame(int index);
    }
}
