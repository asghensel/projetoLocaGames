using System.Security;

namespace Senac.GerenciamentoJogo.Domain.Dtos.Response.Jogo
{
    public class TodosJogosResponse
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public bool Disponivel { get; set; }
        public string TipoCategoria { get; set; }
        public DateTime DataRetirada { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public bool IsEmAtraso
        {
            get
            {
                return DataRetirada < DateTime.Now;
            }
        }
    }
}
