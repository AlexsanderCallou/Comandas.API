namespace Comandas.API.DTOs{

    public class ComandaGetDTO{

        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; } = default!;
        public int SituacaoComanda { get; set; } = 1;      
        public ICollection<ComandaItemGetDTO> ComandaItems {get;set;} = default!;

    }
}