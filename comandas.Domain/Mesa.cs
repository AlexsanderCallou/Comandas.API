

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comandas.Domain{

    public class Mesa {
        
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public  int NumeroMesa { get; set; } 
        public int SituacaoMesa { get; set; }

    }

}