using Projeto_Emprestimo.Models;

namespace Projeto_Emprestimo.Repositories.Contract
{
    public interface IEmprestimoRepository
    {
        //CRUD
        IEnumerable<Emprestimo> ObterTodosEmprestimos();
        void Cadastrar(Emprestimo emprestimo);
        void Atualizar(Emprestimo emprestimo);
        Emprestimo ObterEmprestimos(int ID);
        void buscaIdEmp(Emprestimo emprestimo);
        void Excluir(int ID);
    }
}
