using System.ComponentModel.DataAnnotations;

namespace anime_list.Data
{
  
    public class AnimeRatings
    {
        [Key]
        public int AnimeRatingId { get; set; }
        public int AnimeListId { get; set; }
        public int AnimeRating { get; set; }
       

    }
}
