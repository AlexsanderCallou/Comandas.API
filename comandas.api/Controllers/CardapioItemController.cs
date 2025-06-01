using Comandas.Domain;
using Comandas.Data;
using Comandas.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Comandas.Services.Implementation;
using Comandas.Data.Implementation;
using Comandas.Services.Interface;

namespace Comandas.API.Controllers{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardapioItemController:ControllerBase{


        public readonly ICardapioItemService _cardapioItemService;

        public CardapioItemController(ICardapioItemService cardapioItemService)
        {
            _cardapioItemService = cardapioItemService;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200,"Retorna o item do cardapio",typeof(CardapioItemGetDTO))]
        public async Task<ActionResult<CardapioItemGetDTO>> GetCardapioItem(int Id)
        {
            return Ok (await _cardapioItemService.GetCardapioItem(Id));        
        }

        [HttpGet]
        [SwaggerResponse(200,"Retorna todos os itens do cardapio", typeof(IEnumerable<CardapioItemGetDTO>))]
        public async Task<ActionResult<IEnumerable<CardapioItemGetDTO>>> GetCardapioItens()
        {
            return Ok (await _cardapioItemService.GetCardapioItems());
        }

        [HttpPost]
        [SwaggerResponse(201, "Cadastra um item no cardapio", typeof(CardapioItemPostDTO))]
        public async Task<ActionResult<CardapioItemPostDTO>> PostCardapioItem(CardapioItemPostDTO cardapioItemPostDTO)
        {

            var cardapioItem = await _cardapioItemService.PostCardapioItem(cardapioItemPostDTO);

            return CreatedAtAction("GetCardapioItem", new { id = cardapioItem.Id }, cardapioItem);

        }

        [HttpPut("{id}")]
        [SwaggerResponse(204,"Altera um item do cardapio")]
        [SwaggerResponse(400,"Id do item informado não é o mesmo do corpo")]
        public async Task<ActionResult<CardapioItemPutDTO>> PutCardapioItem(int id, CardapioItemPutDTO cardapioItemPutDTO)
        {

            if(id != cardapioItemPutDTO.Id){
                return BadRequest();
            }

            var cardapioItem = await _cardapioItemService.PutCardapioItem(cardapioItemPutDTO);

            if (cardapioItem == false){
                return NotFound("Item não encontrado.");
            }

            return NoContent();

        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204,"Exclui item do cardapio")]
        public async Task<ActionResult> DeleteCardapioItem(int Id)
        {

            var cardapioItem = await _cardapioItemService.DeleteCardapioItem(Id);

            if (cardapioItem == false){
                return NotFound("Item não encontrado.");
            }

            return NoContent();
        }

    }
}