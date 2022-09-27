using RecetasSLN.servicio.implementacion;
using RecetasSLN.servicio.interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecetasSLN.presentación
{
    public partial class FrmConsultarRecetas : Form
    {
        private IServicio gestor;
        public FrmConsultarRecetas()
        {
            InitializeComponent();
            gestor = new Servicio();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            new FrmAltaReceta().ShowDialog();
        }

        private void FrmConsultarRecetas_Load(object sender, EventArgs e)
        {
            CargarTipos();
        }

        private void CargarTipos()
        {
            cboTipo.DataSource = gestor.ObtenerTipos();
            cboTipo.ValueMember = "IdTipo";
            cboTipo.DisplayMember = "NombreTipo";
            cboTipo.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
