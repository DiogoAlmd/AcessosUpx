using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ApiAcesso.Models;
using ApiAcesso.Classes;
using ApiAcesso.Repositories;

namespace ApiAcesso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly string _connectionString;


        public UserController(ILogger<UserController> logger)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfigurationRoot configuration = builder.Build();
            _connectionString = configuration.GetValue<string>("ConnectionStrings:SqlConnection");
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ObterDados()
        {
            var dados = new List<Usuario>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                await conexao.OpenAsync();

                using (var comando = new SqlCommand("select * from Usuario", conexao))
                {
                    using (var leitor = await comando.ExecuteReaderAsync())
                    {
                        while (await leitor.ReadAsync())
                        {
                            dados.Add(new Usuario
                            {
                                Nome = leitor["Nome"].ToString(),
                                NFC = leitor["NFC"].ToString()
                            });
                        }
                    }
                    return Ok(dados);
                }
            }
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> AdicionaUser([FromBody] NovoUsuario request)
        {
            try
            {
                using (var conexao = new SqlConnection(_connectionString))
                {
                    await conexao.OpenAsync();

                    using (var comando = new SqlCommand($@"insert into Usuario(NOME, NFC) values
                                                           ('{request.Nome}', '{request.NFC}');", conexao)
                                                        )
                    {
                        await comando.ExecuteNonQueryAsync();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao adicionar o Usuario de {request.Nome}");
                return StatusCode(500);
            }
        }
    }
}
