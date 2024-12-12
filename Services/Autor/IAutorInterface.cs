using Projeto_teste.Dto.Autor;
using Projeto_teste.Models;

namespace Projeto_teste.Services.Autor;

public interface IAutorInterface
{
    Task<ResponseModel<List<AutorModel>>> ListarAutores();
    Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor);
    Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro);
    Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCriacaoDto autorCriacaoDto);

    Task<ResponseModel<List<AutorModel>>> EditarAutor(AutorEdicaoDto autorEdicaoDto);

    Task<ResponseModel<List<AutorModel>>> ExcluirAutor(int idAutor);

}
