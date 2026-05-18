using CardioTrack.Domain.Medicoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardioTrack.Infrastructure.Persistencia.Configuracoes;

/// <summary>
/// Mapeamento da entidade <see cref="Medicao"/>: chaves, precisao do peso,
/// conversao do enum de sintomas e indice por usuario para acelerar relatorios.
/// </summary>
public class MedicaoConfiguracao : IEntityTypeConfiguration<Medicao>
{
    public void Configure(EntityTypeBuilder<Medicao> builder)
    {
        builder.ToTable("medicoes");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedNever();

        builder.Property(m => m.UsuarioId)
            .IsRequired();

        builder.Property(m => m.PressaoSistolica)
            .IsRequired();

        builder.Property(m => m.PressaoDiastolica)
            .IsRequired();

        builder.Property(m => m.FrequenciaCardiaca)
            .IsRequired();

        builder.Property(m => m.OxigenacaoSangue)
            .IsRequired();

        builder.Property(m => m.PesoCorporal)
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(m => m.Sintomas)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(m => m.RegistradaEm)
            .IsRequired();

        builder.Property(m => m.CriadaEm)
            .IsRequired();

        builder.Ignore(m => m.PossuiSintomas);

        // Relatorios consultam por usuario e periodo; o indice cobre os dois.
        builder.HasIndex(m => new { m.UsuarioId, m.RegistradaEm });
    }
}
