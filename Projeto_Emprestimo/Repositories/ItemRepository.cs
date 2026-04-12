using MySql.Data.MySqlClient;
using Projeto_Emprestimo.Models;
using Projeto_Emprestimo.Repositories.Contract;
using System.Data;

namespace Projeto_Emprestimo.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly string _conexaoMySQL;
        public ItemRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }
        public void Cadastrar(Item item)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into itensEmp values (default, @codEmp, @codLivro)", conexao);

                cmd.Parameters.Add("@codEmp", MySqlDbType.VarChar).Value = item.codEmp;
                cmd.Parameters.Add("@codLivro", MySqlDbType.VarChar).Value = item.codLivro;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        public void Atualizar(Item item)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                conexao.Close();
            }
        }
        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                conexao.Close();
            }
        }
        public Item ObterItens(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                conexao.Close();
            }
            return null;

        }
        public IEnumerable<Item> ObterTodosItens()
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                conexao.Close();
            }
            return null;
        }
    }
}