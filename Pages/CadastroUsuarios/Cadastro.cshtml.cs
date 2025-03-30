using MeuPontoOnline.Data;
using MeuPontoOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;
using System.Text;

namespace MeuPontoOnline.Pages.CadastroUsuarios
{
    public class CadastroModel : PageModel
    {
        private readonly AppDbContext _context;

        public CadastroModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Funcionario NovoFuncionario { get; set; } = new();

        [BindProperty]
        public string Senha { get; set; } = string.Empty;

        [BindProperty]
        public string FuncaoNome { get; set; } = string.Empty;

        public List<SelectListItem>
    ListaDeSetores { get; set; } = new();

    public string Mensagem { get; set; } = string.Empty;

    public void OnGet()
    {
    CarregarSetores();
    }

    public async Task<IActionResult>
        OnPostAsync()
        {
        CarregarSetores();

        if (!ModelState.IsValid)
        return Page();

        if (string.IsNullOrWhiteSpace(Senha))
        {
        ModelState.AddModelError("Senha", "A senha é obrigatória.");
        return Page();
        }

        var funcao = _context.funcoes.FirstOrDefault(f => f.Nome == FuncaoNome);
        if (funcao == null)
        {
        funcao = new Funcao { Nome = FuncaoNome };
        _context.funcoes.Add(funcao);
        await _context.SaveChangesAsync();
        }

        NovoFuncionario.FuncaoId = funcao.Id;
        NovoFuncionario.SenhaHash = GerarHash(Senha);

        _context.Funcionarios.Add(NovoFuncionario);
        await _context.SaveChangesAsync();

        Mensagem = "Funcionário cadastrado com sucesso!";
        return Page();
        }

        private void CarregarSetores()
        {
        ListaDeSetores = _context.Setores
        .Select(s => new SelectListItem
        {
        Value = s.Id.ToString(),
        Text = s.Nome
        }).ToList();
        }

        private string GerarHash(string senha)
        {
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        return Convert.ToBase64String(hashBytes);
        }
        }
        }
