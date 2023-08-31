using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.filme.manha.Domains;
using webapi.filme.manha.Interfaces;
using webapi.filme.manha.Repositories;

namespace webapi.filme.manha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Senha)
        {
            UsuarioDomain usuario= _usuarioRepository.Login(Email, Senha);

            try
            {
                if (usuario != null)
                {
                    if (usuario.Permissao == true)
                    {
                        return StatusCode(202, usuario);
                    }

                    return StatusCode(401, usuario);
                }

                return null;

            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
