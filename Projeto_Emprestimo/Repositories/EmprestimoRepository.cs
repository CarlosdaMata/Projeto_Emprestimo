using MySql.Data.MySqlClient;
using Projeto_Emprestimo.Models;
using Projeto_Emprestimo.Repositories.Contract;
using System.Data;

namespace Projeto_Emprestimo.Repositories
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private readonly string _conexaoMySQL;
        public EmprestimoRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }
        public void Atualizar(Emprestimo emprestimo)
        {

        }
        public void Cadastrar(Emprestimo emprestimo)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into tbEmprestimo values(default, @dataEmp, @dataDev, @codUsu)", conexao);

                cmd.Parameters.Add("@dataEmp", MySqlDbType.VarChar).Value = emprestimo.dataEmp;
                cmd.Parameters.Add("@dataDev", MySqlDbType.VarChar).Value = emprestimo.dataDev;
                cmd.Parameters.Add("@codUsu", MySqlDbType.VarChar).Value = emprestimo.codUsu;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        public void Excluir(int id)
        {

        }
        public void buscaIdEmp(Emprestimo emprestimo)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlDataReader dr;

                MySqlCommand cmd = new MySqlCommand("SELECT codEmp FROM tbEmprestimo ORDER BY codEmp DESC limit 1", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    emprestimo.codEmp = Convert.ToInt32(dr["codEmp"]);
                }
                conexao.Close();
            }

        }
        public IEnumerable<Emprestimo> ObterTodosEmprestimos()
        {
            return null;
        }
        public Emprestimo ObterEmprestimos(int id)
        {
            return null;
        }
    }
}
