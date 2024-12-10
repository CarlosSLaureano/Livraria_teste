namespace Projeto_teste.Models;

public class LivroModel
{
   public int Id { get; set; }
   public string Título { get; set; }
   
   public AutorModel Autor { get; set; }

}
