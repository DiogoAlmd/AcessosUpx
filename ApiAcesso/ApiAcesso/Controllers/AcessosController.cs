using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ApiAcesso.Models;
using ApiAcesso.Classes;
using ApiAcesso.Repositories;

namespace ApiAcesso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcessosController : ControllerBase
    {
        private readonly ILogger<AcessosController> _logger;
        private readonly string _connectionString;


        public AcessosController(ILogger<AcessosController> logger)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfigurationRoot configuration = builder.Build();
            _connectionString = configuration.GetValue<string>("ConnectionStrings:SqlConnection");
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> ObterDados()
        {
            var dados = new List<Acessos>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                await conexao.OpenAsync();

                using (var comando = new SqlCommand("select * from Acesso", conexao))
                {
                    using (var leitor = await comando.ExecuteReaderAsync())
                    {
                        while (await leitor.ReadAsync())
                        {
                            dados.Add(new Acessos
                            {
                                Id = leitor["Id"].ToString(),
                                NFC = leitor["NFC"].ToString(),
                                Arduino = leitor["Arduino"].ToString(),
                                Localizacao = leitor["Localizacao"].ToString(),
                                Data = leitor["Data"].ToString()
                            });
                        }
                    }
                    return Ok(dados);
                }
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AdicionaAcesso([FromBody] Acesso request)
        {
            try
            {
                var repository = new GetLocation();

                var localizacao = await repository.GetGeoInfoAsync(request.IpAddress);

                using (var conexao = new SqlConnection(_connectionString))
                {
                    await conexao.OpenAsync();

                    using (var comando = new SqlCommand($@"INSERT INTO Acesso(NFC, Arduino, Localizacao, DATA)
                                                           values ('{request.NFC}', '{request.Arduino}', '{localizacao}', GETDATE());", conexao)
                                                        )
                    {
                        await comando.ExecuteNonQueryAsync();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao adicionar o Acesso de {request.Arduino}");
                return StatusCode(500);
            }
        }
    }
}
