namespace Comandas.API.Enumeration{

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
        Andamento = 2,
        Finalizado = 3,
        Entregue = 4,
    }

}