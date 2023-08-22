using webapi.filme.manha.Domains;
using webapi.filme.manha.Interfaces;

namespace webapi.filme.manha.Repositories
{
    public class GeneroRepository : IGeneroRepository
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
        void IGeneroRepository.AtualizarIdCorpo(GeneroDomain Genero)
        {
            throw new NotImplementedException();
        }

        void IGeneroRepository.AtualizarIdUrl(int id, GeneroDomain genero)
        {
            throw new NotImplementedException();
        }

        GeneroDomain IGeneroRepository.BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        void IGeneroRepository.Cadastrar(GeneroDomain NovoGenero)
        {
            throw new NotImplementedException();
        }

        void IGeneroRepository.Deletar(int id)
        {
            throw new NotImplementedException();
        }

        List<GeneroDomain> IGeneroRepository.ListarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
