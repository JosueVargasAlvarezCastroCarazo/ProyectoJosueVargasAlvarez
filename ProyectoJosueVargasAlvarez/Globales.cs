using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoJosueVargasAlvarez
{
    public static class Globales
    {
        public static Form MiFormAcerca = new Formularios.FrmInfo();
        public static Form MiFormPrincipal= new Formularios.FrmPrincipalMdi();
        public static Form MiFormCategorias = new Formularios.FrmCategoria();
        public static Form MiFormCliente = new Formularios.FrmCliente();
        public static Form MiFormArticulo = new Formularios.FrmArticulo();
        public static Form MiFormDonacion = new Formularios.FrmDonacion();
        public static Form MiFormReserva = new Formularios.FrmReserva();
        public static Form MiFormDonacionesReporte = new Formularios.FrmDonacionesPorFecha();
    }
}
