using System.ComponentModel.DataAnnotations;

namespace CasaEventos.Models
{
    public class Casa
    {
        [Key]
        public int CasaId { get;set; }

    
        public string Nome { get;set; } 


        public string Endereco { get;set; }
    }
}