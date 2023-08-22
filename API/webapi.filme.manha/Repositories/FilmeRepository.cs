using webapi.filme.manha.Domains;
using webapi.filme.manha.Interfaces;

namespace webapi.filme.manha.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        /// <summary>
        /// string de conexao com o banco de dados que recebe os seguintes parametros
        /// Data Source : nome do servidor
        /// Initial Catalog : nome do banco de dados
        /// Autenticacao :
        ///     - Windows : integrated Security = true;
        ///     - SQL : User id : sa; Pwd : Senha
        /// </summary>
        public string StringConexao = "Data Source = NOTE09-S14; Initial Catalog = Filmes; User Id = SA; Pwd = Senai@134";
        public void AtualizarCorpo(FilmeDomain Filme)
        {
            throw new NotImplementedException();
        }

        public void AtualizarIdUrl(int id, FilmeDomain Filme)
        {
            throw new NotImplementedException();
        }

        public FilmeDomain BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(FilmeDomain NovoFilme)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public List<FilmeDomain> ListarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
