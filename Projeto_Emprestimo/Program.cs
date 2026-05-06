using Projeto_Emprestimo.GerenciaArquivos;
using Projeto_Emprestimo.Repositories;
using Projeto_Emprestimo.Repositories.Contract;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

// Adicionar um serviço para Injetar os metodos no Controller
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();

builder.Services.Configure<CookiePolicyOptions>(Options =>
{
    Options.CheckConsentNeeded = context => true;
    Options.MinimumSameSitePolicy = SameSiteMode.None;
});
// Corrigir problema com TEMPDATA para aumentar o tempo de duração
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Set a short timeout for easy testing.
    options.IdleTimeout = TimeSpan.FromSeconds(900);
    options.Cookie.HttpOnly = true;

    // Deixar informado para o navegador que a sessão é essencial
    options.Cookie.IsEssential = true;
});

builder.Services.AddMvc().AddSessionStateTempDataProvider();

builder.Services.AddMemoryCache(); // Guardar os dados na memória

// Add Gerenciador Arquivo como serviços
builder.Services.AddScoped<GerenciadorArquivo>();
builder.Services.AddScoped<Projeto_Emprestimo.Cookie.manipCookie>();
builder.Services.AddScoped<Projeto_Emprestimo.CarrinhoCompra.CookieCarrinhoCompra>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
