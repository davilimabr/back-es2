using CardioTrack.Domain.Medicoes;

namespace CardioTrack.Application.Medicoes.Dtos;

/// <summary>
/// Decomposicao da flag <see cref="Sintoma"/> em booleanos, facilitando o consumo
/// pelo front-end sem que ele precise conhecer a representacao de bits.
/// </summary>
public sealed record SintomasResposta(bool FaltaDeAr, bool DorNoPeito, bool Tontura)
{
    public static SintomasResposta DeFlags(Sintoma sintomas) => new(
        sintomas.HasFlag(Sintoma.FaltaDeAr),
        sintomas.HasFlag(Sintoma.DorNoPeito),
        sintomas.HasFlag(Sintoma.Tontura));

    /// <summary>Combina os booleanos na flag de dominio correspondente.</summary>
    public static Sintoma ParaFlags(bool faltaDeAr, bool dorNoPeito, bool tontura)
    {
        var sintomas = Sintoma.Nenhum;
        if (faltaDeAr)
        {
            sintomas |= Sintoma.FaltaDeAr;
        }

        if (dorNoPeito)
        {
            sintomas |= Sintoma.DorNoPeito;
        }

        if (tontura)
        {
            sintomas |= Sintoma.Tontura;
        }

        return sintomas;
    }
}
