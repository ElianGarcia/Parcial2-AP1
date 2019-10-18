using Parcial2_AP1.UI.Consultas;
using Parcial2_AP1.UI.Registros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial2_AP1
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rCategorias registroCategorias = new rCategorias();
            registroCategorias.ShowDialog();
        }

        private void registroToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            rRegistro registroPricipal = new rRegistro();
            registroPricipal.ShowDialog();
        }

        private void consultaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cConsulta consulta = new cConsulta();
            consulta.MdiParent = this;
            consulta.Show();
        }
    }
}
