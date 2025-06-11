using System.Reflection.Metadata;

namespace Comandas.Shared.DTOs
{
    public class ResponseGenericoDTO<T>
    {
        public bool RetornoSucesso { get; set; }
        public string Mensagem { get; set; } = default!;
        public List<string> Erros { get; set; } = default!;
    }

}