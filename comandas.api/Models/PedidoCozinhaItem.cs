
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comandas.API.Models {

    public class PedidoCozinhaItem {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public int PedidoCozinhaId {get;set;}


        public virtual PedidoCozinha PedidoCozinha {get;set;} = default!;


        public int ComanadaItemId {get; set;}

        
        public virtual ComandaItem ComandaItem {get;set;} = default!;

    }
}
