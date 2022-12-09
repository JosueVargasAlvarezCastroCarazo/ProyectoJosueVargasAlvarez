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
    public partial class FrmCliente : Form
    {

        //instancia de cliente
        Cliente MiCliente;

        public FrmCliente()
        {
            InitializeComponent();
        }

        //limpia y establece el mdi parent
        private void FrmCliente_Load(object sender, EventArgs e)
        {
            MdiParent = Globales.MiFormPrincipal;
            Limpiar();
        }


        //limpia el formulario
        private void Limpiar()
        {
            TxtCedula.Clear();
            TxtNombre.Clear();
            TxtTelefono.Clear();
            TxtDireccion.Clear();
            MiCliente = new Cliente();
            DgvLista.DataSource = MiCliente.Listar(CboxVerActivos.Checked);
            BtnEliminar.Enabled = false;
            BtnEliminar.Text = "Eliminar";
        }

        //valida los campos
        private bool ValidarCampos()
        {

            if (string.IsNullOrEmpty(TxtCedula.Text.Trim()) ||
                string.IsNullOrEmpty(TxtNombre.Text.Trim()) ||
                string.IsNullOrEmpty(TxtTelefono.Text.Trim()) ||
                string.IsNullOrEmpty(TxtDireccion.Text.Trim()))
            {
                return false;
            }

            return true;
        }

        // filtra la lista de clientes
        private void TxtBusqueda_TextChanged(object sender, EventArgs e)
        {
            DgvLista.DataSource = MiCliente.Listar(CboxVerActivos.Checked, TxtBusqueda.Text.Trim());
        }

        // filtra la lista de clientes
        private void CboxVerActivos_CheckedChanged(object sender, EventArgs e)
        {
            DgvLista.DataSource = MiCliente.Listar(CboxVerActivos.Checked, TxtBusqueda.Text.Trim());
        }


        //guarda o actualiza un cliente en la base de datos
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {

                if (MiCliente.IdCliente == 0)
                {
                    MiCliente = new Cliente();
                    MiCliente.Cedula = TxtCedula.Text.Trim();
                    MiCliente.Nombre = TxtNombre.Text.Trim();
                    MiCliente.Telefono = TxtTelefono.Text.Trim();
                    MiCliente.Direccion = TxtDireccion.Text.Trim();

                    if (!MiCliente.ConsultarPorCedula())
                    {
                        MiCliente.Agregar();
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("Ya existe un cliente con esta cedula", "Mensaje", MessageBoxButtons.OK);
                    }

                }
                else
                {

                    MiCliente.Cedula = TxtCedula.Text.Trim();
                    MiCliente.Nombre = TxtNombre.Text.Trim();
                    MiCliente.Telefono = TxtTelefono.Text.Trim();
                    MiCliente.Direccion = TxtDireccion.Text.Trim();

                    if (MiCliente.ConsultarPorId().IdCliente > 0)
                    {
                        MiCliente.Modificar();
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("Este cliente ya no existe", "Mensaje", MessageBoxButtons.OK);
                        Limpiar();
                    }


                }
            }
            else
            {
                MessageBox.Show("Debe ingresar todos los espacios", "Mensaje", MessageBoxButtons.OK);
            }
        }

        //elimina un cliente si este existe ne la base de datos
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (MiCliente.IdCliente != 0)
            {

                if (MiCliente.ConsultarPorId().IdCliente > 0)
                {
                    MiCliente.Eliminar();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("Este cliente ya no existe", "Mensaje", MessageBoxButtons.OK);
                    Limpiar();
                }


            }
            else
            {
                MessageBox.Show("Debe seleccionar un elemento de la lista", "Mensaje", MessageBoxButtons.OK);
            }
        }

        //limpia el formulario
        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }


        //accion de cancelar
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //limpia la seleccion de la lista
        private void DgvLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DgvLista.ClearSelection();
        }

        //carga un elemento de la lista para actualizarlo o eliminarlo
        private void DgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvLista.SelectedRows.Count == 1)
            {
                DataGridViewRow MiFila = DgvLista.SelectedRows[0];
                int Id = (int)MiFila.Cells["Id"].Value;
                Limpiar();
                MiCliente = new Cliente();
                MiCliente.IdCliente = Id;
                MiCliente = MiCliente.ConsultarPorId();

                if (MiCliente != null && MiCliente.IdCliente > 0)
                {
                    TxtCedula.Text = MiCliente.Cedula;
                    TxtNombre.Text = MiCliente.Nombre;
                    TxtTelefono.Text = MiCliente.Telefono;
                    TxtDireccion.Text = MiCliente.Direccion;

                    BtnEliminar.Enabled = true;

                    if (!MiCliente.Activo)
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

        //establece que solo se pueden utilizar numeros en el input
        private void TxtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        private void TxtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }
    }
}
