using MeuPontoOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Supabase;

using System.Security.Cryptography;
using System.Text;

namespace MeuPontoOnline.Pages.CadastroUsuarios
{
    public class CadastroModel : PageModel
    {
        private readonly Client _supabase;

        public CadastroModel(Client supabase)
        {
            _supabase = supabase;
        }

        [BindProperty]
        public Funcionario NovoFuncionario { get; set; } = new();

        [BindProperty]
        public string Senha { get; set; } = string.Empty;

        [BindProperty]
        public string FuncaoNome { get; set; } = string.Empty;

        public List<SelectListItem>
    ListaDeSetores
        { get; set; } = new();

        public string Mensagem { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            await CarregarSetores();
        }

        public async Task<IActionResult>
            OnPostAsync()

        { 





            if (!ModelState.IsValid)
                return Page();

            if (string.IsNullOrWhiteSpace(Senha))
            {
                ModelState.AddModelError("Senha", "A senha é obrigatória.");
                return Page();
            }

            var funcoes = await _supabase
              .From<Funcao>()
               .Where(f => f.Nome == FuncaoNome)
                .Get();


            var funcao = funcoes.Models.FirstOrDefault();

            if (funcao == null)
            {
                funcao = new Funcao { Nome = FuncaoNome };
                var result = await _supabase.From<Funcao>().Insert(funcao);

                funcao = result.Models.First();
            }


            NovoFuncionario.FuncaoId = funcao.Id;
            NovoFuncionario.SenhaHash = GerarHash(Senha);

            await _supabase
            .From<Funcionario>()
            .Insert(NovoFuncionario);



            Mensagem = "Funcionário cadastrado com sucesso!";


            return Page();

        }



        private async Task CarregarSetores()
        {
            var setores = await _supabase.From<Setor>().Get();

            ListaDeSetores = setores.Models.Select(s => new SelectListItem
            {
                Text = s.Nome,
                Value = s.Id.ToString(),


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
