using Comandas.API.DataBase;
using Comandas.API.DTOs;
using Comandas.API.Models;
using Comandas.API.Enumeration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;


namespace Comandas.API.Controllers{


    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class ComandaController : ControllerBase{

        private readonly ComandasDBContext _banco;

        public ComandaController(ComandasDBContext comandasDBContext)
        {
            _banco = comandasDBContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComandaGetDTO>> GetComanda(int id){

            var comanda = await _banco.Comandas
                                .Include(c => c.ComandaItems)
                                    .ThenInclude(c => c.CardapioItem)
                                .FirstOrDefaultAsync(c => c.Id == id);


            if (comanda is null){
                return NotFound("Comanda não encontrada.");
            }

            var comandaGetDTO = new ComandaGetDTO {
                Id = comanda.Id,
                NomeCliente = comanda.NomeCliente,
                NumeroMesa = comanda.NumeroMesa,
                SituacaoComanda = comanda.SituacaoComanda,
                ComandaItems = comanda.ComandaItems.Select(c => new ComandaItemGetDTO{
                        Id = c.Id,
                        Titulo = c.CardapioItem.Titulo
                }).ToList()
            };

            return Ok(comandaGetDTO);
       }

       [HttpGet]
       public async Task<ActionResult<IEnumerable<ComandaGetDTO>>> GetComandas(){

        var comandas = await _banco.Comandas
                .Where(c => c.SituacaoComanda == (int)SituacaoComanda.Aberto)
                .Select(c => new ComandaGetDTO{
                    Id = c.Id,
                    NumeroMesa = c.NumeroMesa,
                    NomeCliente = c.NomeCliente,
                    SituacaoComanda = c.SituacaoComanda,

                    ComandaItems = c.ComandaItems
                        .Select(c => new ComandaItemGetDTO{
                            Id = c.Id,
                            Titulo = c.CardapioItem.Titulo
                        }).ToList()
                }).ToListAsync();


        return Ok(comandas);


       }

        [HttpPost]
        public async Task<ActionResult<ComandaGetDTO>> PostComanda(ComandaPostDTO comandaPostDTO){

            //verificar se a mesa esta disponivel

            var mesa = await _banco.Mesas.FirstOrDefaultAsync(c => c.NumeroMesa == comandaPostDTO.NumeroMesa);

            if (mesa is null){
                return BadRequest("Mesa não encontrada.");
            }

            if (mesa.SituacaoMesa == (int)SituacaoMesa.Ocupada){
                return BadRequest($"Mesa {mesa.NumeroMesa} esta ocupada.");
            }

            mesa.SituacaoMesa = (int)SituacaoMesa.Ocupada;

            //criar nova comanda.

            var comanda = new Comanda{
                NumeroMesa = comandaPostDTO.NumeroMesa,
                NomeCliente = comandaPostDTO.NomeCliente,
            };

            //salvar no contexto.

            await _banco.Comandas.AddAsync(comanda);

            //adcionar os itens.

            foreach(var item in comandaPostDTO.CardapioItens){                
                
                var cardapioItem = await _banco.CardapioItems.FirstOrDefaultAsync(c => c.Id == item);
                
                if (cardapioItem is null){
                    return BadRequest("Item não existe.");
                }

                var comandaItem = new ComandaItem{
                    CardapioItem = cardapioItem,
                    Comanda = comanda
                };

                await _banco.ComandaItems.AddAsync(comandaItem);
              
                if (cardapioItem.PossuiPreparo){
                    var pedidoCozinha = new PedidoCozinha{
                        Comanda = comanda,
                        SituacaoId = (int)SituacaoPedidoCozinha.Pendente,         
                    };
                
                    var pedidoCozinhaItem = new PedidoCozinhaItem{
                        PedidoCozinha = pedidoCozinha,
                        ComandaItem = comandaItem
                    };

                    await _banco.PedidosCozinha.AddAsync(pedidoCozinha);
                    await _banco.PedidoCozinhaItems.AddAsync(pedidoCozinhaItem);

                }
            };

            //salvar no banco.

            await _banco.SaveChangesAsync();

            //criar um novo comanda dto

            var comandaGetDTO = new ComandaGetDTO{
                Id = comanda.Id,
                NomeCliente = comanda.NomeCliente,
                NumeroMesa = comanda.NumeroMesa,
                SituacaoComanda = comanda.SituacaoComanda,
                ComandaItems = comanda.ComandaItems.Select(c => new ComandaItemGetDTO{
                    Id = c.Id,
                    CardapioItemId = c.CardapioItemId,
                    ComandaId = c.ComandaId,
                    Titulo = c.CardapioItem.Titulo
                }).ToList()
            };

            return CreatedAtAction("GetComanda", new {id = comanda.Id}, comandaGetDTO);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutComanda(int id, ComandaPutDTO comandaPutDTO){

            if (id != comandaPutDTO.Id){
                return BadRequest();
            };

            var comanda = await _banco.Comandas.FirstOrDefaultAsync(c => c.Id == id);

            if (comanda is null){
                return NotFound("Comanda não encontrada.");
            };

            //verificar se foi informado uma nova mesa. 

            if (comandaPutDTO.NumeroMesa > 0 && comandaPutDTO.NumeroMesa != comanda.NumeroMesa){
                
                var mesa = await _banco.Mesas.FirstOrDefaultAsync(c => c.NumeroMesa == comandaPutDTO.NumeroMesa);
                
                if (mesa is null){
                    return NotFound("Mesa não encontrada");
                }

                if (mesa.SituacaoMesa == (int)SituacaoMesa.Ocupada){
                    return BadRequest($"Mesa {mesa.NumeroMesa} está ocupada.");
                }

                mesa.SituacaoMesa = (int)SituacaoMesa.Ocupada;
                
                var mesaAtual = await _banco.Mesas.FirstOrDefaultAsync(c => c.NumeroMesa == comanda.NumeroMesa);

                mesaAtual!.SituacaoMesa = (int)SituacaoMesa.Disponivel;

                comanda.NumeroMesa = mesa.NumeroMesa;

            }

            //verificar se foi informado um novo nome p o cliente.

            if (!string.IsNullOrEmpty(comandaPutDTO.NomeCliente)){

                comanda.NomeCliente = comandaPutDTO.NomeCliente;

            }

            //percorrer os itens da comanda e verificar se eh uma exclusao.
        /**
            foreach (var item in comandaPutDTO.ComandaItems){
                if(item.Excluir){
                    var comandaItem = await _banco.ComandaItems.FirstOrDefaultAsync(c => c.Id == item.Id);
                    if(comandaItem is not null){
                        _banco.ComandaItems.Remove(comandaItem);
                    }
                }
            }
        **/
            var itensExcluir = new List<int>();

            itensExcluir = comandaPutDTO.ComandaItems.Where(c => c.Excluir).Select(c => c.Id).ToList(); 

            if(itensExcluir.Any()){

            var comandaItensExcluir = await _banco.ComandaItems.Where(c => itensExcluir.Contains(c.Id)).ToListAsync();

            _banco.ComandaItems.RemoveRange(comandaItensExcluir);
            }
            
            
            //verificar se eh para adicionar um novo item. 

            var idsAdd = new List<int>();

            idsAdd = comandaPutDTO.ComandaItems.Where(c => c.Excluir == false)
                                                        .Select(c => c.CardapioItemId).ToList();

            if(idsAdd.Any()){
                
                List<ComandaItem> comandaItens = idsAdd.Select(c => 
                                                                new ComandaItem{
                                                                Comanda = comanda,
                                                                CardapioItemId = c
                                                                }).ToList(); 

                _banco.ComandaItems.AddRange(comandaItens);

                foreach(ComandaItem comandaItem in comandaItens){

                    var cardapioItem = await _banco.CardapioItems.FirstOrDefaultAsync(c => c.Id == comandaItem.CardapioItemId);

                    if(cardapioItem!.PossuiPreparo){
                        
                        var pedidoCozinha = new PedidoCozinha{
                            Comanda = comanda,
                            SituacaoId = (int)SituacaoPedidoCozinha.Pendente
                        };

                        await _banco.PedidosCozinha.AddAsync(pedidoCozinha);

                        var pedidoCozinhaItem = new PedidoCozinhaItem{
                            PedidoCozinha = pedidoCozinha,
                            ComandaItem = comandaItem
                        };

                        await _banco.PedidoCozinhaItems.AddAsync(pedidoCozinhaItem);
                    }    
                }
            }

            //fazer a persistencia dos dados.

            await _banco.SaveChangesAsync();

            return NoContent();
        } 

    }

}

//colocar try catch no erro 500 do put

// finalizar o delete 