using GestorTF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestorTF.Data.Mapping
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                  .IsRequired()
                  .HasMaxLength(100);

            builder.Property(c => c.Cnpj)
                  .IsRequired()
                  .HasMaxLength(14);

            builder.HasIndex(c => c.Cnpj)
                  .IsUnique();

            builder.Property(c => c.Email)
                  .HasMaxLength(100);

            builder.Property(c => c.Telefone)
                  .HasMaxLength(20);

            builder.Property(c => c.Ativo)
                  .IsRequired();

            builder.Property<Guid>("UsuarioId")
                  .IsRequired();
        }
    }
}