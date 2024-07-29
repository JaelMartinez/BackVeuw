using System.ComponentModel.DataAnnotations.Schema;

namespace MyBackend.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        [Column("UserId")] // Mapea la propiedad UserId a la columna UserId en la base de datos
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageSrc { get; set; } = string.Empty;
        public string VideoSrc { get; set; } = string.Empty;

        public Usuario User { get; set; } // No necesita anotación, ya que es solo una propiedad de navegación
    }
}
