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
    public partial class FrmDonacionesPorFecha : Form
    {

        Donacion MiDonacion;

        public FrmDonacionesPorFecha()
        {
            InitializeComponent();
        }

        private void DtpInicio_ValueChanged(object sender, EventArgs e)
        {
            if (DtpInicio.Value > DtpFin.Value)
            {
                DtpFin.Value = DtpInicio.Value;
            }

      
        }

        private void DtpFin_ValueChanged(object sender, EventArgs e)
        {
            if (DtpInicio.Value > DtpFin.Value)
            {
                DtpInicio.Value = DtpFin.Value;
            }

        
        }

        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            DgvLista.DataSource = MiDonacion.VerificarDonacionPorFechaListar(DtpInicio.Value, DtpFin.Value);
        }

        private void FrmDonacionesPorFecha_Load(object sender, EventArgs e)
        {
            MdiParent = Globales.MiFormPrincipal;
            MiDonacion = new Donacion();
        }
    }
}
