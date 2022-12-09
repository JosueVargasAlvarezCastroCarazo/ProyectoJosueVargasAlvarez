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
    public partial class FrmPrincipalMdi : Form
    {
        public FrmPrincipalMdi()
        {
            InitializeComponent();
            Globales.MiFormPrincipal = this;
        }


       

        private void FrmPrincipalMdi_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        //cada metodo se encarga de levantar una ventana segun la opcion del menu que sea seleccionada


        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Globales.MiFormCliente.Visible)
            {
                Globales.MiFormCliente = new Formularios.FrmCliente();
                Globales.MiFormCliente.Show();
            }
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Globales.MiFormCategorias.Visible)
            {
                Globales.MiFormCategorias = new Formularios.FrmCategoria();
                Globales.MiFormCategorias.Show();
            }
        }

        private void articulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Globales.MiFormArticulo.Visible)
            {
                Globales.MiFormArticulo = new Formularios.FrmArticulo();
                Globales.MiFormArticulo.Show();
            }
        }

        private void donacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Globales.MiFormDonacion.Visible)
            {
                Globales.MiFormDonacion = new Formularios.FrmDonacion();
                Globales.MiFormDonacion.Show();
            }
            
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Globales.MiFormAcerca.Visible)
            {
                Globales.MiFormAcerca = new Formularios.FrmInfo();
                Globales.MiFormAcerca.Show();
            }
            
        }

        private void reservasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Globales.MiFormReserva.Visible)
            {
                Globales.MiFormReserva = new Formularios.FrmReserva();
                Globales.MiFormReserva.Show();
            }
        }

        private void donacionesPorFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Globales.MiFormDonacionesReporte.Visible)
            {
                Globales.MiFormDonacionesReporte = new Formularios.FrmDonacionesPorFecha();
                Globales.MiFormDonacionesReporte.Show();
            }
        }
    }
}
