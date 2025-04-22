using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MeuPontoOnline.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supabase;

namespace MeuPontoOnline.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly Client _supabase;

        public LoginModel(Client supabase)
        {
            _supabase = supabase;
        }

        [BindProperty]
        public string Matricula { get; set; } = string.Empty;

        public string? MensagemErro { get; set; }

        [BindProperty]
        public string? Senha { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _supabase
                .From<Funcionario>()
                .Where(f => f.Matricula == Matricula)
                .Get();

            var funcionario = result.Models.FirstOrDefault();

            if (funcionario == null)
            {
                MensagemErro = "Funcionário não encontrado.";
                return Page();
            }

            static string GerarHash(string senha)
            {
                using var sha256 = SHA256.Create();
                {
                    var bytes = Encoding.UTF8.GetBytes(senha);
                    var hash = sha256.ComputeHash(bytes);
                    return Convert.ToBase64String(hash);
                }
            }

            if (funcionario.SenhaHash != GerarHash(Senha))
            {
                MensagemErro = "Senha ou matrícula inválida.";
                return Page();
            }

            await SignInUserAsync(funcionario);

            
            return RedirectToPage("/Registro/Registrar");
        }

        private async Task SignInUserAsync(Funcionario funcionario)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, funcionario.Nome),
        new Claim("Matricula", funcionario.Matricula),
        new Claim("FuncionarioId", funcionario.Id.ToString())
    };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}



