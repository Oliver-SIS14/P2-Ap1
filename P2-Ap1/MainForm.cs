using P2_Ap1.UI.Consultas;
using P2_Ap1.UI.Registros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2_Ap1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void CategoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formulario = new RegistroCategorias();
            formulario.MdiParent = this;
            formulario.Show();
        }

        private void CategoriasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form formulario = new ConsultaCategorias();
            formulario.MdiParent = this;
            formulario.Show();
        }
    }
}
