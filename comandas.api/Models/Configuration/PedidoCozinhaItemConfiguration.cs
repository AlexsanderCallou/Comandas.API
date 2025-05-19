using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Comandas.Domain;

namespace Comandas.API.Models.Configuration {

    public class PedidoCozinhaItemConfiguration : IEntityTypeConfiguration<PedidoCozinhaItem>
    {
        public void Configure(EntityTypeBuilder<PedidoCozinhaItem> builder)
        {
            builder.ToTable("tb_pedido_cozinha_item");
            
            builder.Property(i => i.Id)
            .IsRequired()
            .HasColumnName("id");

            builder.Property(i => i.PedidoCozinhaId)
            .IsRequired()
            .HasColumnName("id_pedido_cozinha");

            builder.Property(i => i.ComanadaItemId)
            .IsRequired()
            .HasColumnName("id_comanda_item");
                       
        }
    }

}