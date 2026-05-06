using System.ComponentModel;

namespace Projeto_Emprestimo.Models
{
    public class Livro
    {
        public int codLivro { get; set; }
        [DisplayName("XYZ")]
        public string nomeLivro { get; set; }
        public string imagemLivro { get; set; }
        public string autorLivro { get; set; }
        public string valorLivro { get; set; }
        public int quantidade { get; set; }
    }
}
