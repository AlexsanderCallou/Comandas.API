namespace Comandas.Shared.Enumeration{

    public enum SituacaoComanda{
        Aberto = 1,
        Fechado = 2
    }
    public enum SituacaoMesa{
        Disponivel = 0,
        Ocupada = 1
    }
    public enum SituacaoPedidoCozinha{
        Pendente = 1,
        EmPreparo = 2,
        Finalizado = 3,
        Entregue = 4,
    }

}