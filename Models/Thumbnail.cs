namespace MyBackend.Models
{
    public class Thumbnail
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty; // Inicializar con un valor por defecto
        public string Categoria { get; set; } = string.Empty; // Inicializar con un valor por defecto
        public string Descripcion { get; set; } = string.Empty; // Inicializar con un valor por defecto
        public string ImagenUrl { get; set; } = string.Empty; // Inicializar con un valor por defecto
        public DateTime FechaPublicacion { get; set; }
    }
}
