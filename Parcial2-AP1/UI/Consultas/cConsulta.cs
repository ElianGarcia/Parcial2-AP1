using Parcial2_AP1.BLL;
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

namespace Parcial2_AP1.UI.Consultas
{
    public partial class cConsulta : Form
    {
        public cConsulta()
        {
            InitializeComponent();
        }

        private void Consultarbutton_Click(object sender, EventArgs e)
        {
            var listado = new List<Servicios>();
            GenericaBLL<Servicios> genericaBLL = new GenericaBLL<Servicios>();

            if (tbCriterio.Text.Trim().Length > 0)
            {
                switch (cbFiltrar.SelectedIndex)
                {
                    case 0:
                        listado = genericaBLL.GetList(p => true);
                        break;
                    case 1:
                        int id = Convert.ToInt32(tbCriterio.Text);
                        listado = genericaBLL.GetList(servicio => servicio.ServiciosID == id);
                        break;
                    case 2: 
                        string nombre = tbCriterio.Text;
                        listado = genericaBLL.GetList(p => p.Estudiante == nombre);
                        break;
                    case 3: 
                        decimal total = Convert.ToDecimal(tbCriterio.Text);
                        listado = genericaBLL.GetList(p => p.Total == total);
                        break;
                }

                listado = listado.Where(c => c.Fecha.Date >= DesdeDateTimePicker.Value.Date && c.Fecha.Date <= HastaDateTimePicker.Value.Date).ToList();
            }
            else
            {
                listado = genericaBLL.GetList(p => true);
            }

            ConsultaDataGridView.DataSource = null;
            ConsultaDataGridView.DataSource = listado;
        }
    }
}
