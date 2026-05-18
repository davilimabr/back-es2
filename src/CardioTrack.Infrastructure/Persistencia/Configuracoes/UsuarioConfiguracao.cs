using CardioTrack.Domain.Medicoes;
using CardioTrack.Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardioTrack.Infrastructure.Persistencia.Configuracoes;

/// <summary>
/// Mapeamento da entidade <see cref="Usuario"/>: chaves, tamanhos de coluna,
/// indice unico de e-mail e o relacionamento com as medicoes.
/// </summary>
public class UsuarioConfiguracao : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedNever();

        builder.Property(u => u.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Sobrenome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.Telefone)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(u => u.SenhaHash)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.DataNascimento)
            .IsRequired();

        builder.Property(u => u.Sexo)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(u => u.PaisResidencia)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.CriadoEm)
            .IsRequired();

        builder.Ignore(u => u.NomeCompleto);

        builder.HasMany(u => u.Medicoes)
            .WithOne(m => m.Usuario)
            .HasForeignKey(m => m.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        // Acessa a colecao via campo privado, respeitando o encapsulamento da entidade.
        builder.Metadata
            .FindNavigation(nameof(Usuario.Medicoes))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
