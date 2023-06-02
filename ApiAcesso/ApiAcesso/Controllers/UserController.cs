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
            // Configuração do logger e obtenção da string de conexão do arquivo de configuração
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
                    // Execução da consulta SQL para obter dados da tabela "Usuario"
                    using (var leitor = await comando.ExecuteReaderAsync())
                    {
                        while (await leitor.ReadAsync())
                        {
                            // Criação de um objeto Usuario com os dados obtidos do banco de dados e adição à lista "dados"
                            dados.Add(new Usuario
                            {
                                Nome = leitor["Nome"].ToString(),
                                NFC = leitor["NFC"].ToString()
                            });
                        }
                    }
                    // Retorno dos dados obtidos em formato JSON
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
                        // Execução do comando SQL para adicionar um novo usuário à tabela "Usuario"
                        await comando.ExecuteNonQueryAsync();
                    }
                }

                // Retorno de sucesso (status 200)
                return Ok();
            }
            catch (Exception ex)
            {
                // Registro de erro no logger caso ocorra uma exceção durante o processamento
                _logger.LogError(ex, $"Erro ao adicionar o Usuario de {request.Nome}");
                // Retorno de erro interno do servidor (status 500)
                return StatusCode(500);
            }
        }
    }
}
