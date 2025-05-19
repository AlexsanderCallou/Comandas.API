using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Comandas.Domain;

namespace Comandas.API.Models.Configuration {

    public class PedidoCozinhaConfiguration : IEntityTypeConfiguration<PedidoCozinha>
    {
        public void Configure(EntityTypeBuilder<PedidoCozinha> builder)
        {
            builder.ToTable("tb_pedido_cozinha");
            
            builder.Property(p => p.Id)
            .IsRequired()
            .HasColumnName("id");

            builder.Property(p => p.ComandaId)
            .IsRequired()
            .HasColumnName("id_comanda");

            builder.Property(p => p.SituacaoId)
            .IsRequired()
            .HasColumnName("ic_situacao");
                       
        }
    }

}