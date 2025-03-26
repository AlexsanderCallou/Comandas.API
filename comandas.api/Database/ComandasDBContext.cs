using Comandas.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Comandas.API.DataBase{

    public class ComandasDBContext:DbContext{

        public DbSet<Mesa> Mesas{get;set;}
        public DbSet<CardapioItem> CardapioItems{get;set;}

        public ComandasDBContext(DbContextOptions<ComandasDBContext> dbContextOptions):base(dbContextOptions)
        {
            

        }

    }

}