namespace Senac.GerenciamentoJogo.Domain.Dtos.Response.Jogo
{
    public class DetalhesJogoResponse

    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string TipoCategoria { get; set; }
        public string Responsavel { get; set; } = null;
        public DateTime DataRetirada { get; set; }
        public bool Disponivel { get; set; } = true;
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
