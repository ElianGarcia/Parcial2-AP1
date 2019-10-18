using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parcial2_AP1.Entidades;
using System.Windows.Forms;
using Parcial2_AP1.BLL;
using Parcial2_AP1.DAL;

namespace Parcial2_AP1.UI.Registros
{
    public partial class rCategorias : Form
    {
        GenericaBLL<Categorias> generica;
        public rCategorias()
        {
            generica = new GenericaBLL<Categorias>();
            InitializeComponent();
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            int id;
            Categorias categoria = new Categorias();

            generica = new GenericaBLL<Categorias>();
            int.TryParse(IDnumericUpDown.Text, out id);

            Limpiar();

            categoria = generica.Buscar(id);

            if (categoria != null)
            {
                LlenaCampos(categoria);
            }
            else
            {
                MessageBox.Show("Categoria no encontrada");
            }
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            Categorias categoria = new Categorias();
            bool realizado = false;

            if (!Validar())
                return;

            categoria = LlenaClase();


            if (IDnumericUpDown.Value == 0)
                realizado = generica.Guardar(categoria);
            else
            {
                if (!Existe())
                {
                    MessageBox.Show("NO SE PUEDE MODIFICAR UNA CATEGORIA INEXISTENTE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                realizado = generica.Modificar(categoria);
            }

            if (realizado)
            {
                Limpiar();
                MessageBox.Show("GUARDADA EXITOSAMENTE", "GUARDADA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("NO SE PUDO GUARDAR", "NO GUARDADA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool Validar()
        {
            bool realizado = true;
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(NombretextBox.Text))
            {
                errorProvider.SetError(NombretextBox, "EL CAMPO NOMBRE NO PUEDE ESTAR VACIO");
                NombretextBox.Focus();
                realizado = false;
            }

            return realizado;
        }

        private Categorias LlenaClase()
        {
            Categorias categoria = new Categorias();
            categoria.CategoriaID = Convert.ToInt32(IDnumericUpDown.Value);
            categoria.Nombre = NombretextBox.Text;

            return categoria;
        }

        private bool Existe()
        {
            Categorias categoria = generica.Buscar((int)IDnumericUpDown.Value);

            return (categoria != null);
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            int id;
            int.TryParse(IDnumericUpDown.Text, out id);
            Contexto db = new Contexto();

            Limpiar();

            if (generica.Eliminar(id))
            {
                MessageBox.Show("Eliminada correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                errorProvider.SetError(IDnumericUpDown, "No se puede eliminar una categoria inexistente");
            }
        }

        private void Limpiar()
        {
            IDnumericUpDown.Value = 0;
            NombretextBox.Text = string.Empty;
        }

        private void LlenaCampos(Categorias categoria)
        {
            IDnumericUpDown.Value = categoria.CategoriaID;
            NombretextBox.Text = categoria.Nombre;
        }
    }
}
