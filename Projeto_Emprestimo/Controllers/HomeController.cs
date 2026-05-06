using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projeto_Emprestimo.CarrinhoCompra;
using Projeto_Emprestimo.Models;
using Projeto_Emprestimo.Repositories;
using Projeto_Emprestimo.Repositories.Contract;

namespace Projeto_Emprestimo.Controllers
{
    public class HomeController : Controller
    {
        private IItemRepository _itemRepository;
        private IEmprestimoRepository _emprestimoRepository;
        private CookieCarrinhoCompra _cookieCarrinhoCompra;
        private ILivroRepository _livroRepository;

        public HomeController(ILivroRepository livroRepository, CookieCarrinhoCompra  cookieCarrinhoCompra,
                                IEmprestimoRepository emprestimoRepository,IItemRepository itemRepository )
        {
            _livroRepository = livroRepository;
            _cookieCarrinhoCompra = cookieCarrinhoCompra;
            _emprestimoRepository = emprestimoRepository;
            _itemRepository = itemRepository;
        }

        public IActionResult Index()
        {
            return View(_livroRepository.ObterTodosLivros());
        }

        public IActionResult AdicionarItem(int id)
        {
            Livro produto = _livroRepository.ObterLivros(id);

            if(produto == null)
            {
                return View("Nao Existe Item");
            }
            else
            {
                var item = new Livro()
                {
                    codLivro = id,
                    quantidade = 1,
                    imagemLivro = produto.imagemLivro,
                    nomeLivro = produto.nomeLivro
                };
                _cookieCarrinhoCompra.Cadastrar(item);
                return RedirectToAction(nameof(CarrinhoCompra));
            }
        }

        public IActionResult Carrinho()
        {
            return View(_cookieCarrinhoCompra.Consultar());
        }

        public IActionResult RemoverItem (int id)
        {
            _cookieCarrinhoCompra.Remover(new Livro() { codLivro=id });
            return RedirectToAction(nameof(Carrinho));
        }

        DateTime data;
        public IActionResult SalvarCarrinho(Emprestimo emprestimo)
        {
            List<Livro> carrinho = new CookieCarrinhoCompra().Consultar();

            Emprestimo mdE = new Emprestimo();
            Item mdI = new Item();

            data = DateTime.Now.ToLocalTime();

            mdE.dataEmp = data.ToString("dd/MM/yyyy");
            mdE.dataDev = data.AddDays(7).ToString();
            mdE.codUsu = 1;
            _emprestimoRepository.Cadastrar(mdE);

            _emprestimoRepository.buscaIdEmp(emprestimo);

            for (int i = 0; i < carrinho.Count; i++)
            {
                mdI.codEmp = Convert.ToInt32(emprestimo.codEmp);
                mdI.codLivro = Convert.ToChar(carrinho[i].codLivro);

                _itemRepository.Cadastrar(mdI);
            }

            _cookieCarrinhoCompra.RemoverTodos();
            return RedirectToAction("confEmp");

        }

        public IActionResult confEmp()
        {
            return View();
        }
       
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
