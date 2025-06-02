
namespace Comandas.Shared.DTOs
{

    public class PedidoCozinhaGetDTO
    {

        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; } = default!;
        public string TituloItem { get; set; } = default!;


    }

    public class PedidioCozinhaPatchDTO
    {
        public int SituacaoPedidoCozinhaId { get; set; }
    }

}






