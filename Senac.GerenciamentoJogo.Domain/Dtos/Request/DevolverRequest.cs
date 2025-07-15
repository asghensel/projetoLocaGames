using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senac.GerenciamentoJogo.Domain.Dtos.Request
{
    public class DevolverRequest
    {
        public string Responsavel { get; set; }
        public DateTime DataDevolucao { get; set; }
        public string Observacao { get; set; }
    }
}
