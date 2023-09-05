using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Administrador, Comum")]
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
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador, Comum")]
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

        /// <summary>
        /// Atualiza um gênero pelo ID passado pelo corpo da solicitação.
        /// </summary>
        /// <param name="id">O ID do gênero a ser atualizado.</param>
        /// <param name="genero">O objeto contendo as informações atualizadas do gênero.</param>
        /// <returns>Uma resposta indicando o resultado da atualização.</returns>
        [HttpPatch("{id}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult AtualizarIdCorpo(int id, GeneroDomain genero)
        {
            try
            {
                genero.IdGenero = id; // Define o ID do gênero com base no parâmetro da rota
                _generoRepository.AtualizarIdCorpo(genero); // Chama o método do repositório para atualizar o gênero
                return Ok(); // Retorna uma resposta de sucesso (HTTP 200 OK)
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message); // Retorna uma resposta de erro com a mensagem da exceção
            }
        }

        /// <summary>
        /// Atualiza o nome de um gênero pelo ID passado pela URL.
        /// </summary>
        /// <param name="idGenero">O ID do gênero a ser atualizado.</param>
        /// <param name="genero">O objeto contendo o novo nome do gênero.</param>
        /// <returns>Uma resposta indicando o resultado da atualização.</returns>
        [HttpPatch("atualizar/{idGenero}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult AtualizarIdUrl(int idGenero, GeneroDomain genero)
        {
            try
            {
                genero.IdGenero = idGenero; // Define o ID do gênero com base no parâmetro da rota
                _generoRepository.AtualizarIdCorpo(genero); // Chama o método do repositório para atualizar o gênero
                return StatusCode(201); // Retorna uma resposta de sucesso (HTTP 201 Created)
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message); // Retorna uma resposta de erro com a mensagem da exceção
            }
        }
    }
}
