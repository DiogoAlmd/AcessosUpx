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
                    string response = await client.GetStringAsync(apiUrl);
                    GetLocationModel getlocation = JsonConvert.DeserializeObject<GetLocationModel>(response);

                    string cidade = getlocation.city;
                    string pais = getlocation.country;
                    string estado = getlocation.region;
                    string geoloc = getlocation.loc;

                    string localizacao = cidade + " " + estado + " " + pais + " " + " " + geoloc;

                    return localizacao;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Ocorreu um erro na requisição: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
