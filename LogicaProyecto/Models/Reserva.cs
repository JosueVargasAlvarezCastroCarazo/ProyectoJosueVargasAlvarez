using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaProyecto.Models
{
    public class Reserva
    {
        public int IdReserva { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public Articulo MiArticulo { get; set; }
        public Cliente MiCliente { get; set; }

        public Reserva()
        {
            MiArticulo = new Articulo();
            MiCliente = new Cliente();
        }

        //agrega una reserva a la base de datos
        public bool Agregar()
        {
            bool Resultado = false;


            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Inicio", FechaInicio));
            MiCnn.ListaParametros.Add(new SqlParameter("@Fin", FechaFin));
            MiCnn.ListaParametros.Add(new SqlParameter("@Articulo", MiArticulo.IdArticulo));
            MiCnn.ListaParametros.Add(new SqlParameter("@Cliente", MiCliente.IdCliente));

            int Consulta = MiCnn.EjecutarUpdateDeleteInsert("SpReservaGuardar");

            if (Consulta > 0)
            {
                Resultado = true;
            }


            return Resultado;
        }

        //elimina una serverva de manera permanente de la base de datos
        public bool Eliminar()
        {
            bool Resultado = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Id", this.IdReserva));

            int Consulta = MiCnn.EjecutarUpdateDeleteInsert("SpReservaEliminar");

            if (Consulta > 0)
            {
                Resultado = true;
            }

            return Resultado;
        }

        //consulta una reserva por id
        public Reserva ConsultarPorId()
        {
            Reserva Resultado = new Reserva();

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Id", IdReserva));

            DataTable Consulta = MiCnn.EjecutarSelect("SpReservaConsultarId");

            if (Consulta != null && Consulta.Rows.Count > 0)
            {
                DataRow Fila = Consulta.Rows[0];
                Resultado.IdReserva = (int)Fila["IDReserva"];
                Resultado.FechaInicio = (DateTime)Fila["FechaInicio"];
                Resultado.FechaFin = (DateTime)Fila["FechaFin"];
                Resultado.MiArticulo.IdArticulo = (int)Fila["IDArticulo"];
                Resultado.MiCliente.IdCliente = (int)Fila["IDCliente"];
            }

            return Resultado;
        }

        //lista las reservas
        public DataTable Listar(string Busqueda = "")
        {
            DataTable Resultado = new DataTable();


            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@busqueda", Busqueda));

            DataTable Consulta = MiCnn.EjecutarSelect("SpReservaListar");

            if (Consulta != null && Consulta.Rows.Count > 0)
            {
                Resultado = Consulta;
            }

            return Resultado;
        }
    }
}
