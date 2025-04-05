using MeuPontoOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supabase;

namespace MeuPontoOnline.Pages.Registro
{
    public class IndexModel : PageModel
    {

        private readonly Client _supabase;

        public IndexModel(Client supabase)
        {

            _supabase = supabase;
        }


        [BindProperty]
        public string? TipoRegistro { get; set; }
        [BindProperty]
        public string? CodigoIdentificacao { get; set; }

        public RegistroPonto? RegistroSalvo { get; set; }
        public string? Mensagem { get; set; }

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

          
            var resultado = await _supabase
                .From<Funcionario>()
                .Where(f => f.CodigoIndetificacao == CodigoIdentificacao)
                .Get();

            var funcionario = resultado.Models.FirstOrDefault();

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

            await _supabase
                .From<RegistroPonto>()
                .Insert(registro);

            Mensagem = $"Registro de '{TipoRegistro}' realizado às {registro.DataHora.ToLocalTime():HH:mm:ss}";
            RegistroSalvo = registro;
            return Page();
        }

    }
}


