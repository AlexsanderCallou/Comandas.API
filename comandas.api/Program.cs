
using System.Text;
using Comandas.Data.Implementation;
using Comandas.Data.Interface;
using Comandas.Services.Implementation;
using Comandas.Services.Interfaces;
using Comandas.Data;
using Comandas.API.DataBase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

//conexao com o redis
var connRedis = ConnectionMultiplexer.Connect("redis:6379");

builder.Services.AddSingleton<IConnectionMultiplexer>(connRedis);

//Obeter a connection string do banco
var connection = builder.Configuration.GetConnectionString("Docker");

//Adicionar o contexto do banco de dados a piperline do app
builder.Services.AddDbContextPool<ComandasDBContext>(config => {
    config.UseNpgsql(connection);
    config.EnableSensitiveDataLogging();
});

builder.Services.AddScoped<IUsuarioService,UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository,UsuarioRepository>();


builder.Services.AddAuthentication(c => {
    c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(c => {
        c.TokenValidationParameters = new TokenValidationParameters{
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("3e8acfc238f45a314fd4b2bde272678ad30bd1774743a11dbc5c53ac71ca494b"))
        };
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
        Description =   "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
                        "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
                        "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
});



//builder.Services.AddScoped<ILogger<ComandaController>>();


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
