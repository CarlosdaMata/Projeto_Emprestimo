using Projeto_Emprestimo.Models;

namespace ProjetoEmprestimo.Repository.Contract
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> ObterTodasCategorias();

    }
}