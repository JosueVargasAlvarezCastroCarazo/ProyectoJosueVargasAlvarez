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
    public partial class FrmDonacion : Form
    {
        //instancias de clases
        Donacion MiDonacion;
        Cliente MiCliente;

        public FrmDonacion()
        {
            InitializeComponent();
        }

        //establece el mdi parent y limpia el fomulario
        private void FrmDonacion_Load(object sender, EventArgs e)
        {
            MdiParent = Globales.MiFormPrincipal;
            Limpiar();
        }

        //limpia el fomrulario
        private void Limpiar()
        {
            TxtMonto.Clear();
            TxtCliente.Clear();
            MiCliente = new Cliente();
            MiDonacion = new Donacion();
            DgvLista.DataSource = MiDonacion.Listar(CboxVerActivos.Checked);
            DgvClientes.DataSource = MiCliente.Listar(true);
            BtnEliminar.Enabled = false;
            BtnEliminar.Text = "Eliminar";
        }

        //valida los campos requeridos
        private bool ValidarCampos()
        {

            if (string.IsNullOrEmpty(TxtMonto.Text.Trim()))
            {
                return false;
            }

            return true;
        }

        //filtra la lista segun una busqueda o si se quiere ver inactivos
        private void TxtBusqueda_TextChanged(object sender, EventArgs e)
        {
            DgvLista.DataSource = MiDonacion.Listar(CboxVerActivos.Checked, TxtBusqueda.Text.Trim());
        }

        private void CboxVerActivos_CheckedChanged(object sender, EventArgs e)
        {
            DgvLista.DataSource = MiDonacion.Listar(CboxVerActivos.Checked, TxtBusqueda.Text.Trim());
        }

        //guarda y actualiza una donacion
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {

                if (MiDonacion.IdDonacion == 0)
                {
                    MiDonacion = new Donacion();
                    MiDonacion.MontoDonacion = double.Parse(TxtMonto.Text.Trim());

                    if (MiCliente.IdCliente > 0)
                    {
                        MiDonacion.Cliente.IdCliente = MiCliente.IdCliente;
                    }
                   
                    MiDonacion.Agregar();
                    Limpiar();
                }
                else
                {

                    MiDonacion.MontoDonacion = double.Parse(TxtMonto.Text.Trim());

                    if (MiCliente.IdCliente > 0)
                    {
                        MiDonacion.Cliente.IdCliente = MiCliente.IdCliente;
                    }

                    if (MiDonacion.ConsultarPorId().IdDonacion > 0)
                    {
                        MiDonacion.Modificar();
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("Esta donacion ya no existe", "Mensaje", MessageBoxButtons.OK);
                        Limpiar();
                    }


                }
            }
            else
            {
                MessageBox.Show("Debe ingresar todos los espacios", "Mensaje", MessageBoxButtons.OK);
            }
        }

        //elimina una donacion
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (MiDonacion.IdDonacion != 0)
            {

                if (MiDonacion.ConsultarPorId().IdDonacion > 0)
                {
                    MiDonacion.Eliminar();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("Esta donacion ya no existe", "Mensaje", MessageBoxButtons.OK);
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

        //accion de cancelar
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //limpia el campo relacionado al cliente ya que se puede guardar una donacion sin cliente
        private void BtnLimpiarCliente_Click(object sender, EventArgs e)
        {
            MiCliente = new Cliente();
            MiDonacion.Cliente.IdCliente = 0;
            TxtCliente.Clear();
        }

        //se carga una donacion para editarla o eliminarla
        private void DgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvLista.SelectedRows.Count == 1)
            {
                DataGridViewRow MiFila = DgvLista.SelectedRows[0];
                int Id = (int)MiFila.Cells["Id"].Value;
                string ClienteNombre = (string)MiFila.Cells["Cliente"].Value;
                Limpiar();
                MiDonacion = new Donacion();
                MiDonacion.IdDonacion = Id;
                MiDonacion = MiDonacion.ConsultarPorId();

                if (MiDonacion != null && MiDonacion.IdDonacion > 0)
                {
                    TxtMonto.Text = ((int)MiDonacion.MontoDonacion).ToString();

                    if (MiDonacion.Cliente.IdCliente > 0)
                    {
                        TxtCliente.Text = ClienteNombre;
                    }
          
                    BtnEliminar.Enabled = true;

                    if (!MiDonacion.Activo)
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

        //carga un cliente para poder asignarlo a una donacion
        private void DgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvClientes.SelectedRows.Count == 1)
            {
                DataGridViewRow MiFila = DgvClientes.SelectedRows[0];
                int Id = (int)MiFila.Cells["Id"].Value;
                MiCliente = new Cliente();
                MiCliente.IdCliente = Id;
                MiCliente = MiCliente.ConsultarPorId();

                if (MiCliente != null && MiCliente.IdCliente > 0)
                {
                    TxtCliente.Text = MiCliente.Nombre;
                }

            }
        }


        //limpia la seleccion de las listas
        private void DgvClientes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DgvClientes.ClearSelection();
        }

        private void DgvLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DgvLista.ClearSelection();
        }

        //valida que solo permita numeros
        private void TxtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        //filtra la lista de clientes
        private void TxtBuscarCliente_TextChanged(object sender, EventArgs e)
        {
            DgvClientes.DataSource = MiCliente.Listar(true, TxtBuscarCliente.Text.Trim());
        }
    }
}
