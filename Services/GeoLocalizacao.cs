using System.Globalization;
using System.Net.Http.Json;

namespace MeuPontoOnline.Services;

public class GeoLocalizacaoService
{
    private readonly HttpClient _httpClient;

    public GeoLocalizacaoService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("MeuPontoOnlineApp/1.0 (contato@seuemail.com)");
    }

    public async Task<string> ObterEnderecoAsync(double latitude, double longitude)
    {

        double latCorrigida = (Math.Abs(latitude) > 90) ? latitude / 1_000_000.0 : latitude;
        double lonCorrigida = (Math.Abs(longitude) > 180) ? longitude / 1_000_000.0 : longitude;

        var API_KEY = "AIzaSyBN3kEhCiEgAinXlXXr3q8uPD5xhAV1SW4";
        var url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latCorrigida.ToString(CultureInfo.InvariantCulture)},{lonCorrigida.ToString(CultureInfo.InvariantCulture)}&key={API_KEY}";


        var response = await _httpClient.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return $"Erro Google: {response.StatusCode} - {json}";
        }

        try
        {
            var result = await response.Content.ReadFromJsonAsync<GoogleGeoCodeResponse>();

            if (result?.results != null && result.results.Any())
            {
                return result.results.First().formatted_address;
            }

            return "Endereço não encontrado.";
        }
        catch (Exception ex)
        {
            return $"Erro ao processar resposta do Google: {ex.Message}";
        }
    }

    private class GoogleGeoCodeResponse
    {
        public List<Result> results { get; set; }

        public class Result
        {
            public string formatted_address { get; set; }
        }
    }
}
