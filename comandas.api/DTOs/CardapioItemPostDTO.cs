namespace Comandas.API.DTOs{

    public class CardapioItemPostDTO {

        public string Titulo { get; set; } = default!;
        public string  Descricao { get; set; } = default!;
        public decimal Preco { get; set; }  
        public bool PossuiPreparo { get; set; } 
    

    }

}