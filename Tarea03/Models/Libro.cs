namespace Tarea03.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public DateTime FechaPublicacion { get; set; }

        public Libro()
        {

        }

        public Libro(int Id, string ISBN, string Titulo, string Autor, DateTime FechaPublicacion)
        {
            this.Id = Id;
            this.ISBN = ISBN;
            this.Titulo = Titulo;
            this.Autor = Autor;
            this.FechaPublicacion = FechaPublicacion;
        }
    }
}
