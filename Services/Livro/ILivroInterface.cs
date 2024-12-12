using Projeto_teste.Dto.Livro;
using Projeto_teste.Models;

namespace Projeto_teste.Services.Livro;

public interface ILivroInterface
{
    Task<ResponseModel<List<LivroModel>>> ListarLivros();
    Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro);
    Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor);
    Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto);

    Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto);

    Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro);
}
