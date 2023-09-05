using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
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
    public class FilmeController : ControllerBase
    {
        /// <summary>
        /// Objeto _filmeRepository que ira receber todos os metodo definidos na interface IFilmeRepositoty
        /// </summary>
        private IFilmeRepository _filmeRepository { get; set; }
        /// <summary>
        /// Instancia o objeto _filmeRepository para que haja referencia aos metodos no repositorio
        /// </summary>
        public FilmeController()
        {
            _filmeRepository = new FilmeRepository();
        }
        /// <summary>
        /// End point que aciona o metodo listar todos do FilmeRepository
        /// </summary>
        /// <returns>Retorna uma lista com objetos</returns>
        [HttpGet]
        [Authorize(Roles = "Administrador, Comum")]
        public IActionResult Get()
        {
            try
            {
                //Cria uma lista que recebe os dados da requisição
                List<FilmeDomain> ListaFilme = _filmeRepository.ListarTodos();

                //Retorna a lista no formato JSON ocm o status code Ok(200)
                return StatusCode(200, ListaFilme);
                //return Ok(ListaGeneros);
            }
            catch (Exception erro)
            {
                //Retorna um status code BadRequest(400) e a mensagem de erro
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// End point que cadastra um objeto na tabela  filme
        /// </summary>
        /// <param name="novoFilme">Objeto que foi cadastrado</param>
        /// <returns>Retorna o objeto para o front-end</returns>
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public IActionResult Post(FilmeDomain novoFilme)
        {
            try
            {
                //fazendo a chamada para o metodo cadastrar passando o objeto como parametro
                _filmeRepository.Cadastrar(novoFilme);
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
        /// End point que aciona o metodo de deletar
        /// </summary>
        /// <param name="id">id do objeto a ser deletado</param>
        /// <returns>Retorna o objeto para o front-end</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Delete(int id)
        {
            try
            {
                _filmeRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// End point que busca um objeto passando como parametro o seu id
        /// </summary>
        /// <param name="id">id do objeto a ser buscado</param>
        /// <returns>Retorna o objeto ao front end</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador, Comum")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                FilmeDomain filme = _filmeRepository.BuscarPorId(id);

                if (filme != null)
                {
                    return Ok(filme); // Retorna o gênero encontrado com status 200 OK
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
        /// End point que atualizara um objeto filme pelo seu corpo
        /// </summary>
        /// <param name="id">id do objeto a ser atualizado</param>
        /// <returns>Retorna um objeto com navas informações</returns>
        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public IActionResult AtualizarIdCorpo(int id, FilmeDomain filmeAtualizado)
        {
            try
            {
                // Busca o filme pelo ID
                FilmeDomain filmeBuscado = _filmeRepository.BuscarPorId(id);

                if (filmeBuscado != null)
                {
                    // Atualiza as propriedades do filme buscado com os dados do filme atualizado
                    filmeBuscado.Nome = filmeAtualizado.Nome;
                    filmeBuscado.IdGenero = filmeAtualizado.IdGenero;

                    try
                    {
                        _filmeRepository.AtualizarCorpo(filmeBuscado);
                        return Ok(); // Retorna um status code de sucesso
                    }
                    catch (Exception erro)
                    {
                        return BadRequest(erro.Message); // Retorna um status code de erro
                    }
                }
                else
                {
                    return NotFound("Filme não encontrado"); // Retorna um status code 404 se o filme não for encontrado
                }
            }
            catch
            {
                return NotFound("Filme não encontrado");
            }
        }
        /// <summary>
        /// End point que atualizara um objeto pela url
        /// </summary>
        /// <param name="id">id do objeto</param>
        /// <param name="filme">objeto a ser atualizado</param>
        /// <returns>Retorna o objeto para o front end</returns>
        [HttpPatch("{id}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult AtualizarIdUrl(int id, FilmeDomain filme)
        {
            filme.IdFilme = id;
            try
            {
                // Busca o filme pelo ID
                FilmeDomain filmeBuscado = _filmeRepository.BuscarPorId(id);

                if (filmeBuscado != null)
                {
                    // Atualiza as propriedades do filme buscado com os dados do filme atualizado
                    filmeBuscado.Nome = filme.Nome;
                    filmeBuscado.IdGenero = filme.IdGenero;

                    try
                    {
                        _filmeRepository.AtualizarIdUrl(id, filmeBuscado);
                        return Ok(); // Retorna um status code de sucesso
                    }
                    catch (Exception erro)
                    {
                        return BadRequest(erro.Message); // Retorna um status code de erro
                    }
                }
                else
                {
                    return NotFound("Filme não encontrado"); // Retorna um status code 404 se o filme não for encontrado
                }
            }
            catch
            {
                return NotFound("Filme não encontrado");
            }
        }
    }
}

