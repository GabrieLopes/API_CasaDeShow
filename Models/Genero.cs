using System.ComponentModel.DataAnnotations;

namespace CasaEventos.Models
{
    public class Genero
    {
        [Required]
        [Key]
        public int GeneroId { get;set; }

        [Required]
        public string GeneroNome { get;set; }
    }
}