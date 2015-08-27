using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class NotaFiscal
    {
        #region Propriedades
        public int Id { get; set; }
        public int NumeroNotaFiscal { get; set; }
        public int Serie { get; set; }
        public string NomeCliente { get; set; }
        public string EstadoDestino { get; set; }
        public string EstadoOrigem { get; set; }
        public List<NotaFiscalItem> ItensDaNotaFiscal { get; set; }
        #endregion

        #region Construtor
        public NotaFiscal()
        {
            ItensDaNotaFiscal = new List<NotaFiscalItem>();
        }
        #endregion

        #region Métodos Privados
        public void EmitirNotaFiscal(Pedido pedido)
        {
            this.NumeroNotaFiscal = 99999;
            this.Serie = new Random().Next(Int32.MaxValue);
            this.NomeCliente = pedido.NomeCliente;
            this.EstadoDestino = pedido.EstadoDestino; // Correção na inversão de UF.
            this.EstadoOrigem = pedido.EstadoOrigem;

            foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
            {
                NotaFiscalItem notaFiscalItem = new NotaFiscalItem();

                GetCFOP(ref notaFiscalItem);
                
                if (this.EstadoDestino == this.EstadoOrigem)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                }
                else
                {
                    notaFiscalItem.TipoIcms = "10";
                    notaFiscalItem.AliquotaIcms = 0.17;
                }
                if (notaFiscalItem.Cfop == "6.009")
                {
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido*0.90; //redução de base
                }
                else
                {
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido;
                    notaFiscalItem.BaseIPI = itemPedido.ValorItemPedido;

                }
                notaFiscalItem.ValorIcms = Math.Round(notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms,2);                
                notaFiscalItem.ValorIPI = Math.Round(notaFiscalItem.BaseIPI * notaFiscalItem.AliquotaIPI, 2);

                PossuiBrinde(itemPedido, notaFiscalItem);

                notaFiscalItem.NomeProduto = itemPedido.NomeProduto;
                notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;

                this.ItensDaNotaFiscal.Add(notaFiscalItem);
            }
        
            NotaDesconto.aplicarDesconto(this);
        }
        #endregion

        #region Métodos Privados
        private static void PossuiBrinde(PedidoItem itemPedido, NotaFiscalItem notaFiscalItem)
        {
            if (itemPedido.Brinde)
            {
                notaFiscalItem.TipoIcms = "60";
                notaFiscalItem.AliquotaIcms = 0.18;
                notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
                
                notaFiscalItem.AliquotaIPI = 0;
                notaFiscalItem.ValorIPI = notaFiscalItem.BaseIPI * notaFiscalItem.AliquotaIPI;
            }
        }

        private void GetCFOP(ref NotaFiscalItem notaFiscalItem)
        {
            if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "RJ"))
            {
                notaFiscalItem.Cfop = "6.000";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PE"))
            {
                notaFiscalItem.Cfop = "6.001";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "MG"))
            {
                notaFiscalItem.Cfop = "6.002";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PB"))
            {
                notaFiscalItem.Cfop = "6.003";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PR"))
            {
                notaFiscalItem.Cfop = "6.004";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PI"))
            {
                notaFiscalItem.Cfop = "6.005";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "RO"))
            {
                notaFiscalItem.Cfop = "6.006";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "SE"))
            {
                notaFiscalItem.Cfop = "6.007";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "TO"))
            {
                notaFiscalItem.Cfop = "6.008";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "SE"))
            {
                notaFiscalItem.Cfop = "6.009";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PA"))
            {
                notaFiscalItem.Cfop = "6.010";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "RJ"))
            {
                notaFiscalItem.Cfop = "6.000";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PE"))
            {
                notaFiscalItem.Cfop = "6.001";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "MG"))
            {
                notaFiscalItem.Cfop = "6.002";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PB"))
            {
                notaFiscalItem.Cfop = "6.003";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PR"))
            {
                notaFiscalItem.Cfop = "6.004";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PI"))
            {
                notaFiscalItem.Cfop = "6.005";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "RO"))
            {
                notaFiscalItem.Cfop = "6.006";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "SE"))
            {
                notaFiscalItem.Cfop = "6.007";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "TO"))
            {
                notaFiscalItem.Cfop = "6.008";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "SE"))
            {
                notaFiscalItem.Cfop = "6.009";
            }
            else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PA"))
            {
                notaFiscalItem.Cfop = "6.010";
            }
            else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "RO")) // Correção de bug Ex. 5
            {
                notaFiscalItem.Cfop = "6.006";
            }
        }
        #endregion
    }

    public class NotaDesconto
    {
        public static void aplicarDesconto(NotaFiscal nota)
        {
            // Exercicio 7.
            if ((nota.EstadoDestino == "SP") || (nota.EstadoDestino == "MG") || (nota.EstadoDestino == "RJ") || (nota.EstadoDestino == "ES"))
            {
                foreach (NotaFiscalItem item in nota.ItensDaNotaFiscal)
                {
                    item.Desconto = 10;
                }
            }
        }
    }
}
