namespace Comandas.Shared.Enumeration{

    enum SituacaoComanda{
        Aberto = 1,
        Fechado = 2
    }
    enum SituacaoMesa{
        Disponivel = 0,
        Ocupada = 1
    }
    enum SituacaoPedidoCozinha{
        Pendente = 1,
        EmPreparo = 2,
        Finalizado = 3,
        Entregue = 4,
    }

}