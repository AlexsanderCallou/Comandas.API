using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comandas.API.Models.Configuration {

    public class ComandaConfiguratio : IEntityTypeConfiguration<Comanda> {


        public void Configure(EntityTypeBuilder<Comanda> builder){


            builder.ToTable("tb_comanda");

            builder.Property(c => c.Id)
            .IsRequired()
            .HasColumnName("id");

            builder.Property(c => c.NomeCliente)
            .HasColumnName("no_cliente");

            builder.Property(c => c.NumeroMesa)
            .HasColumnName("nu_mesa");

            builder.Property(c => c.SituacaoComanda)
            .HasColumnName("ic_situacao");

        }
    }
}