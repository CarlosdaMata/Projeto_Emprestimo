using Newtonsoft.Json;
using Projeto_Emprestimo.Models;

namespace Projeto_Emprestimo.CarrinhoCompra
{
    public class CookieCarrinhoCompra
    {
        private string Key = "Carrinho.Compras";
        private Cookie.manipCookie _cookie;


        public CookieCarrinhoCompra(Cookie.manipCookie cookie)
        {
            _cookie = cookie;
        }

        public CookieCarrinhoCompra()
        {
        }

        public void Salvar(List<Models.Livro> Lista)
        {
            string Valor = JsonConvert.SerializeObject(Lista);
            _cookie.Cadastrar(Key, Valor);
        }

        public List<Models.Livro> Consultar()
        {
            if (_cookie.Existe(Key))
            {
                string valor = _cookie.Consultar(Key);
                return JsonConvert.DeserializeObject<List<Livro>>(valor);
            }
            else
            {
                return new List<Livro>();
            }
        }

        public void Cadastrar(Livro item)
        {
            List<Livro> Lista;
            if (_cookie.Existe(Key))
            {
                Lista = Consultar();
                var ItemLocalizado = Lista.SingleOrDefault(a => a.codLivro == item.codLivro);

                if (ItemLocalizado == null)
                {
                    Lista.Add(item);
                }
                else
                {
                    ItemLocalizado.quantidade = ItemLocalizado.quantidade + 1;
                }
            }
            else
            {
                Lista = new List<Livro>();
                Lista.Add(item);
            }
            Salvar(Lista);
        }
        public void Atualizar(Livro item)
        {
            var Lista = Consultar();
            var ItemLocalizado = Lista.SingleOrDefault(a => a.codLivro == item.codLivro);

            if(ItemLocalizado != null)
            {
                ItemLocalizado.quantidade = item.quantidade + 1;
                Salvar(Lista);
            }
        }

        public void Remover(Livro item)
        {
            var Lista = Consultar();
            var ItemLocalizado = Lista.SingleOrDefault(a => a.codLivro == item.codLivro);

            if(ItemLocalizado != null)
            {
                Lista.Remove(ItemLocalizado);
                Salvar(Lista);
            }
        }
        public bool Existe(string Key)
        {
            if(_cookie.Existe(Key))
            {
                return false;
            }
            return true;
        }
        public void RemoverTodos()
        {
            _cookie.Remover(Key);
        }
    }
}
            