﻿namespace MyBackend.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }

        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
