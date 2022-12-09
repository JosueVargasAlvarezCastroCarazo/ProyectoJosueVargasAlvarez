using LogicaProyecto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoJosueVargasAlvarez.Formularios
{
    public partial class FrmArticulo : Form
    {

        //instancias de objetos
        Categoria MiCategoria;
        Articulo MiArticulo;


        public FrmArticulo()
        {
            InitializeComponent();
        }

        //inicio de formulario y limpieza
        private void FrmArticulo_Load(object sender, EventArgs e)
        {
            MdiParent = Globales.MiFormPrincipal;
            MiCategoria = new Categoria();
            Limpiar();
        }

        //metodo de limpieza de formulario
        private void Limpiar()
        {
            TxtCodigo.Clear();
            TxtNombre.Clear();
            TxtDescripcion.Clear();
            MiArticulo = new Articulo();
            DgvLista.DataSource = MiArticulo.Listar(CboxVerActivos.Checked);
            BtnEliminar.Enabled = false;
            BtnEliminar.Text = "Eliminar";

            DataTable Data = MiCategoria.Listar();

            if (Data != null && Data.Rows.Count > 0)
            {
                CboxCategoria.ValueMember = "Id";
                CboxCategoria.DisplayMember = "Nombre";
                CboxCategoria.DataSource = Data;
                CboxCategoria.SelectedIndex = -1;
            }

        }


        //valida los campos requeridos
        private bool ValidarCampos()
        {

            if (string.IsNullOrEmpty(TxtCodigo.Text.Trim()) ||
                string.IsNullOrEmpty(TxtNombre.Text.Trim()) ||
                string.IsNullOrEmpty(TxtDescripcion.Text.Trim()) ||
                CboxCategoria.SelectedIndex == -1)
            {
                return false;
            }

            return true;
        }

        //guarda un articulo o lo actualza segun el codigo
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {

                if (MiArticulo.IdArticulo == 0)
                {
                    MiArticulo = new Articulo();
                    MiArticulo.Codigo = TxtCodigo.Text.Trim();
                    MiArticulo.NombreArticulo = TxtNombre.Text.Trim();
                    MiArticulo.MiCategoria.IdCategoria = (int)CboxCategoria.SelectedValue;
                    MiArticulo.Descripcion = TxtDescripcion.Text.Trim();

                    if (!MiArticulo.ConsultarPorCodigo())
                    {
                        MiArticulo.Agregar();
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("Ya existe un Articulo con este codigo", "Mensaje", MessageBoxButtons.OK);
                    }

                }
                else
                {

                    MiArticulo.Codigo = TxtCodigo.Text.Trim();
                    MiArticulo.NombreArticulo = TxtNombre.Text.Trim();
                    MiArticulo.MiCategoria.IdCategoria = (int)CboxCategoria.SelectedValue;
                    MiArticulo.Descripcion = TxtDescripcion.Text.Trim();

                    if (MiArticulo.ConsultarPorId().IdArticulo > 0)
                    {
                        MiArticulo.Modificar();
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("Este Articulo ya no existe", "Mensaje", MessageBoxButtons.OK);
                        Limpiar();
                    }


                }
            }
            else
            {
                MessageBox.Show("Debe ingresar todos los espacios", "Mensaje", MessageBoxButtons.OK);
            }
        }

        //elimina un articulo verificando que aun exista en la base
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (MiArticulo.IdArticulo != 0)
            {

                if (MiArticulo.ConsultarPorId().IdArticulo > 0)
                {
                    MiArticulo.Eliminar();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("Este Articulo ya no existe", "Mensaje", MessageBoxButtons.OK);
                    Limpiar();
                }


            }
            else
            {
                MessageBox.Show("Debe seleccionar un elemento de la lista", "Mensaje", MessageBoxButtons.OK);
            }
        }

        //accion de limpiar
        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        //accion de cerrar
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //carga de data source para busquedas
        private void CboxVerActivos_CheckedChanged(object sender, EventArgs e)
        {
            DgvLista.DataSource = MiArticulo.Listar(CboxVerActivos.Checked, TxtBusqueda.Text.Trim());
        }

        //carga de data source para busquedas
        private void TxtBusqueda_TextChanged(object sender, EventArgs e)
        {
            DgvLista.DataSource = MiArticulo.Listar(CboxVerActivos.Checked, TxtBusqueda.Text.Trim());
        }


        //limpieza de seleccion
        private void DgvLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DgvLista.ClearSelection();
        }

        //cuando se toca un elemento de la lista este se carga para poder actualizarlo
        private void DgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvLista.SelectedRows.Count == 1)
            {
                DataGridViewRow MiFila = DgvLista.SelectedRows[0];
                int Id = (int)MiFila.Cells["Id"].Value;
                Limpiar();
                MiArticulo = new Articulo();
                MiArticulo.IdArticulo = Id;
                MiArticulo = MiArticulo.ConsultarPorId();

                if (MiArticulo != null && MiArticulo.IdArticulo > 0)
                {
                    TxtCodigo.Text = MiArticulo.Codigo;
                    TxtNombre.Text = MiArticulo.NombreArticulo;
                    TxtDescripcion.Text = MiArticulo.Descripcion;
                    CboxCategoria.SelectedValue = MiArticulo.MiCategoria.IdCategoria;

                    BtnEliminar.Enabled = true;

                    if (!MiArticulo.Activo)
                    {
                        BtnEliminar.Text = "Restaurar";
                    }
                    else
                    {
                        BtnEliminar.Text = "Eliminar";
                    }


                }

            }
        }
    }
}
