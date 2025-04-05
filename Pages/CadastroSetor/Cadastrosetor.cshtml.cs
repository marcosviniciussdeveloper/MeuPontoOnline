using MeuPontoOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supabase;

namespace MeuPontoOnline.Pages.CadastroSetor
{
    public class CadastroModel : PageModel
    {
        private readonly Client _supabase;

        public CadastroModel(Client supabase)
        {
            _supabase = supabase;
        }

        [BindProperty]
        public Setor NovoSetor { get; set; } = new();

        public string Mensagem { get; set; } = string.Empty;

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var existente = await _supabase.From<Setor>().
                Where(x => x.Nome == NovoSetor.Nome)
                .Get();

                if (existente.Models.Count > 0)
                {
                    Mensagem = $"Já existe um setor com o nome \"{NovoSetor.Nome}\".";
                    return Page();
                }

                var response = await _supabase.From<Setor>().Insert(NovoSetor);

                if (response.Models.Count > 0)
                {
                    Mensagem = "Setor cadastrado com sucesso!";
                }
                else
                {
                    Mensagem = $"Erro ao cadastrar:{response.ResponseMessage?.Content?.ReadAsStringAsync().Result}";
                }
            }
            catch (Exception ex)
            {
                Mensagem = $"Erro inesperado: {ex.Message}";
            }

            return Page();
        }

    }
}
