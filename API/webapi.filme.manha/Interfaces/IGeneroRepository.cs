using webapi.filme.manha.Domains;

namespace webapi.filme.manha.Interfaces
{
    /// <summary>
    /// Interface responsavel pelo repositorio GeneroRepository
    /// Definir os metodos que serao implementados pelo GeneroRespository
    /// </summary>
    public interface IGeneroRepository
    {
        //TipodeRetorno NomedoMetodo (TipodeParametro NomeParametro)

        /// <summary>
        /// Serve para cadastrar um novo genero
        /// </summary>
        /// <param name="NovoGenero">Objeto que sera cadastrado</param> 
        void Cadastrar(GeneroDomain NovoGenero);

        /// <summary>
        /// Listar todos os generos
        /// </summary>
        /// <returns>Retorna os objetos listados</returns>
        List<GeneroDomain> ListarTodos();

        /// <summary>
        /// Atualizar objeto existente passando o seu id pelo corpo da requisição
        /// </summary>
        /// <param name="Genero">Objeto atualizado (novas informações)</param> 
        void AtualizarIdCorpo(GeneroDomain Genero);

        /// <summary>
        /// Atualizar objeto existente passando seu id pela url
        /// </summary>
        /// <param name="id">id do objeto que sera atualizado</param> 
        /// <param name="genero">objeto atualizado (novas informações)</param> 
        void AtualizarIdUrl(int id, GeneroDomain genero);

        /// <summary>
        /// 
        /// Deletar um objeto
        /// </summary>
        /// <param name="id">id so objeto que sera deletado</param> 
        void Deletar (int id);


        /// <summary>
        /// Busca um objeto pelo atraves do seu id
        /// </summary>
        /// <param name="id">id do objeto a ser buscado</param> 
        /// <returns>o objeto buscado</returns> 
        GeneroDomain BuscarPorId(int id);
    }
}
