using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace  Comandas.API.Models{

    public class PedidoCozinha {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ComandaId {get; set;}

        public virtual Comanda Comanda {get; set;}

        public int SituacaoId {get; set;}

        public virtual ICollection<PedidoCozinhaItem> PedidoCozinhaItems {get;set;}

        

    }
    
}