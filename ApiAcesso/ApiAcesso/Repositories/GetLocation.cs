using ApiAcesso.Models;
using Newtonsoft.Json;

namespace ApiAcesso.Repositories
{
    public interface ILocationService
    {
        Task<string> GetGeoInfoAsync(string ipAddress);
    }

    public class GetLocation : ILocationService
    {
        public async Task<string> GetGeoInfoAsync(string ipAddress)
        {
            string apiUrl = $"https://ipinfo.io/{ipAddress}/geo";

            using (var client = new HttpClient())
            {
                try
                {
                    // Realiza uma solicitação GET para a API de informações de geolocalização com base no endereço IP fornecido
                    string response = await client.GetStringAsync(apiUrl);

                    // Desserializa a resposta JSON em um objeto GetLocationModel
                    GetLocationModel getlocation = JsonConvert.DeserializeObject<GetLocationModel>(response);

                    // Extrai as informações de cidade, país, estado e localização geográfica do objeto GetLocationModel
                    string cidade = getlocation.city;
                    string pais = getlocation.country;
                    string estado = getlocation.region;
                    string geoloc = getlocation.loc;

                    // Combina as informações em uma única string de localização
                    string localizacao = cidade + " " + estado + " " + pais + " " + " " + geoloc;

                    // Retorna a string de localização obtida
                    return localizacao;
                }
                catch (HttpRequestException ex)
                {
                    // Registro de erro caso ocorra uma exceção durante a requisição HTTP
                    Console.WriteLine($"Ocorreu um erro na requisição: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
