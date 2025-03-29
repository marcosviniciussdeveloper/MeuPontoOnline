using MeuPontoOnline.Data;
using MeuPontoOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;
using System.Text;

namespace MeuPontoOnline.Pages.CadastroUsuarios
{
    public class CadastroModel(AppDbContext context) : PageModel
    {
        private readonly AppDbContext _context = context;

        [BindProperty]
        public required Funcionario NovoFuncionario { get; set; }
        public string? Senha { get; set; }

        public string Funcaonome { get; set; } = String.Empty;
        public string? Mensagem { get; set; }

        public List<SelectListItem> ListaDeSetores { get; set; } = [];

        public void OnGet()
        {
            ListaDeSetores = _context.Setores
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome
                }).ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            
           var funcao =  _context.funcoes.FirstOrDefault(f=> f.Nome == Funcaonome);


            if(funcao == null)
            {

                funcao = new Funcao { Nome = Funcaonome };
                _context.funcoes.Add(funcao);
                await _context.SaveChangesAsync();



            }

            NovoFuncionario.FuncaoId = funcao.Id;

            if (!ModelState.IsValid)
            {
                OnGet(); 
                return Page();
            }

            if (string.IsNullOrEmpty(Senha))
            {
                ModelState.AddModelError("Senha", "A senha é obrigatória.");
                OnGet(); 
                return Page();
            }

            NovoFuncionario.SenhaHash = GerarHash(Senha);
            _context.Funcionarios.Add(NovoFuncionario);
            await _context.SaveChangesAsync();

            Mensagem = "Funcionário cadastrado com sucesso!";

            OnGet(); 
            return Page();
        }

        private string GerarHash(string senha)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return Convert.ToBase64String(hashBytes);
        }
    }
}
