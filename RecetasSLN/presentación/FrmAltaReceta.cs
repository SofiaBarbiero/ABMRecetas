using RecetasSLN.datos;
using RecetasSLN.dominio;
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
    public partial class FrmAltaReceta : Form
    {
        private IServicio gestor;
        private Receta nueva;
        public FrmAltaReceta()
        {
            InitializeComponent();
            gestor = new Servicio();
            nueva = new Receta();
        }

        private void FrmAltaReceta_Load(object sender, EventArgs e)
        {
            ObtenerProximo();
            CargarTipos();
            CargarIngredientes();
        }

        private void CargarIngredientes()
        {
            cboIngredientes.DataSource = gestor.ObtenerIngredientes();
            cboIngredientes.ValueMember = "IngredienteId";
            cboIngredientes.DisplayMember = "Nombre";
            cboTipo.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void CargarTipos()
        {
            cboTipo.DataSource = gestor.ObtenerTipos();
            cboTipo.ValueMember = "IdTipo";
            cboTipo.DisplayMember = "NombreTipo";
            cboTipo.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void ObtenerProximo()
        {
            int next = gestor.ObtenerProximo();
            if(next > 0)
            {
                lblNext.Text = "Receta N°: " + next.ToString();
            }
            else
            {
                MessageBox.Show("Error de datos. No se puede obtener Nº de receta!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(cboIngredientes.Text.Equals(string.Empty))
            {
                MessageBox.Show("Debe seleccionar un ingrediente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if(nudCantidad.Value <= 0)
            {
                MessageBox.Show("Debe ingresar una cantidad valida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            foreach(DataGridViewRow row in dgvRecetas.Rows)
            {
                if (row.Cells["ColIngredientes"].Value.ToString().Equals(cboIngredientes.Text))
                {
                    MessageBox.Show("El ingrediente: " + cboIngredientes.Text + " ya se encuentra en la lista",
                                    "Control", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }

            Ingrediente i = (Ingrediente)cboIngredientes.SelectedItem;
            int cantidad = Convert.ToInt32(nudCantidad.Value);

            DetalleReceta d = new DetalleReceta(i, cantidad);

            nueva.AgregarDetalle(d);
            dgvRecetas.Rows.Add(d.Ingrediente.IngredienteId, d.Ingrediente.Nombre, d.Cantidad);

            CalcularIngredientes(); 
        }

        private void CalcularIngredientes()
        {
            int total = nueva.CalcularIngredientes();
            txtTotal.Text = total.ToString();     
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "")
            {
                MessageBox.Show("Tiene que colocar el nombre de la receta para continuar", "Stop",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            }
            if (txtChef.Text == string.Empty)
            {
                MessageBox.Show("Tiene que colocar el Cheff para continuar", "Stop",
                   MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            }
            if(int.Parse(txtTotal.Text)<3)
            {
                MessageBox.Show("Ha olvidado ingredientes?", "Stop",
                   MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            }

            GuardarReceta();
        }
        
        private void GuardarReceta()
        {
            TipoReceta t = (TipoReceta)cboTipo.SelectedItem;
            nueva.Nombre = txtNombre.Text;
            nueva.Cheff = txtChef.Text;
            nueva.TipoReceta = t;

            if(Helper.ObtenerInstancia().ConfirmarReceta(nueva))
            {
                MessageBox.Show("Receta registrada con exito!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("No se pudo registrar la receta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtChef.Text = string.Empty;
            cboTipo.SelectedIndex = -1;
            cboIngredientes.SelectedIndex = -1;
            nudCantidad.Value = 0;
            dgvRecetas.Rows.Clear();
            txtTotal.Text = string.Empty;
        }
    }
}
