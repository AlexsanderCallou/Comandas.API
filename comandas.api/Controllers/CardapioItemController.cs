using System.Collections.Generic;
using System.Threading.Tasks;
using Comandas.API.DataBase;
using Comandas.API.DTOs;
using Comandas.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Swashbuckle.AspNetCore.Annotations;

namespace Comandas.API.Controllers{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardapioItemController:ControllerBase{


        public readonly ComandasDBContext _banco;

        public CardapioItemController(ComandasDBContext comandasDBContext)
        {
            _banco = comandasDBContext;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200,"Retorna o item do cardapio",typeof(CardapioItemGetDTO))]
        public async Task<ActionResult<CardapioItemGetDTO>> GetCardapioItem(int id){

            var cardapioItem = await _banco.CardapioItems.AsNoTracking().FirstAsync(c => c.Id == id);

            return Ok (new CardapioItemGetDTO(){Id = cardapioItem.Id, 
                                                Descricao = cardapioItem.Descricao, 
                                                PossuiPreparo = cardapioItem.PossuiPreparo, 
                                                Preco = cardapioItem.Preco, 
                                                Titulo = cardapioItem.Titulo});
        
        }

        [HttpGet]
        [SwaggerResponse(200,"Retorna todos os itens do cardapio", typeof(IEnumerable<CardapioItemGetDTO>))]
        public async Task<ActionResult<IEnumerable<CardapioItemGetDTO>>> GetCardapioItens(){

            var cardapioItem = await _banco.CardapioItems.AsNoTracking().ToListAsync();

            return Ok (cardapioItem.Select(c => new CardapioItemGetDTO{
                                            Id = c.Id,
                                            Descricao = c.Descricao,
                                            PossuiPreparo = c.PossuiPreparo,
                                            Preco = c.Preco,
                                            Titulo = c.Titulo})
            );
        }

        [HttpPost]
        [SwaggerResponse(201,"Cadastra um item no cardapio", typeof(CardapioItemPostDTO))]
        public async Task<ActionResult<CardapioItemPostDTO>> PostCardapioItem(CardapioItemPostDTO cardapioItemPostDTO){
            
            var cardapioItem = new CardapioItem(){
                                    Descricao = cardapioItemPostDTO.Descricao,
                                    PossuiPreparo = cardapioItemPostDTO.PossuiPreparo,
                                    Preco = cardapioItemPostDTO.Preco,
                                    Titulo = cardapioItemPostDTO.Titulo
            };

            _banco.CardapioItems.Add(cardapioItem);

            await _banco.SaveChangesAsync();

            return CreatedAtAction("GetCardapioItem",new{id=cardapioItem.Id},cardapioItem);

        }

        [HttpPut("{id}")]
        [SwaggerResponse(204,"Altera um item do cardapio")]
        [SwaggerResponse(400,"Id do item informado não é o mesmo do corpo")]
        public async Task<ActionResult<CardapioItemPutDTO>> PutCardapioItem(int id, CardapioItemPutDTO cardapioItemPutDTO){

            if(id != cardapioItemPutDTO.Id){
                return BadRequest();
            }

            var cardapioItem = await _banco.CardapioItems.FirstOrDefaultAsync(c => c.Id == id);

            if (cardapioItem is null){
                return NotFound("Item não encontrado.");
            }

            cardapioItem.Descricao = cardapioItemPutDTO.Descricao;
            cardapioItem.PossuiPreparo = cardapioItemPutDTO.PossuiPreparo;
            cardapioItem.Preco = cardapioItemPutDTO.Preco;
            cardapioItem.Titulo = cardapioItemPutDTO.Titulo;

            await _banco.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204,"Exclui item do cardapio")]
        public async Task<ActionResult> DeleteCardapioItem(int id){

            var cardapioItem = await _banco.CardapioItems.FirstOrDefaultAsync(c => c.Id == id);

            if (cardapioItem is null){
                return NotFound("Item não encontrado.");
            }

            _banco.CardapioItems.Remove(cardapioItem);

            await _banco.SaveChangesAsync();
            return NoContent();
        }


    }
}