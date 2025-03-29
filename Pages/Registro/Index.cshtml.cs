using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MeuPontoOnline.Data;
using MeuPontoOnline.Models;

namespace MeuPontoOnline.Pages.Registro
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(AppDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public string? TipoRegistro { get; set; }

        public RegistroPonto? RegistroSalvo { get; set; }
        public string? Mensagem { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(TipoRegistro))
            {
                Mensagem = "Selecione um tipo de registro.";
                return Page();
            }

            // TEMPOR�RIO - substituir pela l�gica de login no futuro
            int funcionarioId = 1;

            var registro = new RegistroPonto
            {
                FuncionarioId = funcionarioId,
                TipoRegistro = TipoRegistro,
                DataHora = DateTime.UtcNow,
                Observacao = ""
            };

            _context.RegistroPontos.Add(registro);
            await _context.SaveChangesAsync();

            Mensagem = $"Registro de '{TipoRegistro}' realizado �s {registro.DataHora:HH:mm:ss}";
            RegistroSalvo = registro;
            return Page();
        }
    }
}
