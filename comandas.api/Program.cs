
using Comandas.API.DataBase;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Obeter a connection string do banco
var connection = builder.Configuration.GetConnectionString("Default");

//Adicionar o contexto do banco de dados a piperline do app
builder.Services.AddDbContextPool<ComandasDBContext>(config => {
    config.UseNpgsql(connection);
    config.EnableSensitiveDataLogging();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


//continuar a aula a partir da aqui, na criação do escopo.
using(var escopo = app.Services.CreateScope()){

    var contexto = escopo.ServiceProvider.GetRequiredService<ComandasDBContext>();
    await contexto.Database.MigrateAsync();
    InicializarDados.Semear(contexto);

}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();




app.Run();
