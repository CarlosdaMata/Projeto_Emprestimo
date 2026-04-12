using Projeto_Emprestimo.Models;

namespace Projeto_Emprestimo.Repositories.Contract
{
    public interface ILivroRepository
    {
        //CRUD
        IEnumerable<Livro> ObterTodosLivros();
        void Cadastrar(Livro livro);
        void Atualizar(Livro livro);
        Livro ObterLivros(int id);
        void Excluir(int id);
    }
}
