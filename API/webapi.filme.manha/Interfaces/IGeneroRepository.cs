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

        void Cadastrar(GeneroDomain NovoGenero);
        List<GeneroDomain> ListarTodos();

        void AtualizarIdCorpo(GeneroDomain Genero);

        void Deletar (int id);
    }
}
