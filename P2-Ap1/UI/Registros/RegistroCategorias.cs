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

namespace P2_Ap1.UI.Registros
{
    public partial class RegistroCategorias : Form
    {
        public RegistroCategorias()
        {
            InitializeComponent();
        }
        public void Limpiar()
        {
            IDNumericUpDown.Value = 0;
            DescripcionTextBox.Text = string.Empty;
        }
        public Categorias LlenarClase()
        {
            Categorias categorias = new Categorias();

            categorias.id = Convert.ToInt32(IDNumericUpDown.Value);
            categorias.Descripcion = DescripcionTextBox.Text;

            return categorias;
        }
        public void LlenarCampo(Categorias categoria)
        {
            IDNumericUpDown.Value = categoria.id;
            DescripcionTextBox.Text = categoria.Descripcion;
        }

        public bool Validar()
        {
            bool paso = true;
            MyErrorProvider.Clear();

            if (string.IsNullOrWhiteSpace(DescripcionTextBox.Text))
            {
                MyErrorProvider.SetError(DescripcionTextBox, "El campo descripcion no puede estar vacio.");
                DescripcionTextBox.Focus();
                paso = false;
            }
            return paso;
        }

        public bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Categorias> repo = new RepositorioBase<Categorias>();
            Categorias categorias = repo.Buscar((int)IDNumericUpDown.Value);

            return categorias != null;
        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            bool paso = false;
            RepositorioBase<Categorias> repo = new RepositorioBase<Categorias>();
            Categorias categorias = new Categorias();

            if (!Validar())
                return;

            categorias = LlenarClase();

            if (IDNumericUpDown.Value == 0)
                paso = repo.Guardar(categorias);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No existe en la base de datos");
                }
                paso = repo.Modificar(categorias);
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado","Exito",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No fue Posible guardar","Fallo",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void BuscarButton_Click(object sender, EventArgs e)
        {
            int id;
            int.TryParse(IDNumericUpDown.Text, out id);
            RepositorioBase<Categorias> repo = new RepositorioBase<Categorias>();
            Categorias categoria = repo.Buscar(id);
            
            Limpiar();

            if(categoria == null)
            {
                MessageBox.Show("No encontrado", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                LlenarCampo(categoria);
            }
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            MyErrorProvider.Clear();
            int id;
            int.TryParse(IDNumericUpDown.Text, out id);
            RepositorioBase<Categorias> repo = new RepositorioBase<Categorias>();

            Limpiar();

            if (repo.Buscar(id) != null)
            {
                if (repo.Eliminar(id))
                {
                    MessageBox.Show("Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MyErrorProvider.SetError(IDNumericUpDown, "No se pudo eliminar la entidad con ese ID");
                    IDNumericUpDown.Focus();
                }
            }
            else
            {
                MyErrorProvider.SetError(IDNumericUpDown, "No se pudo encontrar la entidad con ese ID");
                IDNumericUpDown.Focus();
            }

        }
    }
}
