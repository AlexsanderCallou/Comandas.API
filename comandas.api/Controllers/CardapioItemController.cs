using System.Collections.Generic;
using Comandas.API.DataBase;
using Comandas.API.DTOs;
using Comandas.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Swashbuckle.AspNetCore.Annotations;

namespace Comandas.API.Controllers{

    [Route("api/[controller]")]
    [ApiController]
    public class CardapioItemController:ControllerBase{


        public readonly ComandasDBContext _banco;

        public CardapioItemController(ComandasDBContext comandasDBContext)
        {
            _banco = comandasDBContext;
        }




        [HttpGet("{id}")]
        [SwaggerResponse(200,"Retorna o item do cardapio",typeof(CardapioItemGetDTO))]
        public ActionResult<CardapioItemGetDTO> GetCardapioItem(int id){
            return Ok (new CardapioItemGetDTO(){Id = 1, Descricao = "Arroz", PossuiPreparo = true, Preco = 11.00M, Titulo = "Arroz"});
        }

        [HttpGet]
        [SwaggerResponse(200,"Retorna todos os itens do cardapio", typeof(IEnumerable<CardapioItemGetDTO>))]
        public ActionResult<IEnumerable<CardapioItemGetDTO>> GetCardapioItens(){
            return Ok (new List<CardapioItemGetDTO>(){
                new CardapioItemGetDTO(){Id = 1, Descricao = "Arroz", PossuiPreparo = true, Preco = 11.00M, Titulo = "Arroz"}
                ,new CardapioItemGetDTO(){Id = 2, Descricao = "Feijao", PossuiPreparo = true, Preco = 21.00M, Titulo = "Feijao"}
            });
        }

        [HttpPost]
        [SwaggerResponse(201,"Cadastra um item no cardapio", typeof(CardapioItemPostDTO))]
        public ActionResult<CardapioItemPostDTO> PostCardapioItem(CardapioItemPostDTO cardapioItemPostDTO){
            
            var Cardapio = new CardapioItemPostDTO();

            return CreatedAtAction("GetCardapioItem",new{id=1},Cardapio);

        }

        [HttpPut("{id}")]
        [SwaggerResponse(204,"Altera um item do cardapio")]
        [SwaggerResponse(400,"Id do item informado não é o mesmo do corpo")]
        public ActionResult<CardapioItemPutDTO> PutCardapioItem(int id, CardapioItemPutDTO cardapioItemPutDTO){

            if(id != cardapioItemPutDTO.Id){
                return BadRequest();
            }

            cardapioItemPutDTO.Descricao = "Arroz Parbolizado";

            return NoContent();

        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204,"Exclui item do cardapio")]
        public ActionResult DeleteCardapioItem(int id){
            return NoContent();
        }


    }
}