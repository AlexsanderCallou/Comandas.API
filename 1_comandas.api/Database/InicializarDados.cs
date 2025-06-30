
using Comandas.Domain;
using Comandas.Data;

namespace Comandas.API.DataBase {

    public static class InicializarDados{

        public static void Semear(ComandasDBContext banco){

            if (!banco.CardapioItems.Any())
            {

                banco.CardapioItems.AddRange(
                    new CardapioItem()
                    {
                        Titulo = "X-Salada",
                        Descricao = "Bife, ovo, presunto e queijo",
                        Preco = 10.00M,
                        PossuiPreparo = true
                    },
                        new CardapioItem()
                        {
                            Titulo = "X-Bacon",
                            Descricao = "Bife, ovo, presunto e queijo",
                            Preco = 12.00M,
                            PossuiPreparo = true
                        },
                       new CardapioItem()
                       {
                           Titulo = "Coca Cola KS",
                           Descricao = "Coca de vidro.",
                           Preco = 4.00M,
                           PossuiPreparo = false
                       }

                );
            }

            if(!banco.Mesas.Any()){
                banco.Mesas.AddRange(
                    new Mesa(){
                        NumeroMesa = 1,
                        SituacaoMesa = 1
                    },
                    new Mesa(){
                        NumeroMesa = 2,
                        SituacaoMesa = 1
                    },
                    new Mesa(){
                        NumeroMesa = 3,
                        SituacaoMesa = 1
                    }
                );

            }

            if(!banco.Usuarios.Any()){
                banco.Usuarios.AddRange(
                    new Usuario{
                        Nome = "Alex",
                        Email = "alex@email.com",
                        Senha = "1234"
                    }
                );
            }
            
            if (!banco.Comandas.Any())
            {
                var comanda = new Comanda
                {
                    NomeCliente = "Alex",
                    NumeroMesa = 1,
                    SituacaoComanda = 1
                };
                banco.Comandas.Add(comanda);

                ComandaItem[] comandaItems = {
                                        new ComandaItem(){
                                            Comanda = comanda,
                                            CardapioItemId = 1
                                        },
                                        new ComandaItem(){
                                            Comanda = comanda,
                                            CardapioItemId = 2
                                        },
                                        new ComandaItem(){
                                            Comanda = comanda,
                                            CardapioItemId = 3
                                        }
                };
                if (!banco.ComandaItems.Any())
                {
                    banco.ComandaItems.AddRange(comandaItems);
                }

                PedidoCozinha pedidoCozinha = new PedidoCozinha { Comanda = comanda };

                PedidoCozinhaItem[] pedidoCozinhaItems = {
                                    new PedidoCozinhaItem{
                                        PedidoCozinha = pedidoCozinha,
                                        ComandaItem = comandaItems[0]
                                    }

                };

                banco.PedidosCozinha.Add(pedidoCozinha);
                banco.PedidoCozinhaItems.AddRange(pedidoCozinhaItems);

            }

            if (!banco.Usuarios.Where(u => u.Nome == "login").Select(u => u.Id).Any())
            {
                var usuarioLogin = new Usuario
                {
                    Nome = "login",
                    Email = "string",
                    Senha = "string"
                };
                banco.Usuarios.Add(usuarioLogin);
                
                banco.SaveChanges();
            }

        }

    }

}




