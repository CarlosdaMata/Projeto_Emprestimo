using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projeto_Emprestimo.GerenciaArquivos;
using Projeto_Emprestimo.Models;
using Projeto_Emprestimo.Repositories.Contract;
using System.Diagnostics;

namespace Projeto_Emprestimo.Controllers
{
    public class LivrosController : Controller
    {
        private ILivroRepository _livroRepository;

        public LivrosController(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Livro livro, IFormFile file)
        {
            var Caminho = GerenciadorArquivo.CadastrarImagemProduto(file);

            livro.imagemLivro = Caminho;

            _livroRepository.Cadastrar(livro);

            ViewBag.msg = "Cadastro realizado";
            return View();
        }


        public IActionResult CadLivro()
        {
            var listCategorias = _livroRepository.ObterTodosLivros();
            ViewBag.Categorias = new SelectList(listCategorias, "codLivro", "descricao");
            return View();
        }
        [HttpPost]
        public IActionResult CadLivro(Livro livro, IFormFile file)
        {
            var listCategorias = _livroRepository.ObterTodosLivros();
            ViewBag.Categorias = new SelectList(listCategorias, "codLivro", "descricao");

            var Caminho = GerenciadorArquivo.CadastrarImagemProduto(file);

            livro.imagemLivro = Caminho;

            _livroRepository.Cadastrar(livro);

            ViewBag.msg = "Cadastro realizado";
            return View();
        }
    }
}
