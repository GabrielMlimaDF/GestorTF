using GestorTF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestorTF.Data.Mapping
{
    public class ChamadoMap : IEntityTypeConfiguration<Chamado>
    {
        public void Configure(EntityTypeBuilder<Chamado> builder)
        {
            builder.ToTable("Chamados");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Titulo)
                  .IsRequired()
                  .HasMaxLength(200);

            builder.Property(c => c.Descricao)
                  .IsRequired();

            builder.Property(c => c.DataAbertura)
              .IsRequired();

            builder.Property(c => c.Aberto)
                  .IsRequired();

            builder.Property<Guid>("UsuarioId")
                  .IsRequired();

            builder.HasOne<Cliente>()
                  .WithMany()
                  .HasForeignKey(c => c.ClienteId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}