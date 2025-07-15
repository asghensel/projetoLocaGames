using System.Text.Json.Serialization;

namespace Senac.GerenciamentoJogo.Domain.Dtos.Response
{
    public class ErroResponse
    {
        public string Mensagem { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Codigo { get; set; }
    }
}
