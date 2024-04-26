using App.Entities.Models;

namespace WebUI.Dtos
{
    public class SimilarPostDto
    {
        public List<Post> Posts { get; set; }
        public List<Favorite> Favorites { get; set; }

    }
}
