using System.Data.SqlClient;
using webapi.filme.manha.Domains;
using webapi.filme.manha.Interfaces;

namespace webapi.filme.manha.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        // A string de conexão com o banco de dados SQL Server.
        private string StringConexao = "Data Source=NOTE09-S14; Initial Catalog=Filmes; User ID =sa; Pwd =Senai@134";

        /// <summary>
        /// Método para realizar o login de um usuário com base no email e senha.
        /// </summary>
        /// <param name="email">O email do usuário.</param>
        /// <param name="senha">A senha do usuário.</param>
        /// <returns>Um objeto do tipo UsuarioDomain se o login for bem-sucedido, caso contrário, retorna null.</returns>
        public UsuarioDomain Login(string email, string senha)
        {
            // Cria uma nova conexão com o banco de dados.
            using (SqlConnection connection = new SqlConnection(StringConexao))
            {
                // Define a consulta SQL para buscar o usuário com base no email e senha fornecidos.
                string queryUsuario = "SELECT IdUsuario, Email, Permissao FROM Usuario WHERE Email = @Email AND Senha = @Senha";

                // Abre a conexão com o banco de dados.
                connection.Open();

                // Cria um novo comando SQL usando a consulta e a conexão.
                using (SqlCommand cmd = new SqlCommand(queryUsuario, connection))
                {
                    // Adiciona parâmetros ao comando para substituir os marcadores na consulta.
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Senha", senha);

                    // Executa a consulta SQL e obtém o resultado.
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // Verifica se há dados no resultado.
                    if (rdr.Read())
                    {
                        // Cria um objeto UsuarioDomain e popula com os dados do resultado.
                        UsuarioDomain usuario = new UsuarioDomain
                        {
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"]),
                            Email = rdr["Email"].ToString(),
                            Permissao = rdr["Permissao"].ToString()
                        };
                        return usuario; // Retorna o objeto do usuário encontrado.
                    }
                    return null; // Retorna null se nenhum usuário for encontrado.
                }
            }
        }
    }
}
