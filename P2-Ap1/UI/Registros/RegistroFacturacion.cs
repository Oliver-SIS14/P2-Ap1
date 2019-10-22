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
    public partial class RegistroFacturacion : Form
    {
        public List<ProductosDetalle> detalle { get; set; }
        public RegistroFacturacion()
        {
            InitializeComponent();
            this.detalle = new List<ProductosDetalle>();
        }
        public void cargarGrid()
        {
            DetalleDataGridView.DataSource = null;
            DetalleDataGridView.DataSource = this.detalle;
        }
        public void Limpiar()
        {
            IDNumericUpDown.Value = 0;
            FechaDateTimePicker.Value = DateTime.Now;
            EstudianteTextBox.Text = string.Empty;
            TotalTextBox.Text = string.Empty;

            this.detalle = new List<ProductosDetalle>();
            cargarGrid();
        }
        public Factura LlenarClase()
        {
            Factura factura = new Factura();

            factura.Facturaid = Convert.ToInt32(IDNumericUpDown.Value);
            factura.Fecha = FechaDateTimePicker.Value;
            factura.Estudiante = EstudianteTextBox.Text;
            factura.productos = this.detalle;
            factura.Total = Convert.ToDecimal(TotalTextBox.Text);

            cargarGrid();
            return factura;
        }
        public void LlenarCampo(Factura factura)
        {
            IDNumericUpDown.Value = factura.Facturaid;
            FechaDateTimePicker.Value = factura.Fecha;
            EstudianteTextBox.Text = factura.Estudiante;
            this.detalle = factura.productos;
            TotalTextBox.Text = Convert.ToString(factura.Total);

            cargarGrid();
        }

        public bool validar()
        {
            bool paso = true;

            MyErrorProvider.Clear();

            if(this.detalle.Count == 0)
            {
                MyErrorProvider.SetError(AgregarButton, "Debe agregar al menos un producto");
                AgregarButton.Focus();
                paso = false;
            }
            if (string.IsNullOrWhiteSpace(EstudianteTextBox.Text))
            {
                MyErrorProvider.SetError(EstudianteTextBox, "El campo estudiante no puede estar vacio");
                EstudianteTextBox.Focus();
                paso = false;
            }
            return paso;
        }
        public bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Factura> repo = new RepositorioBase<Factura>();
            Factura factura = repo.Buscar((int)IDNumericUpDown.Value);
            return factura != null;
        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            bool paso = false;
            RepositorioGuardar repoG = new RepositorioGuardar();
            RepositorioModificar repoM = new RepositorioModificar();

            Factura factura = new Factura();

            if (!validar())
                return;

            factura = LlenarClase();

            if (IDNumericUpDown.Value == 0)
                paso = repoG.Guardar(factura);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No esta registrado");
                    return;
                }
                paso = repoG.Modificar(factura);
            }

            if (paso)
            {
                MessageBox.Show("Guardado");
            }
            else
            {
                MessageBox.Show("No fue posible guardar");
            }
        }

        private void BuscarButton_Click(object sender, EventArgs e)
        {
            int id;
            int.TryParse(IDNumericUpDown.Text, out id);
            RepositorioBase<Factura> repo = new RepositorioBase<Factura>();
            Factura factura = new Factura();
            factura = repo.Buscar(id);

            if (factura == null)
            {
                MessageBox.Show("No encontrado");
            }
            else
            {
                Limpiar();
                LlenarCampo(factura);
            }
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            MyErrorProvider.Clear();
            int id;
            int.TryParse(IDNumericUpDown.Text, out id);
            RepositorioBase<Factura> repo = new RepositorioBase<Factura>();

            if (repo.Buscar(id) != null)
            {
                if (repo.Eliminar(id))
                {
                    MessageBox.Show("Eliminado");
                }
                else
                {
                    MyErrorProvider.SetError(IDNumericUpDown, "No se pudo eliminar ese registro");
                    IDNumericUpDown.Focus();
                }
            }
            else
            {
                MessageBox.Show("Direccion no valida para eliminar");
            }
        }

        private void RemoverButton_Click(object sender, EventArgs e)
        {
            if (DetalleDataGridView.Rows.Count > 0 && DetalleDataGridView.CurrentRow != null)
            {
                detalle.RemoveAt(DetalleDataGridView.CurrentRow.Index);

                cargarGrid();
            }
        }

        private void AgregarButton_Click(object sender, EventArgs e)
        {

            if (DetalleDataGridView.DataSource != null)
                this.detalle = (List<ProductosDetalle>)DetalleDataGridView.DataSource;

            this.detalle.Add(

                new ProductosDetalle(

                    id: 0,
                    Facturaid: (int)IDNumericUpDown.Value,
                    Categoria: CategoriasComboBox.Text,
                    Cantidad: Convert.ToInt32(CantidadTextBox.Text),
                    Precio: Convert.ToDecimal(PrecioTextBox.Text),
                    Importe: (Convert.ToInt32(CantidadTextBox.Text) * Convert.ToDecimal(PrecioTextBox.Text))
                    )

                );

            decimal total = 0;

            foreach (var item in this.detalle)
            {
                total += item.Cantidad * item.Precio;
            }

            TotalTextBox.Text = Convert.ToString(total);
            CategoriasComboBox.Text = string.Empty;
            CantidadTextBox.Text = Convert.ToString(0);
            PrecioTextBox.Text = Convert.ToString(0); 
            ImporteTextBox.Text = Convert.ToString(0);

            cargarGrid();
        }

        private void PrecioTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CantidadTextBox.Text))
            {
                CantidadTextBox.Text = Convert.ToString(0);
            }
            if (string.IsNullOrWhiteSpace(PrecioTextBox.Text))
            {
                PrecioTextBox.Text = Convert.ToString(0);
            }
            if (string.IsNullOrWhiteSpace(PrecioTextBox.Text) || string.IsNullOrWhiteSpace(CantidadTextBox.Text))
            {
                CantidadTextBox.Text = Convert.ToString(0);
                PrecioTextBox.Text = Convert.ToString(0);
            }


            int cantidad = Convert.ToInt32(CantidadTextBox.Text);
            decimal precio = Convert.ToDecimal(PrecioTextBox.Text);
            ImporteTextBox.Text = Convert.ToString(cantidad * precio);
        }

        private void RegistroFacturacion_Load(object sender, EventArgs e)
        {
            RepositorioBase<Categorias> repo = new RepositorioBase<Categorias>();

            var Lista = repo.GetList(p => true);

            foreach (var item in Lista)
            {
                CategoriasComboBox.Items.Add(item.Descripcion);
            }
        }
    }
}
