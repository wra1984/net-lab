using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Imposto.Core.Service;
using System.Data;
using Imposto.Core.Domain;
using System.Configuration;

namespace Tests
{
    [TestClass]
    public class NotaTests
    {
        NotaFiscalService _service = null;

        [TestMethod]
        public void GerarPedido()
        {
            _service  = new NotaFiscalService();
            DataTable dados = _service.PrepararItensPedido();
            DataRow linha = dados.NewRow();
            linha[0] = "Sabonete";
            linha[1] = "100101";
            linha[2] = 10.00;
            linha[3] = false;
            dados.Rows.Add(linha);

            Pedido pedido = _service.GerarPedido("Willian Rodrigues", "SP", "MG", dados);

            if (pedido != null)
                Console.Write("Pedido para " + pedido.NomeCliente);
        }


        [TestMethod]
        public void TestarDesconto()
        {
            _service = new NotaFiscalService();
            DataTable dados = _service.PrepararItensPedido();
            DataRow linha = dados.NewRow();
            linha[0] = "Sabonete";
            linha[1] = "100101";
            linha[2] = 10.00;
            linha[3] = false;
            dados.Rows.Add(linha);

            Pedido pedido = _service.GerarPedido("Willian Rodrigues", "SP", "MG", dados);

            NotaFiscal notaFiscal = new NotaFiscal();
            notaFiscal.EmitirNotaFiscal(pedido);

            if (notaFiscal.ItensDaNotaFiscal[0].Desconto == 10 )
                Console.Write("Desconto foi aplicado!");
        }

        [TestMethod]
        public void GerarXMLNota()
        {
            string _diretorio = ConfigurationManager.AppSettings.Get("CaminhoNota").ToString();

            _service = new NotaFiscalService();
            DataTable dados = _service.PrepararItensPedido();
            DataRow linha = dados.NewRow();
            linha[0] = "Sabonete";
            linha[1] = "100101";
            linha[2] = 10.00;
            linha[3] = false;
            dados.Rows.Add(linha);

            Pedido pedido = _service.GerarPedido("Willian Rodrigues", "SP", "MG", dados);
            
            NotaFiscal notaFiscal = new NotaFiscal();
            notaFiscal.EmitirNotaFiscal(pedido);

            _service.GerarXMLNota(_diretorio, notaFiscal);

            if (pedido != null)
                Console.Write("Pedido para " + notaFiscal.NumeroNotaFiscal);
        }

        [TestMethod]
        public void GerarNotaFiscal()
        {
            string _diretorio = ConfigurationManager.AppSettings.Get("CaminhoNota").ToString();

            _service = new NotaFiscalService();
            DataTable dados = _service.PrepararItensPedido();
            DataRow linha = dados.NewRow();
            linha[0] = "Sabonete";
            linha[1] = "100101";
            linha[2] = 10.00;
            linha[3] = false;
            dados.Rows.Add(linha);

            Pedido pedido = _service.GerarPedido("Willian Rodrigues", "SP", "MG", dados);

            _service.GerarNotaFiscal(pedido, _diretorio);            
        }
    }
}
