using System.Data.SqlClient;
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

        /// <summary>
        /// Esse metodo vai buscar um objeto passando como parametro o seu id
        /// </summary>
        /// <param name="id">o Id do objeto a ser buscado</param>
        /// <returns>Retrona a informação para o front-end</returns>
        GeneroDomain IGeneroRepository.BuscarPorId(int id)
        {
            using (SqlConnection connection = new SqlConnection(StringConexao))
            {
                string queryBuscar = "SELECT * FROM Genero WHERE IdGenero = @IdGenero";

                using (SqlCommand cmd = new SqlCommand(queryBuscar, connection))
                {
                    cmd.Parameters.AddWithValue("@IdGenero", id);

                    connection.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            GeneroDomain genero = new GeneroDomain();

                            genero.IdGenero = Convert.ToInt32(rdr["IdGenero"]);
                            genero.Nome = rdr["Nome"].ToString();

                            return genero;
                        }
                        else
                        {
                            return null;
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Esse metodo vai cadastrar um novo genero
        /// </summary>
        /// <param name="NovoGenero">Objeto com as informaçõs que seram cadastradas</param>
        void IGeneroRepository.Cadastrar(GeneroDomain novoGenero)
        {
            //Declara a conexão passando a string de conexão como parametro
            using (SqlConnection connection = new SqlConnection(StringConexao))
            {
                //Declara a query a ser executada
                string queryInsert = "INSERT INTO Genero(Nome) VALUES (@Nome)";

                //Declara o SqlCommand passando a query que será execuatada e a conexão com o bd
                using (SqlCommand cmd = new SqlCommand(queryInsert, connection))
                {

                    cmd.Parameters.AddWithValue("@Nome", novoGenero.Nome);

                    //Abre a conexão com o bd
                    connection.Open();

                    //Executar a query(queryInsert)
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Esse metodo vai deletar um objeto passando como parametro um id
        /// </summary>
        /// <param name="id">O objeto a ser deletado</param>
        void IGeneroRepository.Deletar(int id)
        {
            //Declara a conexão passando a string de conexão como parametro
            using (SqlConnection connection = new SqlConnection(StringConexao))
            {
                //Declara a query a ser executada
                string queryDelete = "DELETE FROM Genero WHERE IdGenero = @IdGenero";
                //Declara o SqlCommand passando a query que será execuatada e a conexão com o bd
                using (SqlCommand cmd = new SqlCommand(queryDelete, connection))
                {
                    cmd.Parameters.AddWithValue("@IdGenero", id);

                    //Abre a conexão com o bd
                    connection.Open();

                    //Executar a query(queryInsert)
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Esse metodo vai listar todos os objetos da tabela Genero
        /// </summary>
        /// <returns></returns>
        List<GeneroDomain> IGeneroRepository.ListarTodos()
        {
            //Cria uma lista de objetos do tipo gênero
            List<GeneroDomain> ListaGeneros = new List<GeneroDomain>();

            using (SqlConnection connection = new SqlConnection(StringConexao))
            {
                //Declaração a instrução a ser executada
                string querySelectAll = "SELECT IdGenero,Nome FROM Genero";

                //Ele abre a conexão com o banco de dados
                connection.Open();

                //Declara o SqlDataReader para percorrer a tabela do banco de dados
                SqlDataReader rdr;

                //Declara o SqlCommand passsando a query que sera executada e a conexão com o bd
                using (SqlCommand cmd = new SqlCommand(querySelectAll, connection))
                {
                    //Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        GeneroDomain genero = new GeneroDomain()
                        {
                            //Atribui a propiedade IdGenero o valor recebido no rdr
                            IdGenero = Convert.ToInt32(rdr[0]),
                            //Atribui a propiedade Nome o valor recebido no rdr
                            Nome = rdr["Nome"].ToString(),
                        };

                        //Adiciona cada objeto dentro da lista
                        ListaGeneros.Add(genero);
                    }
                }
            }
            //Retorna a lista
            return ListaGeneros;
        }
    }
}
