using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comandas.API.Models.Configuration {

    public class MesaConfiguration : IEntityTypeConfiguration<Mesa>
    {
        public void Configure(EntityTypeBuilder<Mesa> builder)
        {
            builder.ToTable("TB_MESA");
            
            builder.Property(m => m.Id)
            .IsRequired()
            .HasColumnName("ID");

            builder.Property(m => m.NumeroMesa)
            .IsRequired()
            .HasColumnName("NUM_MESA");

            builder.Property(m => m.SituacaoMesa)
            .IsRequired()
            .HasColumnName("IC_SITUACAO");
                       
        }
    }

}