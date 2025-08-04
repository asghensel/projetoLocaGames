namespace Senac.GerenciamentoJogo.Domain.Models
{
    public class Jogo
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public bool Disponivel { get; set; }
        public string Responsavel { get; set; }
        public TipoCategoria Categoria { get; set; }
        public DateTime? DataRetirada { get; set; }
        public bool IsEmAtraso{get; set;}
    }
}
