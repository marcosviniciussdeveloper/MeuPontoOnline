using MeuPontoOnline.Data;
using MeuPontoOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeuPontoOnline.Pages.CadastroSetor
{
    public class CadastroModel : PageModel
    {
        private readonly AppDbContext _context;

        public CadastroModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Setor NovoSetor { get; set; } = new();

        public string Mensagem { get; set; } = string.Empty;

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Setores.Add(NovoSetor);
            await _context.SaveChangesAsync();

            Mensagem = "Setor cadastrado com sucesso!";
            return Page();
        }
    }
}
