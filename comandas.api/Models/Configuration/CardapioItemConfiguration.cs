using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Comandas.Domain;

namespace Comandas.API.Models.Configuration {

    public class CardapioItemConfigaration : IEntityTypeConfiguration<CardapioItem>{

        public void Configure(EntityTypeBuilder<CardapioItem> builder){
            
            builder.ToTable("tb_cardapio_item");

            builder.Property(c => c.Id)
            .IsRequired()
            .HasColumnName("id");

            builder.Property(c => c.Titulo)
            .HasColumnName("no_item");

            builder.Property(c => c.Descricao)
            .HasColumnName("tx_descricao");

            builder.Property(c => c.Preco)
            .HasColumnName("vl_preco");

            builder.Property(c => c.PossuiPreparo)
            .HasColumnName("ic_preparo");
        }
    }
}