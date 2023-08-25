using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.filme.manha.Domains;
using webapi.filme.manha.Interfaces;
using webapi.filme.manha.Repositories;

namespace webapi.filme.manha.Controllers
{
    //Define que a rota de uma requisição será no seguinte formato
    //dominio/api/nomeController
    //ex: http://localhost:/api/genero
    [Route("api/[controller]")]

    //Define que é um controlador de API
    [ApiController]

    //Define que o tipo de resposta da API será no formato JSON
    [Produces("application/json")]

    //Metodo controlador que herda da controller base
    //Onde será criado os Endpoints(rotas)
    public class GeneroController : ControllerBase
    {
        /// <summary>
        /// Objeto _genroRepository que ira receber todos os metodo definidos na interface IGeneroRepositoty
        /// </summary>
        private IGeneroRepository _generoRepository { get; set; }

        /// <summary>
        /// Instancia o objeto _generoRepository para que haja referencia aos metodos no repositorio
        /// </summary>
        public GeneroController()
        {
            _generoRepository = new GeneroRepository();
        }

        /// <summary>
        /// Ele é o Endpoint que aciona o metodo listar Todos no repositorio e retorna a resposta para o usuario(frontEnd)
        /// </summary>
        /// <returns>Resposta para o usuario(front-end)</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //Cria uma lista que recebe os dados da requisição
                List<GeneroDomain> ListaGeneros = _generoRepository.ListarTodos();

                //Retorna a lista no formato JSON ocm o status code Ok(200)
                return StatusCode(200, ListaGeneros);
                //return Ok(ListaGeneros);
            }
            catch (Exception erro)
            {
                //Retorna um status code BadRequest(400) e a mensagem de erro
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint que aciona metodo de cadastro de genero
        /// </summary>
        /// <param name="novoGenero">Objeto recebido na requisição</param>
        /// <returns>Retorna para o usuario(front-end)</returns>
        [HttpPost]
        public IActionResult Post(GeneroDomain novoGenero)
        {
            try
            {
                //fazendo a chamada para o metodo cadastrar passando o objeto como parametro
                _generoRepository.Cadastrar(novoGenero);
                //Retorna um status code 201(created)
                return StatusCode(201);
            }
            catch (Exception erro)
            {
                //Retorna um status code 400(BadRequest) e a mensagem de erro
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint do metodo de deletar o objeto passando o seu id  como parametro
        /// </summary>
        /// <param name="id">O id do objeto a ser deletado</param>
        /// <returns>Retorna o objeto para o front-end</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _generoRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint do metedo de buscar por id
        /// </summary>
        /// <param name="id">Id do objeto a ser buscado</param>
        /// <returns>Retorna para o front-end</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                GeneroDomain genero = _generoRepository.BuscarPorId(id);

                if (genero != null)
                {
                    return Ok(genero); // Retorna o gênero encontrado com status 200 OK
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found se o gênero não for encontrado
                }
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message); // Retorna erro 400 Bad Request em caso de exceção
            }
        }

    }
}
