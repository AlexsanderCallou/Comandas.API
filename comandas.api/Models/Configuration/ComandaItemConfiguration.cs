using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comandas.API.Models.Configuration{

    public class ComandaItemConfiguration : IEntityTypeConfiguration<ComandaItem>{

        public void Configure(EntityTypeBuilder<ComandaItem> builder){

            builder.ToTable("tb_comanda_item");

            builder.Property(c => c.Id)
            .IsRequired()
            .HasColumnName("id");

            builder.Property(c => c.CardapioItemId)
            .IsRequired()
            .HasColumnName("id_cardapio_item");

            builder.Property(c => c.ComandaId)
            .IsRequired()
            .HasColumnName("id_comanda");
        }
    }
}