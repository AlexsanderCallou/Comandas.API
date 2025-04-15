using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comandas.API.Models.Configuration {

    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("tb_usuario");
            
            builder.Property(i => i.Id)
            .IsRequired()
            .HasColumnName("id");

            builder.Property(i => i.Nome)
            .IsRequired()
            .HasColumnName("no_ususario");

            builder.Property(i => i.Email)
            .IsRequired()
            .HasColumnName("tx_email");

            builder.Property(i => i.Senha)
            .IsRequired()
            .HasColumnName("tx_senha");
                     
        }
    }

}