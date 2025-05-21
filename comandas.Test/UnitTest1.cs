using System.Threading.Tasks;
using Comandas.API.Controllers;
using Comandas.Data;
using Comandas.Shared.DTOs;
using Comandas.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Comandas.Test;

public class UnitTest1
{
    private readonly ComandaController comandaController;
    private readonly ComandasDBContext comandasDBContext;

    public UnitTest1()
    {
        var provedorServico = new ServiceCollection()
                                .AddDbContext<ComandasDBContext>(c => c.UseInMemoryDatabase(Guid.NewGuid().ToString()))
                                .BuildServiceProvider();

        var escopo = provedorServico.CreateScope();

        comandasDBContext = escopo.ServiceProvider.GetRequiredService<ComandasDBContext>();

        comandaController = new ComandaController(comandasDBContext,null);

        carregarbanco();

    }

    private void carregarbanco()
    {
        var mesa = new Mesa{
            Id = 1,
            NumeroMesa = 1,
            SituacaoMesa = 0
        };

        var cardapioItem = new CardapioItem{
            Id = 1,
            Descricao = "Coca Late",
            PossuiPreparo = false,
            Preco = 2,
            Titulo = "Coca Cola"
        };

        var comanda = new Comanda{
            Id = 1,
            NomeCliente = "Alex",
            NumeroMesa = 1,
            SituacaoComanda = 1

        };

        var comandaItem = new ComandaItem{
            Comanda = comanda,
            CardapioItem = cardapioItem
        };

        comandasDBContext.Mesas.Add(mesa);
        comandasDBContext.CardapioItems.Add(cardapioItem);
        comandasDBContext.Comandas.Add(comanda);
        comandasDBContext.ComandaItems.Add(comandaItem);

        comandasDBContext.SaveChanges();

    }

    [Fact]
    public async Task GetComandas_Return_ComandasAbertas()
    {

        //act 
        
        var comandaFilterDTO = new ComandaFilterDTO{
            SituacaoComanda = 1
        };

        var result = await comandaController.GetComandas(comandaFilterDTO);

        //asset

        var result200 = Assert.IsType<OkObjectResult>(result.Result);

        var resultComandas = Assert.IsType<List<ComandaGetDTO>>(result200.Value);

        Assert.NotEmpty(resultComandas);
        Assert.All(resultComandas,c => Assert.Equal(1,c.Id));
        Assert.All(resultComandas,c => Assert.Equal("Alex", c.NomeCliente));
        
        //arange 

    }
}