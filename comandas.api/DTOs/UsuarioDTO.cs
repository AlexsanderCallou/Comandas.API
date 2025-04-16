namespace Comandas.API.DTOs{

    public class UsuarioGetDTO{
        public int Id { get; set; }
        public string Nome { get; set; } = default!;   
        public string Email { get; set; } = default!;

    }

    public class UsusarioPostDTO{
        public string Nome { get; set; } = default!;   
        public string Email { get; set; } = default!;
        public string Senha { get; set; } = default!;

    }

    public class UsuarioPostResponseDTO{
        public int Id { get; set; }
        public string Nome { get; set; } = default!;   
        public string Email { get; set; } = default!;       
    }

    public class UsuarioPutDTO{
        public int Id { get; set; }
        public string Nome { get; set; } = default!;   
        public string Email { get; set; } = default!;
        public string Senha { get; set; } = default!;
    }

    public class UsuarioLoginResponseDTO{
        public int Id { get; set; }
        public string BearerToken { get; set; } = default!;

    }

    public class UsuarioLoginResquestDTO{
        public string Email { get; set; } = default!;
        public string Senha { get; set; } = default!;
    }


}