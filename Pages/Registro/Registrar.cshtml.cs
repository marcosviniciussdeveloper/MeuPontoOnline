using MeuPontoOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supabase;
using Supabase.Postgrest.Exceptions;

namespace MeuPontoOnline.Pages.Registro;

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
    public string? NomeFuncionario { get; set; }

    public RegistroPonto? RegistroSalvo { get; set; }
    public string? Mensagem { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(TipoRegistro))
        {
            Mensagem = "Selecione um tipo de registro.";
            return Page();
        }

        if (string.IsNullOrWhiteSpace(NomeFuncionario))
        {
            Mensagem = "Informe o nome do funcionário.";
            return Page();
        }

        try
        {
            var funcionario = await BuscarFuncionarioPorNome(NomeFuncionario.Trim());

            if (funcionario == null)
            {
                Mensagem = "Funcionário não encontrado.";
                return Page();
            }

            var novoRegistro = new RegistroPonto
            {   
                FuncionarioId = funcionario.Id,
                TipoRegistro = TipoRegistro!,
                DataHora = DateTime.UtcNow,
                Observacao = ""
            };

            var resposta = await _supabase
                .From<RegistroPonto>()
                .Insert(novoRegistro);

            RegistroSalvo = resposta.Models.FirstOrDefault();
            Mensagem = $"Registro '{TipoRegistro}' feito às {novoRegistro.DataHora.ToLocalTime():HH:mm:ss}.";
            return Page();
        }
        catch (PostgrestException pgEx)
        {
            Mensagem = $"Erro Supabase: {pgEx.Message}\nConteúdo: {pgEx.Content}";
            return Page();
        }
        catch (Exception ex)
        {
            Mensagem = $"Erro inesperado: {ex.Message}";
            return Page();
        }
    }

    private async Task<Funcionario?> BuscarFuncionarioPorNome(string nome)
    {
        var resultado = await _supabase
            .From<Funcionario>()
            .Where(x => x.Nome == nome)
            .Get();

        return resultado.Models.FirstOrDefault();
    }
}
