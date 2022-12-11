using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaProyecto.Models
{
    public class Donacion
    {

        public int IdDonacion { get; set; }
        public double MontoDonacion { get; set; }
        public bool Activo { get; set; }

        public Cliente Cliente { get; set; }

        public Donacion()
        {
            Cliente = new Cliente();
        }

        //agrega una donacion a la base de datos
        public bool Agregar()
        {

            bool Resultado = false;


            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Monto", MontoDonacion));

            //verifica si hay cliente si no no lo guarda
            if (Cliente.IdCliente > 0)
            {
                MiCnn.ListaParametros.Add(new SqlParameter("@Cliente", Cliente.IdCliente));
            }

            int Consulta = MiCnn.EjecutarUpdateDeleteInsert("SpDonacionGuardar");

            if (Consulta > 0)
            {
                Resultado = true;
            }


            return Resultado;
        }

        //modifica una donacion de la base de datos
        public bool Modificar()
        {
            bool Resultado = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Id", IdDonacion));
            MiCnn.ListaParametros.Add(new SqlParameter("@Monto", MontoDonacion));

            //verifica si hay cliente si no no lo guarda
            if (Cliente.IdCliente > 0)
            {
                MiCnn.ListaParametros.Add(new SqlParameter("@Cliente", Cliente.IdCliente));
            }

            int Consulta = MiCnn.EjecutarUpdateDeleteInsert("SpDonacionModificar");

            if (Consulta > 0)
            {
                Resultado = true;
            }

            return Resultado;
        }

        //elimina una donacion de la base ded atos
        public bool Eliminar()
        {
            bool Resultado = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Id", this.IdDonacion));

            int Consulta = MiCnn.EjecutarUpdateDeleteInsert("SpDonacionEliminar");

            if (Consulta > 0)
            {
                Resultado = true;
            }

            return Resultado;
        }

        //consulta una donacion por id
        public Donacion ConsultarPorId()
        {
            Donacion Resultado = new Donacion();

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Id", IdDonacion));

            DataTable Consulta = MiCnn.EjecutarSelect("SpDonacionConsultarId");

            if (Consulta != null && Consulta.Rows.Count > 0)
            {
                DataRow Fila = Consulta.Rows[0];
                Resultado.IdDonacion = (int)Fila["IDDonacion"];
                Resultado.MontoDonacion = double.Parse(Fila["MontoDonacion"].ToString());
                Resultado.Activo = (bool)Fila["Activo"];

                if (Fila["IDCliente"] != null && !string.IsNullOrEmpty(Fila["IDCliente"].ToString()))
                {
                    Resultado.Cliente.IdCliente = (int)Fila["IDCliente"];
                }

                
            }

            return Resultado;
        }

        //lista las donaciones de la base de datos
        public DataTable Listar(bool VerActivos = false, string Busqueda = "")
        {
            DataTable Resultado = new DataTable();


            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@activos", VerActivos));
            MiCnn.ListaParametros.Add(new SqlParameter("@busqueda", Busqueda));

            DataTable Consulta = MiCnn.EjecutarSelect("SpDonacionListar");

            if (Consulta != null && Consulta.Rows.Count > 0)
            {
                Resultado = Consulta;
            }

            return Resultado;
        }

        //lista las Donaciones segun fecha
        public DataTable VerificarDonacionPorFechaListar(DateTime Inicio, DateTime Fin)
        {
            DataTable Resultado = new DataTable();


            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Inicio", Inicio));
            MiCnn.ListaParametros.Add(new SqlParameter("@Fin", Fin));

            DataTable Consulta = MiCnn.EjecutarSelect("SpDonacionFechasListar");

            if (Consulta != null && Consulta.Rows.Count > 0)
            {
                Resultado = Consulta;
            }

            return Resultado;
        }

    }
}
