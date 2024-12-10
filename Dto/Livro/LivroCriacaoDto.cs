using Projeto_teste.Dto.Vinculo;
using System.Runtime.CompilerServices;

namespace Projeto_teste.Dto.Livro;

public class LivroCriacaoDto
{
    public string? Título { get; set; }
    public AutorVinculoDto? Autor {  get; set; }
}
