using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VGDBAPI.Data;
using VGDBAPI.Models;

namespace VGDBAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class HomeController : Controller
    {
        private VideoGameDBDAL GameRepo;

        public HomeController(VideoGameDBContext context) : base()
        {
            GameRepo = new VideoGameDBDAL(context);
        }



        //•	GetCollection()
        [HttpGet("getcollection")]
        public JsonResult Library()
        {
            return Json(GameRepo.GetCollection());
        }




        //•	SearchForGames(string key)
        [HttpGet("search")]
        public JsonResult Search([FromForm, Required]string key)
        {
            return Json(GameRepo.SearchForGames(key));
        }





        //•	AddGame()
        [HttpPost("addgame")]
        public JsonResult Add([FromForm, Required]string title, [FromForm, Required]string platform, [FromForm, Required]string genre, [FromForm, Required]string rating, [FromForm, Required] int year, [FromForm, Url]string image)
        {
            Game g;
            if (image == null)
            {
                g = new Game(title, platform, genre, rating, year);
            }
            else
            {
                g = new Game(title, platform, genre, rating, year, image);
            }
            GameRepo.AddGame(g);
            return Json(g);
        }





        //•	DeleteGame()
        [HttpPost("delete")]
        public JsonResult Remove([FromForm, Required]int index)
        {
            GameRepo.DeleteGame(index);
            return Json($"Game {index} Deleted");
        }





        //•	LoanGame()
        [HttpPost("borrow")]
        public JsonResult Borrow([FromForm, Required]string name, [FromForm, Required]DateTime date, [FromForm, Required]int index)
        {
            Game g = GameRepo.GetCollection().FirstOrDefault(e => e.Index == index);
            g.LoanedTo = name;
            g.LoanDate = date;
            GameRepo.DeleteGame(index);
            GameRepo.EditGame(g);
            return Json(g);
        }

        [HttpPost("return")]
        public JsonResult Return([FromForm, Required]int index)
        {
            Game g = GameRepo.GetCollection().FirstOrDefault(e => e.Index == index);
            g.LoanedTo = null;
            g.LoanDate = DateTime.MinValue;
            GameRepo.DeleteGame(index);
            GameRepo.EditGame(g);
            return Json(g);
        }







        //•	FilterGame() based on genre, platform, and ESRB rating
        [HttpPost("filter")]
        public JsonResult Filter([FromForm]string Genre, [FromForm]string Rating, [FromForm]string Platform)
        {
            GameRepo.GetCollection();
            return Json(GameRepo.FilterCollection(Genre, Platform, Rating));
        }




        //Edit Game

        [HttpPost("edit")]
        public IActionResult Edit([FromForm, Required] int index, [FromForm]string title, [FromForm]string platform, [FromForm]string genre, [FromForm]string rating, [FromForm] int year, [FromForm, Url]string image)
        {
            GameRepo.GetCollection();
            Game g = GameRepo.GetGameByID(index);


            if (title != null)
            {
                g.Title = title;
            }
            if (platform != null)
            {
                g.Platform = platform;
            }
            if (genre != null)
            {
                g.Genre = genre;
            }
            if (rating != null)
            {
                g.Rating = rating;
            }
            if (year != default)
            {
                g.ReleaseYear = year;
            }
            if (image != null)
            {
                g.Image = image;
            }
            GameRepo.DeleteGame(index);
            GameRepo.EditGame(g);
            return Json(g);

        }

    }
}
