using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Data;
using System.Windows.Forms;
using CapaControlador;

namespace CapaVista
{
    public partial class Frm_Facultades : Form
    {
        private readonly ControladorFacultades _ctrl = new ControladorFacultades();

        public Frm_Facultades() 
        { 
            InitializeComponent(); 
        }

        private void Frm_Facultades_Load(object sender, EventArgs e) => RefrescarGrid();

        private void RefrescarGrid()
        {
            try
            {
                DataTable dt = _ctrl.ObtenerLista();
                DgvDatos.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e) => Limpiar();

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!_ctrl.Guardar(TxtCodigo.Text, TxtNombre.Text, TxtEstatus.Text, out string error))
            { MessageBox.Show("No se pudo guardar: " + error); return; }
            MessageBox.Show("Guardado.");
            RefrescarGrid(); Limpiar();
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (!_ctrl.Actualizar(TxtCodigo.Text, TxtNombre.Text, TxtEstatus.Text, out string error))
            { MessageBox.Show("No se pudo actualizar: " + error); return; }
            MessageBox.Show("Actualizado.");
            RefrescarGrid(); Limpiar();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtCodigo.Text)) { MessageBox.Show("Ingrese código."); return; }
            if (MessageBox.Show("¿Eliminar registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            if (!_ctrl.Eliminar(TxtCodigo.Text, out string error))
            { MessageBox.Show("No se pudo eliminar: " + error); return; }
            MessageBox.Show("Eliminado.");
            RefrescarGrid(); Limpiar();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            var f = _ctrl.Buscar(TxtCodigo.Text);
            if (f == null) { MessageBox.Show("No encontrado."); return; }
            TxtCodigo.Text = f.Codigo;
            TxtNombre.Text = f.Nombre;
            TxtEstatus.Text = f.Estatus;
        }

        private void BtnLimpiar_Click(object sender, EventArgs e) => Limpiar();

        private void Limpiar()
        {
            TxtCodigo.Clear(); TxtNombre.Clear(); TxtEstatus.Clear();
            TxtCodigo.Focus();
        }

        private void DgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = DgvDatos.Rows[e.RowIndex];
            TxtCodigo.Text = row.Cells["codigo_facultad"].Value?.ToString();
            TxtNombre.Text = row.Cells["nombre_facultad"].Value?.ToString();
            TxtEstatus.Text = row.Cells["estatus_facultad"].Value?.ToString();
        }
    }
}

