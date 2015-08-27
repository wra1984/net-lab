using Imposto.Core.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Imposto.Core.Domain;
using System.Configuration;

namespace TesteImposto
{
    public partial class FormImposto : Form
    {
        string _diretorio = string.Empty;
        NotaFiscalService _service = null;

        public FormImposto()
        {
            InitializeComponent();
            
            _service = new NotaFiscalService();            
            
           ResizeColumns();
        }

        private void ResizeColumns()
        {
            dataGridViewPedidos.AutoGenerateColumns = true;
            dataGridViewPedidos.DataSource = _service.PrepararItensPedido();
            
            double mediaWidth = dataGridViewPedidos.Width / dataGridViewPedidos.Columns.GetColumnCount(DataGridViewElementStates.Visible);

            for (int i = dataGridViewPedidos.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = dataGridViewPedidos.Columns[i];
                coluna.Width = Convert.ToInt32(mediaWidth);
            }

            ListaEstado lista = new ListaEstado();
            ListaEstado lista2 = new ListaEstado();

            cbxEstadoOrigem.DataSource = lista.EstadosLista;
            cbxEstadoDestino.DataSource = lista2.EstadosLista;

            _diretorio = ConfigurationManager.AppSettings.Get("CaminhoNota").ToString();
        }

        private void btnGerarNota_Click(object sender, EventArgs e)
        {
            GerarNota();
        }

        private void GerarNota()
        {
            try
            {
                Pedido pedido = _service.GerarPedido(textBoxNomeCliente.Text,
                                                    (string)cbxEstadoOrigem.SelectedValue,
                                                    (string)cbxEstadoDestino.SelectedValue,
                                                    (DataTable)dataGridViewPedidos.DataSource);

                _service.GerarNotaFiscal(pedido, _diretorio);

                MessageBox.Show("Operação efetuada com sucesso");

                textBoxNomeCliente.Clear();
                ResizeColumns();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormImposto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                GerarNota();
            }
        }
    }
}
