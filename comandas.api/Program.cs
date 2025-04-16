
using Comandas.API.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
builder.Services.AddSwaggerGen(i => {
    i.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()  
        }
    });
    i.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme{
        Description = "",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
});



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
app.UseAuthorization();
app.MapControllers();




app.Run();
