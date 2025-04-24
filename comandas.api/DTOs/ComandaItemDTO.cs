using System.Runtime.CompilerServices;

namespace Comandas.API.DTOs{

    public class ComandaItemGetDTO{

        public int Id { get; set; }
        public int CardapioItemId { get; set; }
        public int ComandaId {get;set;}
        public string Titulo {get;set;} = default!;
        
    }

}