using System.Data;

namespace Senac.GerenciamentoJogo.Infra.Data.DataBaseConfigurations
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
