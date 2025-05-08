using Comandas.API.DTOs.Item;

namespace Comandas.API.DTOs{

    public class ComandaGetDTO{

        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; } = default!;
        public int SituacaoComanda { get; set; } = 1;      
        public ICollection<ComandaItemGetDTO> ComandaItems {get;set;} = default!;

    }

    public class ComandaPostDTO{

        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; } = default!;
        public int[] CardapioItens { get; set; } = [];

    }

    public class ComandaPutDTO{
       
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; } = default!;      
        public List<ComandaItemPutDTO> ComandaItems { get; set; } = [];
    }

    public class ComandaPatchDTO{
        public int SituacaoComanda { get; set; }
    }

    public class ComandaFilterDTO{
        public int? SituacaoComanda { get; set; }
    }

}