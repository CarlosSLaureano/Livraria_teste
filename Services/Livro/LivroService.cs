using Microsoft.EntityFrameworkCore;
using Projeto_teste.Data;
using Projeto_teste.Dto.Livro;
using Projeto_teste.Logs;
using Projeto_teste.Models;

namespace Projeto_teste.Services.Livro;

public class LivroService : ILivroInterface
{
    private readonly AppDbContext _context;

    public LivroService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
    {
        ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();

        try
        {

            var livro = await _context.Livros.Include(a => a.Autor).FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

            if (livro == null)
            {
                resposta.Mensagem = "Nenhum resgistro localizado";
                return resposta;
            }

            resposta.Dados = livro;
            resposta.Mensagem = "Livro Localizado com sucesso";
            return resposta;

        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
    {
        ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

        try
        {
            var livro = await _context.Livros.Include(a => a.Autor)
                .Where(livroBanco => livroBanco.Autor.Id == idAutor)
                .ToListAsync();

            if (livro == null)
            {
                resposta.Mensagem = "Nenhum registro localizado!";
                return resposta;
            }

            resposta.Dados = livro;
            resposta.Mensagem = "Livros localizados com sucesso!";
            return resposta;


        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
    {
        ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

        try
        {
            var autor = await _context.Autores
                .FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroCriacaoDto.Autor.Id);
            if (autor == null)
            {
                resposta.Mensagem = "Nenhum resgistro de autor localizado";
                return resposta;
            }

            var livro = new LivroModel()
            {
                Título = livroCriacaoDto.Título,
                Autor = autor
            };

            _context.Add(livro);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();

            resposta.Mensagem = "Livro adicionado com sucesso!";
            return resposta;



        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
    {
        ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

        try
        {

            var livro = await _context.Livros
                .Include(a => a.Autor)
                .FirstOrDefaultAsync(livroBanco => livroBanco.Id == livroEdicaoDto.Id);
            var autor = await _context.Autores
                .FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroEdicaoDto.Autor.Id);

            if (livro == null)
            {
                resposta.Mensagem = "Nenhum registro de livro localizado!";
                return resposta;
            }

            if (autor == null)
            {
                resposta.Mensagem = "Nenhum registro de autor localizado!";
                return resposta;
            }

            livro.Título = livroEdicaoDto.Título;
            livro.Autor = autor;

            _context.Update(livro);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Livros.ToListAsync();
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro)
    {
        ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

        try
        {
            var livro = await _context.Livros.Include(a => a.Autor)
                .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);
            if (livro == null)
            {
                resposta.Mensagem = "Nenhum livro localizado!";
                return resposta;
            }

            _context.Remove(livro);
            await _context.SaveChangesAsync();

            resposta.Dados = await _context.Livros.ToListAsync();
            resposta.Mensagem = "Livro Removido com sucesso!";

            return resposta;


        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
    {
        ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
        try
        {


            var livros = await _context.Livros.Include(a => a.Autor).ToListAsync(); // await é usado para esperar todos os aurores serem carregados

            resposta.Dados = livros;
            resposta.Mensagem = "Todos os livros foram coletados";

            Log.LogToFile("Listar Livros - Sucesso", "Livros Listados com Sucesso");

            return resposta;


        }
        catch (Exception ex)
        {
            resposta.Mensagem = ex.Message;
            resposta.Status = false;

            Log.LogToFile("Listar Livros - Erro", ex.Message);
            return resposta;
        }
    }
}

