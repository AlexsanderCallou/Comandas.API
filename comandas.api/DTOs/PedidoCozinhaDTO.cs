using System.Runtime.InteropServices;
using System.Threading.Tasks.Dataflow;

namespace Comandas.API.DTOs{

    public class PedidoCozinhaGetDTO {

        public int Id { get; set; }
        public int ComandaId {get; set;}
        public int SituacaoId {get; set;}
        
        //public virtual ICollection<PedidoCozinhaItem> PedidoCozinhaItems {get;set;}

    }

}
 





