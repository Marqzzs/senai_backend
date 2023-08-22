using webapi.filme.manha.Domains;

namespace webapi.filme.manha.Interfaces
{
    /// <summary>
    /// Interface responsavel pelo repositorio FilmeRepository
    /// Definir os metodos que serao implementados pelo FilmeRespository
    /// </summary>  
    public interface IFilmeRepository
    {
        /// <summary>
        /// Serve para cadastrar um novo Filme
        /// </summary>
        /// <param name="NovoFilme">Objeto que sera cadastrado</param>
        void Cadastrar(FilmeDomain NovoFilme);


        /// <summary>
        /// Metodo para listar todos os filmes
        /// </summary>
        /// <returns>Listagem dos filmes</returns>
        List<FilmeDomain> ListarTodos();

        /// <summary>
        /// Atualizar objeto passando o seu id pelo corpo de requisição
        /// </summary>
        /// <param name="Filme">objeto atualizado(novas informações)</param>
        void AtualizarCorpo (FilmeDomain Filme);

        /// <summary>
        /// Atualizar o objeto passando o seu id pela url
        /// </summary>
        /// <param name="id">id do objeto que sera atualizado</param>
        /// <param name="Filme">Objeto atualizado(novas informações)</param>
        void AtualizarIdUrl (int id, FilmeDomain Filme);

        /// <summary>
        /// Deleta o objeto passando o seu id como parametro
        /// </summary>
        /// <param name="id">id do objeto que sera deletado</param>
        void Deletar(int id);

        /// <summary>
        /// Busca um objeto pelo seu id
        /// </summary>
        /// <param name="id">id do objeto a ser buscado</param>
        /// <returns>Retorna o objeto</returns>
        FilmeDomain BuscarPorId (int id);
    }
}
