using P2_Ap1.BLL;
using P2_Ap1.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2_Ap1.UI.Consultas
{
    public partial class ConsultaCategorias : Form
    {
        public ConsultaCategorias()
        {
            InitializeComponent();
        }

        private void ConsultarButton_Click(object sender, EventArgs e)
        {
            var Listado = new List<Factura>();
            RepositorioBase<Factura> repo = new RepositorioBase<Factura>();

            if(CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 1:
                        Listado = repo.GetList(p => true);
                        break;
                    case 2:
                        int id = Convert.ToInt32(CriterioTextBox.Text);
                        Listado = repo.GetList(p => p.Facturaid == id);
                        break;
                }
                Listado = Listado.Where(c => c.Fecha >= DesdeDateTimePicker.Value.Date && c.Fecha <= HastaDateTimePicker.Value.Date).ToList();
            }
            else
            {
                Listado = repo.GetList(p => true);
            }

            ConsultaDataGridView.DataSource = null;
            ConsultaDataGridView.DataSource = Listado;
        }
    }
}
