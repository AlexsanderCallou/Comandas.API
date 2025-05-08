
namespace Comandas.API.DTOs.Item{

    public class ComandaItemGetDTO{

        public int Id { get; set; }
        public int CardapioItemId { get; set; }
        public int ComandaId {get;set;}
        public string Titulo {get;set;} = default!;
        
    }

        public class ComandaItemPutDTO{
        public int Id { get; set; }
        public int CardapioItemId { get; set; }
        public bool Excluir { get; set; } = false;
    }

}