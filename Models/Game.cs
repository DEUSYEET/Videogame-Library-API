using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VGDBAPI.Models
{
    public class Game
    {
        public Game(string title, string platform, string genre, string rating, int year, string image)
        {
            Title = title;
            Platform = platform;
            Genre = genre;
            Rating = rating;
            ReleaseYear = year;
            Image = image;
        }
        public Game(string title, string platform, string genre, string rating, int year)
        {
            Title = title;
            Platform = platform;
            Genre = genre;
            Rating = rating;
            ReleaseYear = year;
        }
        public Game() {}

        [Key]
        public int Index { get; set; }


        [Required(ErrorMessage ="Required", AllowEmptyStrings = false)]
        public string Title { get; set; } = "Default";



        [Required(ErrorMessage = "Required", AllowEmptyStrings = false)]
        public string Platform { get; set; } = "Default";


        [Required(ErrorMessage = "Required", AllowEmptyStrings = false)]
        public string Genre { get; set; } = "Default";

        [RegularExpression(@"^(RP)|(eC)|(E)|(E10)|(T)|(M)|(Ao)|(Not Rated)$", ErrorMessage = "Rating Must Be Either RP|eC|E|E10|T|M|Ao|Not Rated")]
        public string Rating { get; set; } = "Default";


        [Required(ErrorMessage ="Required", AllowEmptyStrings = false)]
        public int ReleaseYear { get; set; } = 0;


        [Url]
        public string Image { get; set; } = null;
        public string LoanedTo { get; set; } 
        public DateTime LoanDate { get; set; }
    }
}
