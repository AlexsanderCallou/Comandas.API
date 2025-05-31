namespace Comandas.Shared.DTOs {

    public class CardapioItemGetDTO {
        public int Id { get; set; }
        public string Titulo { get; set; } = default!;
        public string  Descricao { get; set; } = default!;
        public decimal Preco { get; set; }  
        public bool PossuiPreparo { get; set; } 
    }

    public class CardapioItemPostDTO {
        public string Titulo { get; set; } = default!;
        public string  Descricao { get; set; } = default!;
        public decimal Preco { get; set; }  
        public bool PossuiPreparo { get; set; } 
    }
     public class CardapioItemResponsePostDTO {
        public int Id { get; set; }
        public string Titulo { get; set; } = default!;
        public string  Descricao { get; set; } = default!;
        public decimal Preco { get; set; }  
        public bool PossuiPreparo { get; set; } 
    }

    public class CardapioItemPutDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public decimal Preco { get; set; }
        public bool PossuiPreparo { get; set; }
    }
}