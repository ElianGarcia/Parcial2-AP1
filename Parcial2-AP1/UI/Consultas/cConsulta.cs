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
            var listado = new List<ServiciosDetalle>();
            GenericaBLL<Servicios> ServiciosBLL = new GenericaBLL<Servicios>();

            if (tbCriterio.Text.Trim().Length > 0)
            {
                switch (cbFiltrar.SelectedIndex)
                {
                    case 0:
                        listado = GenericaBLL.GetList(p => true);
                        break;
                    case 1:
                        int id = Convert.ToInt32(tbCriterio.Text);
                        listado = ServiciosBLL.GetList(servicio => servicio.ServiciosID == id);
                        break;
                }

                listado = listado.Where(c => c.Fecha.Date >= DesdeDateTimePicker.Value.Date && c.Fecha.Date <= HastaDateTimePicker.Value.Date).ToList();
            }
            else
            {
                listado = AsistenciasBLL.GetList(p => true);
            }

            ConsultaDataGridView.DataSource = null;
            ConsultaDataGridView.DataSource = listado;
        }
    }
}
