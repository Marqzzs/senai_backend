using System.Data.SqlClient;
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
            using (SqlConnection connection = new SqlConnection(StringConexao))
            {
                string queryBuscar = "SELECT IdFilme, IdGenero, Nome FROM Filme WHERE IdFilme= @IdFilme";

                using (SqlCommand cmd = new SqlCommand(queryBuscar, connection))
                {

                    cmd.Parameters.AddWithValue("@IdFilme", id);
                    //Abre a conexão com o banco de dados
                    connection.Open();

                    //Declara o SqlDataReader para percorrer a tabela do banco de dados
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            // Cria um objeto FilmeDomain e preenche suas propriedades com os valores do rdr
                            FilmeDomain filme = new FilmeDomain()
                            {
                                IdFilme = Convert.ToInt32(rdr["IdFilme"]),
                                Nome = rdr["Nome"].ToString(),
                                IdGenero = Convert.ToInt32(rdr["IdGenero"]),

                                // Cria um novo objeto GeneroDomain e preenche suas propriedades com os valores do rdr
                                Genero = new GeneroDomain()
                                {
                                    IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                                    Nome = rdr["Nome"].ToString()
                                }
                            };
                            return filme;
                        }
                        else
                        {
                            //Retorna nulo
                            return null;
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Metodo que cadastra um objeto na tabela Filme
        /// </summary>
        /// <param name="NovoFilme">Novo objeto que foi cadastrado</param>
        public void Cadastrar(FilmeDomain NovoFilme)
        {
            //Declara a conexão passando a string de conexão como parametro
            using (SqlConnection connection = new SqlConnection(StringConexao))
            {
                //Declara a query a ser executada
                string queryInsert = "INSERT INTO Filme(IdGenero, Nome) VALUES (@IdGenero, @Nome)";

                //Declara o SqlCommand passando a query que será executada e a conexão com o bd
                using (SqlCommand cmd = new SqlCommand(queryInsert, connection))
                {
                    // Atribui o valor do IdGenero do NovoFilme ao parâmetro @IdGenero
                    cmd.Parameters.AddWithValue("@IdGenero", NovoFilme.IdGenero);
                    cmd.Parameters.AddWithValue("@Nome", NovoFilme.Nome);

                    //Abre a conexão com o bd
                    connection.Open();

                    //Executar a query(queryInsert)
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Metodo que deleta o objeto da tabela filme
        /// </summary>
        /// <param name="id">Id do objeto que sera deletado</param>
        public void Deletar(int id)
        {
            //Declara a conexão passando a string de conexão como parametro
            using (SqlConnection connection = new SqlConnection(StringConexao))
            {
                //Declara a query a ser executada
                string queryDelete = "DELETE FROM Filme WHERE IdFilme = @IdFilme";
                //Declara o SqlCommand passando a query que será execuatada e a conexão com o bd
                using (SqlCommand cmd = new SqlCommand(queryDelete, connection))
                {
                    cmd.Parameters.AddWithValue("@IdFilme", id);

                    //Abre a conexão com o bd
                    connection.Open();

                    //Executar a query(queryInsert)
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Metodo de listar todos os objetos da tabela filme
        /// </summary>
        /// <returns>Retorna a lista com os objetos</returns>
        public List<FilmeDomain> ListarTodos()
        {
            // Cria uma lista para armazenar os objetos do tipo FilmeDomain
            List<FilmeDomain> ListaFilme = new List<FilmeDomain>();

            // Cria uma conexão com o banco de dados utilizando a string de conexão fornecida
            using (SqlConnection connection = new SqlConnection(StringConexao))
            {
                // Define a consulta SQL que seleciona os campos necessários das tabelas Filme e Genero
                string querySelectAll = "SELECT F.IdFilme, F.Nome, F.IdGenero, G.Nome AS NomeGenero FROM Filme F INNER JOIN Genero G ON F.IdGenero = G.IdGenero";

                // Abre a conexão com o banco de dados
                connection.Open();

                // Declara um objeto SqlDataReader para percorrer os resultados da consulta
                SqlDataReader rdr;

                // Cria um novo SqlCommand com a consulta SQL e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelectAll, connection))
                {
                    // Executa a consulta SQL e armazena os resultados no objeto rdr
                    rdr = cmd.ExecuteReader();

                    // Percorre os resultados da consulta
                    while (rdr.Read())
                    {
                        // Cria um objeto FilmeDomain e preenche suas propriedades com os valores do rdr
                        FilmeDomain filme = new FilmeDomain()
                        {
                            IdFilme = Convert.ToInt32(rdr["IdFilme"]),
                            Nome = rdr["Nome"].ToString(),
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),

                            // Cria um novo objeto GeneroDomain e preenche suas propriedades com os valores do rdr
                            Genero = new GeneroDomain()
                            {
                                IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                                Nome = rdr["NomeGenero"].ToString()
                            }
                        };

                        // Adiciona o objeto FilmeDomain à lista ListaFilme
                        ListaFilme.Add(filme);
                    }
                }

                // Fecha o SqlDataReader para liberar recursos
                rdr.Close();

                // Retorna a lista de filmes populada com os objetos FilmeDomain e suas associações de gênero
                return ListaFilme;
            }
        }

    }
}
