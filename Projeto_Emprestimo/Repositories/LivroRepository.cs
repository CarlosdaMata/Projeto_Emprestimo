using MySql.Data.MySqlClient;
using Projeto_Emprestimo.Models;
using Projeto_Emprestimo.Repositories.Contract;
using System.Data;

namespace Projeto_Emprestimo.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly string _conexaoMySQL;
        public LivroRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySql");
        }


        public void Cadastrar(Livro livro)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into tbLivro values (default, @NomeLivro, @ImagemLivro)", conexao);

                cmd.Parameters.Add("@NomeLivro", MySqlDbType.VarChar).Value = livro.nomeLivro;
                cmd.Parameters.Add("@ImagemLivro", MySqlDbType.VarChar).Value = livro.imagemLivro;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        public Livro ObterLivros(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from tbLivro where codLivro = @Cod", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Livro livro = new Livro();
                dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    livro.codLivro = Convert.ToInt32(dr["codLivro"]);
                    livro.nomeLivro = (String)(dr["nomeLivro"]);
                    livro.imagemLivro = (String)(dr["imagemLivro"]);
                }
                return livro;
            }
        }
        public IEnumerable<Livro> ObterTodosLivros()
        {
            List<Livro> Livrolist = new List<Livro>();
            using(var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbLivro", conexao);
                MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sd.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    Livrolist.Add(
                        new Livro
                        {
                            codLivro = Convert.ToInt32(dr["codLivro"]),
                            nomeLivro = (String)(dr["nomeLivro"]),
                            imagemLivro = (String)(dr["imagemLivro"])
                        });
                }
                return Livrolist;
            }
        }
        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }
        public void Atualizar(Livro livro)
        {
            throw new NotImplementedException();
        }
    }
}
