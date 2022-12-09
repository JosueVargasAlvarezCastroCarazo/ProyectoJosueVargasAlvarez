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

            if (DtpInicio.Value < DateTime.Today)
            {
                DtpInicio.Value = DateTime.Today;
                DtpFin.Value = DateTime.Today;
            }
        }

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
        }

        private void BtnGenerar_Click(object sender, EventArgs e)
        {

        }

        private void FrmDonacionesPorFecha_Load(object sender, EventArgs e)
        {
            MdiParent = Globales.MiFormPrincipal;
        }
    }
}
