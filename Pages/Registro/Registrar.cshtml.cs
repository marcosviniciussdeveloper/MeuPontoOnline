
using MeuPontoOnline.Models;
using MeuPontoOnline.Pages.Login;
using MeuPontoOnline.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Supabase;
using Supabase.Postgrest.Exceptions;



namespace MeuPontoOnline.Pages.Registro;

public class IndexModel : PageModel
{
    private readonly Client _supabase;

    private readonly GeoLocalizacaoService _geoService;

    public IndexModel(Client supabase, GeoLocalizacaoService geoLocalizacaoService)
    {
        _supabase = supabase;
        _geoService = geoLocalizacaoService;
    }

    [BindProperty]
    public string? TipoRegistro { get; set; }

    [BindProperty]
    public double Latitude { get; set; }

    [BindProperty]
    public double Longitude { get; set; }

    [BindProperty]
    public string? Matricula { get; set; }

    [BindProperty]
    public int FuncionarioId { get; set; }
    public RegistroPonto? RegistroSalvo { get; set; }
    public string? Mensagem { get; set; }
    public string MensagemErro { get; private set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(TipoRegistro))
        {
            Mensagem = "Selecione um tipo de registro.";
            return Page();
        }

        try
        {
           var matricula = User.FindFirst("Matricula")?.Value;

           var funcionario = User.FindFirst("Funcionario")?.Value;

           if (funcionario == null)
           {
              Mensagem = "Funcionário com esta matrícula não encontrado.";
              return Page();
           }


            var usuarioLogado = await _supabase
                .From<Funcionario>()
                .Where(x => x.Matricula == Matricula)
                .Get();

            if (funcionario.Matricula != Matricula)
            {
                MensagemErro = "Você não tem permissão para bater o ponto de outro funcionário.";
                return Page();
            }



            var hoje = DateTime.UtcNow.Date;
            var amanha = hoje.AddDays(1);

            var registros = await _supabase
                .From<RegistroPonto>()
                .Where(x => x.FuncionarioId == funcionario.Id)
                .Where(x => x.DataHora >= hoje)
                .Where(x => x.DataHora <= amanha)
                .Where(x => x.TipoRegistro == TipoRegistro)
                .Get();

            if (registros.Models.Any())
            {
                Mensagem = $"Já existe um registro de ponto '{TipoRegistro}' para o funcionário {funcionario.Nome} no dia de hoje.";
                return Page();
            }




            var enderecoCompleto = await _geoService.ObterEnderecoAsync(Latitude, Longitude);

            var novoRegistro = new RegistroPonto
            {
                FuncionarioId = funcionario.Id,
                TipoRegistro = TipoRegistro!,
                DataHora = DateTime.UtcNow,
                Observacao = "",
                Latitude = Latitude,
                Longitude = Longitude,
                EnderecoCompleto = enderecoCompleto,
            };

            var resposta = await _supabase
                .From<RegistroPonto>()
                .Insert(novoRegistro);

            RegistroSalvo = resposta.Models.FirstOrDefault();

            Mensagem = $"Registro '{TipoRegistro}' feito às {novoRegistro.DataHora.ToLocalTime():HH:mm:ss} em {enderecoCompleto}.";
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

}
