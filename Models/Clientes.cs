using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace S.NotaAtualizacao.Models
{
    [Table(nameof(Clientes))]
    public class Clientes
    {
        [Key]
        public int Codigo { get; set; } 

        [DisplayName("Sigla")]
        public string Sigla { get; set; }

        [DisplayName("Nome")]
        public string Nome { get; set; }

        [NotMapped, DisplayName("Codigo")]
        public string CodigoString 
        {
            get 
            {
                return Codigo.ToString().PadLeft(5, '0');
            }
        }
    }
}
