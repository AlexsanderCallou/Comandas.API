using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Comandas.Domain;

namespace Comandas.API.Models.Configuration {

    public class MesaConfiguration : IEntityTypeConfiguration<Mesa>
    {
        public void Configure(EntityTypeBuilder<Mesa> builder)
        {
            builder.ToTable("tb_mesa");
            
            builder.Property(m => m.Id)
            .IsRequired()
            .HasColumnName("id");

            builder.Property(m => m.NumeroMesa)
            .IsRequired()
            .HasColumnName("nu_mesa");

            builder.Property(m => m.SituacaoMesa)
            .IsRequired()
            .HasColumnName("ic_situacao");
                       
        }
    }

}