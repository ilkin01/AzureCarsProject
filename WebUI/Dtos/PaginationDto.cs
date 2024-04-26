using App.Entities.Models;

namespace WebUI.Dtos
{
    public class PaginationDto
    {

        public int CurrentPage { get; set; }
        public int SelectedPage { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
        public int TotalPages { get; set; }
		public int TotalPages2 { get; set; }

		public List<Favorite> Favorites { get; set;} = new List<Favorite>();
        public List<Post> Vips { get; set; } = new List<Post>();
    }
}
