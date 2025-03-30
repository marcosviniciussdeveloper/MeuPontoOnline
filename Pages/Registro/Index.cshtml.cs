using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MeuPontoOnline.Data;
using MeuPontoOnline.Models;
using Microsoft.EntityFrameworkCore;

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
        [BindProperty]
        public string? CodigoIdentificacao { get; set; }

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

            if (string.IsNullOrEmpty(CodigoIdentificacao))
            {
                Mensagem = "Informe o código de identificação.";
                return Page();
            }

            var funcionario = await _context.Funcionarios.
                FirstOrDefaultAsync(f => f.CodigoIndetificacao == CodigoIdentificacao);

            if (funcionario == null)
            {
                Mensagem = "Funcionário não encontrado.";
                return Page();
            }

            var registro = new RegistroPonto
            {
                FuncionarioId = funcionario.Id,
                TipoRegistro = TipoRegistro,
                DataHora = DateTime.UtcNow,
                Observacao = ""
            };

            _context.RegistroPontos.Add(registro);
            await _context.SaveChangesAsync();

            Mensagem = $"Registro de '{TipoRegistro}' realizado às {registro.DataHora:HH:mm:ss}";
            RegistroSalvo = registro;
            return Page();
        }
    }
}
