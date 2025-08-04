namespace Senac.GerenciamentoJogo.Domain.Dtos.Request
{
    public class CadastrarRequest
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string TipoCategoria { get; set; }
        public string Responsavel { get; set; } 
        DateTime DataRetirada { get; set; } 
        public bool Disponivel { get; set; } 
        
    }
}
