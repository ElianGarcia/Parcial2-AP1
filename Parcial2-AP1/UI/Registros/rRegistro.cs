using Parcial2_AP1.BLL;
using Parcial2_AP1.DAL;
using Parcial2_AP1.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial2_AP1.UI.Registros
{
    public partial class rRegistro : Form
    {
        List<ServiciosDetalle> ServiciosDetalle;
        GenericaBLL<Servicios> genericaBLL;
        private decimal total;

        public rRegistro()
        {
            this.genericaBLL = new GenericaBLL<Servicios>();
            this.ServiciosDetalle = new List<ServiciosDetalle>();
            total = 0;
            InitializeComponent();
            LlenarComboBox();
        }

        public void CargarGrid()
        {
            DataGridViewCheckBoxColumn columna = new DataGridViewCheckBoxColumn();

            dataGridView.DataSource = null;
            dataGridView.DataSource = this.ServiciosDetalle;
            dataGridView.Columns["ServiciosDetalleID"].Visible = false;
            dataGridView.Columns["CategoriaID"].Visible = false;
        }

        public void Limpiar()
        {
            IDnumericUpDown.Value = 0;
            estudianteTextBox.Text = string.Empty;
            CategoriascomboBox.Text = string.Empty;
            FechadateTimePicker.Value = DateTime.Now;
            this.ServiciosDetalle = new List<ServiciosDetalle>();
            CantidadTextField.Text = string.Empty;
            PrecioTextField.Text = string.Empty;
            ImporteTextField.Text = string.Empty;
            errorProvider.Clear();
            CargarGrid();
        }

        private bool Existe()
        {
            Servicios servicio = genericaBLL.Buscar((int)IDnumericUpDown.Value);

            return (servicio != null);
        }

        public void LlenarComboBox()
        {
            CategoriascomboBox.DataSource = null;
            GenericaBLL<Categorias> genericaBLL = new GenericaBLL<Categorias>();
            List<Categorias> lista = genericaBLL.GetList(p => true);
            CategoriascomboBox.DataSource = lista;
            CategoriascomboBox.DisplayMember = "Nombre";
            CategoriascomboBox.ValueMember = "CategoriaID";
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            int id;
            Servicios servicios = new Servicios();

            genericaBLL = new GenericaBLL<Servicios>();
            int.TryParse(IDnumericUpDown.Text, out id);

            Limpiar();

            servicios = genericaBLL.Buscar(id);

            if (servicios != null)
            {
                LlenaCampos(servicios);
            }
            else
            {
                MessageBox.Show("Servicio no encontrado");
            }
        }

        private void LlenaCampos(Servicios servicios)
        {
            IDnumericUpDown.Value = servicios.ServiciosID;
            estudianteTextBox.Text = servicios.Estudiante;
            FechadateTimePicker.Value = servicios.Fecha;
            TotalTextbox.Text = servicios.Total.ToString();
            dataGridView.DataSource = servicios.Venta;
        }

        private void Removerbutton_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count > 0 && dataGridView.CurrentRow != null)
            {
                decimal valorEliminar = Convert.ToDecimal(dataGridView.CurrentRow.Cells[4].Value);
                total -= valorEliminar;
                ServiciosDetalle.RemoveAt(dataGridView.CurrentRow.Index);
                TotalTextbox.Text = total.ToString();
                CargarGrid();
            }
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            Servicios servicio = new Servicios();
            ServiciosBLL servicios = new ServiciosBLL();
            bool realizado = false;

            if (!Validar())
                return;

            servicio = LlenaClase();


            if (IDnumericUpDown.Value == 0)
                realizado = servicios.Guardar(servicio);
            else
            {
                if (!Existe())
                {
                    MessageBox.Show("NO SE PUEDE MODIFICAR UN SERVICIO INEXISTENTE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                realizado = servicios.Modificar(servicio);
            }

            if (realizado)
            {
                Limpiar();
                MessageBox.Show("GUARDADO EXITOSAMENTE", "GUARDADA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("NO SE PUDO GUARDAR", "NO GUARDADO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Servicios LlenaClase()
        {
            Servicios servicios = new Servicios();

            servicios.ServiciosID = Convert.ToInt32(IDnumericUpDown.Value);
            servicios.Estudiante = estudianteTextBox.Text;
            servicios.Fecha = FechadateTimePicker.Value;
            servicios.Total = Convert.ToDecimal(TotalTextbox.Text);
            servicios.Venta = this.ServiciosDetalle;

            return servicios;
        }

        private bool Validar()
        {
            bool validado = false;

            if (string.IsNullOrWhiteSpace(estudianteTextBox.Text))
            {
                errorProvider.SetError(estudianteTextBox, "Debe agregar algun estudiante");
                estudianteTextBox.Focus();
                validado = false;
            }
            if (this.ServiciosDetalle.Count == 0)
            {
                errorProvider.SetError(dataGridView, "Debe agregar algun servicio");
                CategoriascomboBox.Focus();
                validado = false;
            }

            return validado;
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            int id;
            int.TryParse(IDnumericUpDown.Text, out id);
            Contexto db = new Contexto();

            Limpiar();

            if (genericaBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminada correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                errorProvider.SetError(IDnumericUpDown, "No se puede eliminar un servicio inexistente");
            }
        }

        private void agregarCategoriaButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.DataSource != null)
            {
                this.ServiciosDetalle = (List<ServiciosDetalle>)dataGridView.DataSource;
            }
            GenericaBLL<Categorias> Categorias = new GenericaBLL<Categorias>();

            string nombre = Categorias.Buscar(id: (int)CategoriascomboBox.SelectedIndex + 1).Nombre;

            this.ServiciosDetalle.Add(new ServiciosDetalle(
                serviciosDetalleID: 0,
                categoriaID: (int)CategoriascomboBox.SelectedIndex,
                nombre: nombre,
                cantidad: Convert.ToInt32(CantidadTextField.Text),
                precio: Convert.ToDecimal(PrecioTextField.Text),
                importe: Importe()
                )
            );

            total += Importe();

            TotalTextbox.Text = Convert.ToString(total);
            errorProvider.Clear();
            CargarGrid();
        }

        private bool ValidarDetalle()
        {
            errorProvider.Clear();
            bool validado = true;
            int cantidad = 0;
            decimal precio = 0;

            if (string.IsNullOrWhiteSpace(CantidadTextField.Text))
            {
                errorProvider.SetError(CantidadTextField, "El campo cantidad es obligatorio");
                validado = false;
            }
            else
            {
                try
                {
                    cantidad = Convert.ToInt32(CantidadTextField.Text);
                }
                catch (Exception)
                {
                    errorProvider.SetError(CantidadTextField, "Este debe ser numerico.");
                    validado = false;
                }
            }

            if (string.IsNullOrWhiteSpace(PrecioTextField.Text))
            {

                errorProvider.SetError(PrecioTextField, "El campo precio es obligatorio");
                validado = false;
            }
            else
            {
                try
                {
                    precio = Convert.ToInt32(PrecioTextField.Text);
                }
                catch (Exception)
                {
                    errorProvider.SetError(PrecioTextField, "Este debe ser numerico.");
                    validado = false;
                }
            }

            return validado;
        }

        private decimal Importe()
        {
            decimal cantidad = Convert.ToInt32(CantidadTextField.Text);
            decimal precio = Convert.ToDecimal(PrecioTextField.Text);

            return Convert.ToDecimal(cantidad * precio);
        }

        private void PrecioTextField_TextChanged(object sender, EventArgs e)
        {
            if (!ValidarDetalle())
                return;
            errorProvider.Clear();
            int cantidad = Convert.ToInt32(CantidadTextField.Text);
            decimal precio = Convert.ToDecimal(PrecioTextField.Text);
            decimal importe = cantidad * precio;

            ImporteTextField.Text = importe.ToString();
        }
    }
}
