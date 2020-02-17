using System.ComponentModel.DataAnnotations;

namespace CasaEventos.Models
{
    public class Casa
    {
        [Required]
        [Key]
        public int CasaId { get;set; }

        [Required(ErrorMessage="Nome da Casa de Show é obrigatório!")]
        [StringLength(100, ErrorMessage="Nome da casa de show é muito grande, tente um nome menor")]
        [MinLength(2, ErrorMessage="Nome da casa de show é muito pequeno, tente um nome com pelo menos 2 caracteres")]
        public string Nome { get;set; } 

        [Required(ErrorMessage="Endereço da casa de show é obrigatório!")]
        [StringLength(150, ErrorMessage="Endereço tem um nome muito extenso, tente um nome menor")]
        [MinLength(2, ErrorMessage="Endereço tem um nome muito pequeno, tente um nome com pelo menos 2 caracteres")]
        public string Endereco { get;set; }
    }
}