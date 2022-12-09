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
    public partial class FrmCategoria : Form
    {

        //instancia de objeto
        Categoria MiCategoria;

        public FrmCategoria()
        {
            InitializeComponent();
        }

        //se establece el parent y se limpia el formulario
        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            MdiParent = Globales.MiFormPrincipal;
            Limpiar();
        }

        //accion de cancelar
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //accion de busqueda
        private void TxtBusqueda_TextChanged(object sender, EventArgs e)
        {
            DgvLista.DataSource = MiCategoria.Listar(CboxVerActivos.Checked, TxtBusqueda.Text.Trim());
        }


        //accion de ver activos
        private void CboxVerActivos_CheckedChanged(object sender, EventArgs e)
        {
            DgvLista.DataSource = MiCategoria.Listar(CboxVerActivos.Checked, TxtBusqueda.Text.Trim());
        }


        //accion de limpiar
        private void Limpiar()
        {
            TxtNombre.Clear();
            MiCategoria = new Categoria();
            DgvLista.DataSource = MiCategoria.Listar(CboxVerActivos.Checked);
            BtnEliminar.Enabled = false;
            BtnEliminar.Text = "Eliminar";
        }


        //valida los campos requeridos
        private bool ValidarCampos()
        {

            if (string.IsNullOrEmpty(TxtNombre.Text.Trim()))
            {
                return false;
            }

            return true;
        }

        //guarda o actualiza una categoria
        private void BtnGuardar_Click(object sender, EventArgs e)
        {

            if (ValidarCampos())
            {

                if (MiCategoria.IdCategoria == 0)
                {
                    MiCategoria = new Categoria();
                    MiCategoria.NombreCategoria = TxtNombre.Text.Trim();

                    if (!MiCategoria.ConsultarPorNombre())
                    {
                        MiCategoria.Agregar();
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("Ya existe una categoria con este nombre", "Mensaje", MessageBoxButtons.OK);
                    }

                }else {

                    MiCategoria.NombreCategoria = TxtNombre.Text.Trim();

                    if (MiCategoria.ConsultarPorId().IdCategoria > 0)
                    {
                        MiCategoria.Modificar();
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("Esta categoria ya no existe", "Mensaje", MessageBoxButtons.OK);
                        Limpiar();
                    }


                }



            }
            else
            {
                MessageBox.Show("Debe ingresar todos los espacios", "Mensaje", MessageBoxButtons.OK);
            }

           
        }

        //elimina una categoria si esta existe
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (MiCategoria.IdCategoria != 0)
            {

                if (MiCategoria.ConsultarPorId().IdCategoria > 0)
                {
                    MiCategoria.Eliminar();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("Esta categoria ya no existe", "Mensaje", MessageBoxButtons.OK);
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

        //carga una categoria cuando se selecciona de la lista para modificarla
        private void DgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvLista.SelectedRows.Count == 1)
            {
                DataGridViewRow MiFila = DgvLista.SelectedRows[0];
                int IdCategoria = (int)MiFila.Cells["Id"].Value;
                Limpiar();
                MiCategoria = new Categoria();
                MiCategoria.IdCategoria = IdCategoria;
                MiCategoria = MiCategoria.ConsultarPorId();

                if (MiCategoria != null && MiCategoria.IdCategoria > 0)
                {
                    TxtNombre.Text = MiCategoria.NombreCategoria;
                    BtnEliminar.Enabled = true;

                    if (!MiCategoria.Activo)
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

        //elimina la seleccion
        private void DgvLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DgvLista.ClearSelection();
        }
    }
}
