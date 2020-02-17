using System.ComponentModel.DataAnnotations;

namespace CasaEventos.Models
{
    public class Genero
    {
        [Required]
        [Key]
        public int GeneroId { get;set; }

        [Required(ErrorMessage="Nome do genero é obrigatório!")]
        [StringLength(100, ErrorMessage="Nome do genero é muito grande, tente um nome menor")]
        [MinLength(2, ErrorMessage="Nome do genero é muito pequeno, tente um nome com pelo menos 2 caracteres")]
        public string GeneroNome { get;set; }
    }
}