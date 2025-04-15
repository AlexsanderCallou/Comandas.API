using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Authentication;
using Comandas.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Comandas.API.DataBase{

    public class ComandasDBContext:DbContext{

        public DbSet<Usuario> Usuarios {get;set;}
        public DbSet<Mesa> Mesas{get;set;}     
        public DbSet<CardapioItem> CardapioItems{get;set;}
        public DbSet<Comanda> Comandas {get;set;}
        public DbSet<ComandaItem> ComandaItems {get;set;}
        public DbSet<PedidoCozinha> PedidosCozinha {get;set;}
        public DbSet<PedidoCozinhaItem> PedidoCozinhaItems{get;set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("dbo"); //ver com o professor se essa é a forma correta.

            modelBuilder.Entity<Comanda>() 
            .HasMany(c => c.ComandaItems)
            .WithOne(ci => ci.Comanda)
            .HasForeignKey(ci => ci.ComandaId);

            modelBuilder.Entity<ComandaItem>()
            .HasOne(ci => ci.Comanda)
            .WithMany(ci => ci.ComandaItems)
            .HasForeignKey(c => c.ComandaId);

            modelBuilder.Entity<ComandaItem>()
            .HasOne(ci => ci.CardapioItem)
            .WithMany()
            .HasForeignKey(ci => ci.CardapioItemId);

            modelBuilder.Entity<PedidoCozinha>()
            .HasMany(p => p.PedidoCozinhaItems)
            .WithOne(pi => pi.PedidoCozinha)
            .HasForeignKey(pi => pi.PedidoCozinhaId);

            modelBuilder.Entity<PedidoCozinhaItem>()
            .HasOne(pi => pi.PedidoCozinha)
            .WithMany(pi => pi.PedidoCozinhaItems)
            .HasForeignKey(pi => pi.PedidoCozinhaId);

            modelBuilder.Entity<PedidoCozinhaItem>()
            .HasOne (ci => ci.ComandaItem)
            .WithMany()
            .HasForeignKey (ci => ci.ComanadaItemId);

            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }


        public ComandasDBContext(DbContextOptions<ComandasDBContext> dbContextOptions):base(dbContextOptions)
        {
            

        }

    }

}