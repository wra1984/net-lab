using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Imposto.Core.Domain;

namespace Imposto.Core.Data
{
    public class NotaFiscalRepository
    {
        #region Atributos
        private System.Data.SqlClient.SqlConnection _connection = null;
        private string _strConection = string.Empty;
        #endregion

        #region Métodos Públicos
        public bool SalvarNota(NotaFiscal nota)
        {
            Conectar();

            SqlCommand command = new SqlCommand("P_NOTA_FISCAL", _connection);
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                int IdNotaFiscal = 0;

                SqlParameter parm = new SqlParameter("@pId", SqlDbType.Int);
                parm.Value = 0;
                parm.Direction = ParameterDirection.InputOutput;
                command.Parameters.Add(parm);
                command.Parameters.Add("@pNumeroNotaFiscal", SqlDbType.Int).Value = nota.NumeroNotaFiscal;
                command.Parameters.Add("@pSerie", SqlDbType.Int).Value = nota.Serie;
                command.Parameters.Add("@pNomeCliente", SqlDbType.VarChar).Value = nota.NomeCliente;
                command.Parameters.Add("@pEstadoDestino", SqlDbType.VarChar).Value = nota.EstadoDestino;
                command.Parameters.Add("@pEstadoOrigem", SqlDbType.VarChar).Value = nota.EstadoOrigem;
                command.ExecuteNonQuery();
                IdNotaFiscal = (int)command.Parameters["@pId"].Value;

                foreach (NotaFiscalItem item in nota.ItensDaNotaFiscal)
                {
                    command = new SqlCommand("P_NOTA_FISCAL_ITEM", _connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@pId", SqlDbType.Int).Value = 0;                    
                    command.Parameters.Add("@pIdNotaFiscal", SqlDbType.Int).Value = IdNotaFiscal;
                    command.Parameters.Add("@pCfop", SqlDbType.VarChar).Value = item.Cfop;
                    command.Parameters.Add("@pTipoIcms", SqlDbType.VarChar).Value = item.TipoIcms;
                    command.Parameters.Add("@pBaseIcms", SqlDbType.Decimal).Value = item.BaseIcms;
                    command.Parameters.Add("@pAliquotaIcms", SqlDbType.Decimal).Value = item.AliquotaIcms;
                    command.Parameters.Add("@pValorIcms", SqlDbType.Decimal).Value = item.ValorIcms;
                    command.Parameters.Add("@pNomeProduto", SqlDbType.VarChar).Value = item.NomeProduto;
                    command.Parameters.Add("@pCodigoProduto", SqlDbType.VarChar).Value = item.CodigoProduto;
                    command.Parameters.Add("@pBaseIPI", SqlDbType.Decimal).Value = item.BaseIPI;
                    command.Parameters.Add("@pAliquotaIPI", SqlDbType.Decimal).Value = item.AliquotaIPI;
                    command.Parameters.Add("@pValorIPI", SqlDbType.Decimal).Value = item.ValorIPI;
                    command.Parameters.Add("@pDesconto", SqlDbType.Decimal).Value = item.Desconto;                                                           
                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (SqlException exception)
            {
                throw exception;
            }
        }
        #endregion

        #region Métodos Privados
        private void Conectar()
        {
            _connection = new SqlConnection();
            _connection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _connection.Open();
        }
        #endregion
    }
}
