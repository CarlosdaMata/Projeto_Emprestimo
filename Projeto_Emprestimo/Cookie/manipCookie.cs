namespace Projeto_Emprestimo.Cookie
{
    public class manipCookie
    {
        private readonly IHttpContextAccessor _context;
        private readonly IConfiguration _configuration;

        public manipCookie(IHttpContextAccessor context, IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private HttpContext GetHttpContext()
        {
            var ctx = _context.HttpContext;
            if (ctx == null)
            {
                throw new InvalidOperationException("HttpContext is not available. This operation requires an active HTTP request.");
            }
            return ctx;
        }

        // Cadastrar Cookie
        public void Cadastrar(string Key, string Valor)
        {
            var httpContext = GetHttpContext();
            CookieOptions Options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                IsEssential = true
            };

            httpContext.Response.Cookies.Append(Key, Valor, Options);
        }

        // Deleta Cookie
        public void Remover(string Key)
        {
            var httpContext = GetHttpContext();
            httpContext.Response.Cookies.Delete(Key);
        }

        //Consulta Cookie
        public string? Consultar(string Key, bool Cript = true)
        {
            var httpContext = GetHttpContext();
            return httpContext.Request.Cookies[Key];
        }

        public bool Existe(string Key)
        {
            var httpContext = GetHttpContext();
            return httpContext.Request.Cookies[Key] != null;
        }

        public void Atualizar(string Key, string Valor)
        {
            if (Existe(Key))
            {
                Remover(Key);
            }
            Cadastrar(Key, Valor);
        }
    }
}
