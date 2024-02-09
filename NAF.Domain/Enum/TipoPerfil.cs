namespace NAF.Domain.Enum
{
    [Flags]
    public enum TipoPerfil
    {
        Nenhum,
        Comunidade,
        Aluno = 1 << 1,
        Professor = 1 << 2
    }
}
