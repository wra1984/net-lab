using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Imposto.Core.Service
{
    public class NotaFiscalService
    {
        public void GerarNotaFiscal(Domain.Pedido pedido, string diretorio)
        {
            NotaFiscal notaFiscal = new NotaFiscal();
            notaFiscal.EmitirNotaFiscal(pedido);

            foreach (NotaFiscalItem item in notaFiscal.ItensDaNotaFiscal)
            {
                if (String.IsNullOrEmpty(item.Cfop))
                {
                    throw new Exception("CFOP não foi atribuído ao item: " + item.CodigoProduto);
                }
            }

            if (GerarXMLNota(diretorio, notaFiscal))
            {
                Data.NotaFiscalRepository repositorio = new Data.NotaFiscalRepository();
                repositorio.SalvarNota(notaFiscal);                
            }
        }

        public DataTable PrepararItensPedido()
        {
            DataTable table = new DataTable("pedidos");
            table.Columns.Add(new DataColumn("Nome do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Codigo do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Valor", typeof(decimal)));
            table.Columns.Add(new DataColumn("Brinde", typeof(bool)));

            return table;
        }

        public Pedido GerarPedido(string cliente, string ufOrigem, string ufDestino, DataTable itens )
        {
            if (String.IsNullOrEmpty(cliente))
            {
                throw new Exception("Favor informar o cliente para o pedido");
            }
            if (String.IsNullOrEmpty(ufOrigem))
            {
                throw new Exception("Favor informar o estado de origem do pedido");
            }
            if (String.IsNullOrEmpty(ufDestino))
            {
                throw new Exception("Favor informar o estado de destino do pedido");
            }
            if (itens.Rows.Count == 0)
            {
                throw new Exception("Não foi informado itens para o pedido");
            }


            Pedido pedido = new Pedido();

            pedido.EstadoOrigem = ufOrigem; 
            pedido.EstadoDestino = ufDestino;
            pedido.NomeCliente = cliente;

            foreach (DataRow row in itens.Rows)
            {
                pedido.ItensDoPedido.Add(
                    new PedidoItem()
                    {
                        Brinde = Convert.ToBoolean(row["Brinde"]),
                        CodigoProduto = row["Codigo do produto"].ToString(),
                        NomeProduto = row["Nome do produto"].ToString(),
                        ValorItemPedido = Convert.ToDouble(row["Valor"].ToString())
                    });
            }

            return pedido;
        }

        public bool GerarXMLNota(string diretorio, NotaFiscal nota)
        {
            try
            {
                if (!diretorio.EndsWith("\\"))
                {
                    diretorio = diretorio + "\\";
                }

                diretorio = diretorio + nota.NumeroNotaFiscal.ToString() + " - "+nota.NomeCliente.ToString() +".xml";

                if (!File.Exists(diretorio))
                {                                                        
                    XmlTextWriter xmlArquivo = new XmlTextWriter(diretorio, Encoding.GetEncoding("ISO-8859-1"));
                    XmlSerializer x = new XmlSerializer(nota.GetType());
                    x.Serialize(xmlArquivo, nota);
                    if (File.Exists(diretorio))
                        return true;
                    else
                        return false;
                }
                else
                    return false;

                
            }
            catch (Exception e)
            {
                return false;
                throw new Exception(e.Message);
            }
        }
    }
}
