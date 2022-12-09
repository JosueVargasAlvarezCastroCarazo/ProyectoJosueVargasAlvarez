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
    public partial class FrmReserva : Form
    {

        //instancias necesarias para realizar una reserva
        Reserva MiReserva;
        Cliente MiCliente;
        Articulo MiArticulo;

        public FrmReserva()
        {
            InitializeComponent();
        }

        //establece el mdi y limpia el formulario
        private void FrmReserva_Load(object sender, EventArgs e)
        {
            MdiParent = Globales.MiFormPrincipal;
            Limpiar();
        }

        //metodo encargado de limpiar el fomulario
        private void Limpiar()
        {
            TxtCliente.Clear();
            TxtArticulo.Clear();
            MiCliente = new Cliente();
            MiReserva = new Reserva();
            MiArticulo = new Articulo();
            DgvLista.DataSource = MiReserva.Listar();
            DgvClientes.DataSource = MiCliente.Listar(true);
            DtpInicio.Value = DateTime.Today;
            DtpFin.Value = DateTime.Today.AddDays(1);
            DgvArticulos.DataSource = MiArticulo.VerificarReservaListar(DtpInicio.Value, DtpFin.Value);
            BtnEliminar.Enabled = false;
            BtnGuardar.Enabled = true;
        }

        //valida los campos necesarios para el formulario
        private bool ValidarCampos()
        {

            if (string.IsNullOrEmpty(TxtCliente.Text.Trim()) ||
                string.IsNullOrEmpty(TxtArticulo.Text.Trim()))
            {
                return false;
            }

            return true;
        }

        //guarda una reserva
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {

                MiReserva = new Reserva();
                MiReserva.MiArticulo.IdArticulo = MiArticulo.IdArticulo;
                MiReserva.MiCliente.IdCliente = MiCliente.IdCliente;
                MiReserva.FechaInicio = DtpInicio.Value;
                MiReserva.FechaFin = DtpFin.Value;

                MiReserva.Agregar();
                Limpiar();
                
            }
            else
            {
                MessageBox.Show("Debe ingresar todos los espacios", "Mensaje", MessageBoxButtons.OK);
            }
        }

        //elimina una reserva si esta existe en base de datos
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (MiReserva.IdReserva != 0)
            {

                if (MiReserva.ConsultarPorId().IdReserva > 0)
                {
                    MiReserva.Eliminar();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("Esta Reserva ya no existe", "Mensaje", MessageBoxButtons.OK);
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

        //filtra la lista de reservas
        private void TxtBusqueda_TextChanged(object sender, EventArgs e)
        {
            DgvLista.DataSource = MiReserva.Listar(TxtBusqueda.Text.Trim());
        }

        //carga una reserva para poder eliminarla
        private void DgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvLista.SelectedRows.Count == 1)
            {
                DataGridViewRow MiFila = DgvLista.SelectedRows[0];
                int Id = (int)MiFila.Cells["Id"].Value;
                string ClienteNombre = (string)MiFila.Cells["Cliente"].Value;
                string ArticuloNombre = (string)MiFila.Cells["Articulo"].Value;
                Limpiar();
                MiReserva = new Reserva();
                MiReserva.IdReserva = Id;
                MiReserva = MiReserva.ConsultarPorId();

                if (MiReserva != null && MiReserva.IdReserva > 0)
                {

                    TxtCliente.Text = ClienteNombre;
                    TxtArticulo.Text = ArticuloNombre;
                    DtpInicio.Value = MiReserva.FechaInicio;
                    DtpFin.Value = MiReserva.FechaFin;

                    BtnEliminar.Enabled = true;
                    BtnGuardar.Enabled = false;

                }

            }
        }

        // filtra la lista de articulos
        private void TxtBuscarArticulos_TextChanged(object sender, EventArgs e)
        {
            DgvArticulos.DataSource = MiArticulo.VerificarReservaListar(DtpInicio.Value, DtpFin.Value, TxtBuscarArticulos.Text.Trim());
        }

        //carga un articulo para servervarlo

        private void DgvArticulos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvArticulos.SelectedRows.Count == 1)
            {
                DataGridViewRow MiFila = DgvArticulos.SelectedRows[0];
                int Id = (int)MiFila.Cells["Id"].Value;
                MiArticulo = new Articulo();
                MiArticulo.IdArticulo = Id;
                MiArticulo = MiArticulo.ConsultarPorId();

                if (MiArticulo != null && MiArticulo.IdArticulo > 0)
                {
                    TxtArticulo.Text = MiArticulo.NombreArticulo;
                }

            }
        }

        //filtra la lista de clientes
        private void TxtBuscarCliente_TextChanged(object sender, EventArgs e)
        {
            DgvClientes.DataSource = MiCliente.Listar(true, TxtBuscarCliente.Text.Trim());
        }

        //carga un cliente el cual realiza la reserva de un articulo
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


        //limpia la seleccion de carga inicial
        private void DgvArticulos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DgvArticulos.ClearSelection();
        }

        private void DgvClientes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DgvClientes.ClearSelection();
        }

        private void DgvLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DgvLista.ClearSelection();
        }

        //verifica que las fecha no puedan tener un orden alterado
        private void DtpInicio_ValueChanged(object sender, EventArgs e)
        {

            if (DtpInicio.Value > DtpFin.Value)
            {
                DtpFin.Value = DtpInicio.Value;
            }

            if (DtpInicio.Value < DateTime.Today)
            {
                DtpInicio.Value = DateTime.Today;
                DtpFin.Value = DateTime.Today;
            }

            DgvArticulos.DataSource = MiArticulo.VerificarReservaListar(DtpInicio.Value, DtpFin.Value);
        }

        //verifica que las fecha no puedan tener un orden alterado
        private void DtpFin_ValueChanged(object sender, EventArgs e)
        {

            if (DtpInicio.Value > DtpFin.Value)
            {
                DtpInicio.Value = DtpFin.Value;
            }

            if (DtpFin.Value < DateTime.Today)
            {
                DtpInicio.Value = DateTime.Today;
                DtpFin.Value = DateTime.Today;
            }

            DgvArticulos.DataSource = MiArticulo.VerificarReservaListar(DtpInicio.Value, DtpFin.Value);
        }
    }
}
