using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaProyecto.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }

        public List<Reserva> MisReservas { get; set; }
        public List<Donacion> MisDonaciones { get; set; }

        public Cliente()
        {
            MisReservas = new List<Reserva>();
            MisDonaciones = new List<Donacion>();
        }


        //guarda un cliente en la base de datos
        public bool Agregar()
        {
            bool Resultado = false;


            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Cedula", Cedula));
            MiCnn.ListaParametros.Add(new SqlParameter("@Nombre", Nombre));
            MiCnn.ListaParametros.Add(new SqlParameter("@Telefono", Telefono));
            MiCnn.ListaParametros.Add(new SqlParameter("@Direccion", Direccion));

            int Consulta = MiCnn.EjecutarUpdateDeleteInsert("SpClienteGuardar");

            if (Consulta > 0)
            {
                Resultado = true;
            }


            return Resultado;
        }

        //modifica un cliente en la base de datos
        public bool Modificar()
        {
            bool Resultado = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Id", IdCliente));
            MiCnn.ListaParametros.Add(new SqlParameter("@Cedula", Cedula));
            MiCnn.ListaParametros.Add(new SqlParameter("@Nombre", Nombre));
            MiCnn.ListaParametros.Add(new SqlParameter("@Telefono", Telefono));
            MiCnn.ListaParametros.Add(new SqlParameter("@Direccion", Direccion));

            int Consulta = MiCnn.EjecutarUpdateDeleteInsert("SpClienteModificar");

            if (Consulta > 0)
            {
                Resultado = true;
            }

            return Resultado;
        }
        
        //elimina o restaura un cliente de la base de datos
        public bool Eliminar()
        {
            bool Resultado = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Id", this.IdCliente));

            int Consulta = MiCnn.EjecutarUpdateDeleteInsert("SpClienteEliminar");

            if (Consulta > 0)
            {
                Resultado = true;
            }

            return Resultado;
        }

        //consulta un cliente por cedula
        public bool ConsultarPorCedula()
        {
            bool Resultado = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Cedula", Cedula));

            DataTable Consulta = MiCnn.EjecutarSelect("SpClienteConsultarCedula");

            if (Consulta != null && Consulta.Rows.Count > 0)
            {
                Resultado = true;
            }

            return Resultado;
        }

        //consulta un cliente por id
        public Cliente ConsultarPorId()
        {
            Cliente Resultado = new Cliente();

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Id", IdCliente));

            DataTable Consulta = MiCnn.EjecutarSelect("SpClienteConsultarId");

            if (Consulta != null && Consulta.Rows.Count > 0)
            {
                DataRow Fila = Consulta.Rows[0];
                Resultado.IdCliente = (int)Fila["IDCliente"];
                Resultado.Cedula = (string)Fila["Cedula"];
                Resultado.Nombre = (string)Fila["Nombre"];
                Resultado.Telefono = (string)Fila["Telefono"];
                Resultado.Direccion = (string)Fila["Direccion"];
                Resultado.Activo = (bool)Fila["Activo"];
            }

            return Resultado;
        }


        //lista los clientes
        public DataTable Listar(bool VerActivos = false, string Busqueda = "")
        {
            DataTable Resultado = new DataTable();


            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@activos", VerActivos));
            MiCnn.ListaParametros.Add(new SqlParameter("@busqueda", Busqueda));

            DataTable Consulta = MiCnn.EjecutarSelect("SpClienteListar");

            if (Consulta != null && Consulta.Rows.Count > 0)
            {
                Resultado = Consulta;
            }

            return Resultado;
        }
    }
}
