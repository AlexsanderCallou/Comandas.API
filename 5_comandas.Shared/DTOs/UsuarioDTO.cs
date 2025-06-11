using System.Reflection.Metadata;

namespace Comandas.Shared.DTOs
{

    public class UsuarioGetDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = default!;
        public string Email { get; set; } = default!;
    }

    public class UsuarioPostDTO
    {
        public string Nome { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Senha { get; set; } = default!;
    }

    public class UsuarioPostResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = default!;
        public string Email { get; set; } = default!;
    }

    public class UsuarioPutDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Senha { get; set; } = default!;
    }

    public class UsuarioPutResponseDTO
    {

    }

    public class UsuarioDeleteResponseDTO
    {

    }

    public class UsuarioLoginResponseDTO
    {
        public int Id { get; set; }
        public string BearerToken { get; set; } = default!;
    }

    public class UsuarioLoginServiceDTO
    {
        public UsuarioLoginResponseDTO? usuarioLoginResponseDTO { get; set; }
        public bool RetornoSucesso { get; set; }
        public string Retorno { get; set; } = default!;

    }

    public class UsuarioLoginResquestDTO
    {
        public string Email { get; set; } = default!;
        public string Senha { get; set; } = default!;
    }
    
    public class UsuarioLoginRepositorytDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Senha { get; set; } = default!;
    }
}