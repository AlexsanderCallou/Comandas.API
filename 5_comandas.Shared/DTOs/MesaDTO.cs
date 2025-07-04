namespace Comandas.Shared.DTOs 
{

    public class MesaGetDTO
    {
        public  int Id { get; set; }
        public int NumeroMesa { get; set; }
        public int SituacaoMesa { get; set; }
    }

    public class MesaPostDTO
    {
        public int NumeroMesa { get; set; }
        public int SituacaoMesa { get; set; }
    }

    public class MesaResponsePostDTO
    {
        public  int Id { get; set; }
        public int NumeroMesa { get; set; }
        public int SituacaoMesa { get; set; }
    }

    public class MesaPutDTO
    {
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public int SituacaoMesa { get; set; }
    }

}
