using System;
using System.ComponentModel.DataAnnotations;

namespace MovieList
{
    public class Movie
    {
        [Required]
        public int Id { get; set; }
        
        [MinLength(2)]
        public string Title { get; set; }
        
        [MinLength(2)]
        public string Director { get; set; }

        //Between years : 1900-2099
        [RegularExpression("^(19|20)[0-9]{2}")]
        public int ReleaseDate { get; set; }
        
        [Required, MinLength(2)]
        public string Genre { get; set; }
    }
}