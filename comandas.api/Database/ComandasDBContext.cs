using Comandas.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Comandas.API.DataBase{

    public class ComandasDBContext:DbContext{

        public DbSet<Mesa> mesas{get;set;}

        public ComandasDBContext(DbContextOptions<ComandasDBContext> dbContextOptions):base(dbContextOptions)
        {
            

        }

    }

}