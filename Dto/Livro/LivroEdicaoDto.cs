using Projeto_teste.Dto.Vinculo;

namespace Projeto_teste.Dto.Livro;

public class LivroEdicaoDto
{
    public int Id { get; set; }
    public string? Título { get; set; }

    public AutorVinculoDto? Autor { get; set; }
}
