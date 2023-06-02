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
            // Configuração do logger e obtenção da string de conexão do arquivo de configuração
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
                    // Execução da consulta SQL para obter dados da tabela "Acesso"
                    using (var leitor = await comando.ExecuteReaderAsync())
                    {
                        while (await leitor.ReadAsync())
                        {
                            // Criação de um objeto Acessos com os dados obtidos do banco de dados e adição à lista "dados"
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
                    // Retorno dos dados obtidos em formato JSON
                    return Ok(dados);
                }
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AdicionaAcesso([FromBody] Acesso request)
        {
            try
            {
                // Instanciação do repositório GetLocation
                var repository = new GetLocation();

                // Obtenção da informação de localização com base no endereço IP fornecido
                var localizacao = await repository.GetGeoInfoAsync(request.IpAddress);

                using (var conexao = new SqlConnection(_connectionString))
                {
                    await conexao.OpenAsync();

                    using (var comando = new SqlCommand($@"INSERT INTO Acesso(NFC, Arduino, Localizacao, DATA)
                                                           values ('{request.NFC}', '{request.Arduino}', '{localizacao}', GETDATE());", conexao)
                                                        )
                    {
                        // Execução do comando SQL para adicionar um novo acesso à tabela "Acesso"
                        await comando.ExecuteNonQueryAsync();
                    }
                }

                // Retorno de sucesso (status 200)
                return Ok();
            }
            catch (Exception ex)
            {
                // Registro de erro no logger caso ocorra uma exceção durante o processamento
                _logger.LogError(ex, $"Erro ao adicionar o Acesso de {request.Arduino}");
                // Retorno de erro interno do servidor (status 500)
                return StatusCode(500);
            }
        }
    }
}
