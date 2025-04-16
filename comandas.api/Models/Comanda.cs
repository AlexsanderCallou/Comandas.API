using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Comandas.API.Models;

namespace Comandas.API.Models {

    public class Comanda {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; } = default!;
        public int SituacaoComanda { get; set; } = 1;      
        public virtual ICollection<ComandaItem> ComandaItems {get;set;} = default!;

    }
}