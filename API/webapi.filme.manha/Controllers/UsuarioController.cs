using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using webapi.filme.manha.Domains;
using webapi.filme.manha.Interfaces;
using webapi.filme.manha.Repositories;

namespace webapi.filme.manha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]

    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        /// <summary>
        /// Método para realizar o login de um usuário com base no email e senha fornecidos.
        /// </summary>
        /// <param name="usuario">O objeto UsuarioDomain contendo o email e senha do usuário.</param>
        /// <returns>
        /// Um IActionResult que pode ser um NotFound com uma mensagem de erro se o login falhar,
        /// ou um Ok com os dados do usuário se o login for bem-sucedido.
        /// </returns>
        [HttpPost]
        public IActionResult Login(UsuarioDomain usuario)
        {
            try
            {
                // Chama o método Login do repositório para autenticar o usuário.
                UsuarioDomain usuarioBuscado = _usuarioRepository.Login(usuario.Email, usuario.Senha);

                if (usuarioBuscado == null)
                {
                    return NotFound("Email ou senha inválidos"); // Retorna um erro 404 se o login falhar.
                }

                //Caso encontre o usuario prossegue para a criação do Token

                //1 - Definir as informações(Claims) que serão fornecidos no token(PAYLOAD)

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),

                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),

                    new Claim(ClaimTypes.Role, usuarioBuscado.Permissao)

                    //existe a possibilidade de criar uma claim personalizavel
                    //new Claim("Claim Personalizada", "Valo da Claim personalizada")
                };

                //2 - Definir a chave de acesso ao token

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev"));

                //3 - Definir as credencias do token(HEADER) 

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //4 - Gerar o token

                var token = new JwtSecurityToken
                    (
                        //emissor do token
                        issuer: "webapi.filme.manha",

                        //Destinatario do token
                        audience: "webapi.filme.manha",

                        //Dados definidos nas Claims(informações)
                        claims: claims,

                        //Tempo de expiração do token
                        expires: DateTime.Now.AddMinutes(5),

                        //Credenciais do token
                        signingCredentials: creds
                    );
                //5 - Retornar o token criado

                return Ok(new{

                    token = new JwtSecurityTokenHandler().WriteToken(token)

                });
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }
    }
}
