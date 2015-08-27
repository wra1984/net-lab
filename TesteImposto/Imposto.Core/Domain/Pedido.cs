using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class Pedido
    {
        #region Propriedades
        public string EstadoDestino { get; set; }
        public string EstadoOrigem { get; set; }
        public string NomeCliente { get; set; }
        public List<PedidoItem> ItensDoPedido { get; set; }
        #endregion

        #region Construtor
        public Pedido()
        {
            ItensDoPedido = new List<PedidoItem>();
        }
        #endregion
    }
}
