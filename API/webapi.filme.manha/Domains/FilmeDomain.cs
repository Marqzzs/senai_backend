using System.ComponentModel.DataAnnotations;

namespace webapi.filme.manha.Domains
{
    /// <summary>
    /// Classe que representa a entidade(tabela) filme
    /// </summary>
    public class FilmeDomain
    {
        public int? IdFilme { get; set; }

        public int? IdGenero { get; set; }

        [Required(ErrorMessage = "O nome do filme é obrigatorio!")]
        public string? Nome { get; set; }

        public GeneroDomain? Genero { get; set; }
    }
}
