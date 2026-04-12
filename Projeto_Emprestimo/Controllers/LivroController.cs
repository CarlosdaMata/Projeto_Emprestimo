using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projeto_Emprestimo.GerenciaArquivos;
using Projeto_Emprestimo.Models;
using Projeto_Emprestimo.Repositories.Contract;

public class LivroController : Controller
{
    private ILivroRepository _livroRepository;

    public LivroController(ILivroRepository livroRepository)
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
}
