using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaProyecto.Models
{
    public class Articulo
    {
        public int IdArticulo { get; set; }
        public string Codigo { get; set; }
        public string NombreArticulo { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public Categoria MiCategoria { get; set; }

        public Articulo()
        {
            MiCategoria = new Categoria();
        }

        //agrega un articulo a la base de datos
        public bool Agregar()
        {
            bool Resultado = false;


            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Codigo", Codigo));
            MiCnn.ListaParametros.Add(new SqlParameter("@Nombre", NombreArticulo));
            MiCnn.ListaParametros.Add(new SqlParameter("@Descripcion", Descripcion));
            MiCnn.ListaParametros.Add(new SqlParameter("@Categoria", MiCategoria.IdCategoria));

            int Consulta = MiCnn.EjecutarUpdateDeleteInsert("SpArticuloGuardar");

            if (Consulta > 0)
            {
                Resultado = true;
            }


            return Resultado;
        }

        //modifica un articulo de la base de datos
        public bool Modificar()
        {
            bool Resultado = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Id", IdArticulo));
            MiCnn.ListaParametros.Add(new SqlParameter("@Codigo", Codigo));
            MiCnn.ListaParametros.Add(new SqlParameter("@Nombre", NombreArticulo));
            MiCnn.ListaParametros.Add(new SqlParameter("@Descripcion", Descripcion));
            MiCnn.ListaParametros.Add(new SqlParameter("@Categoria", MiCategoria.IdCategoria));

            int Consulta = MiCnn.EjecutarUpdateDeleteInsert("SpArticuloModificar");

            if (Consulta > 0)
            {
                Resultado = true;
            }

            return Resultado;
        }

        //elimina un articulo de la base de datos o lo reactiva
        public bool Eliminar()
        {
            bool Resultado = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Id", this.IdArticulo));

            int Consulta = MiCnn.EjecutarUpdateDeleteInsert("SpArticuloEliminar");

            if (Consulta > 0)
            {
                Resultado = true;
            }

            return Resultado;
        }

        //consulta un articulo por codigo
        public bool ConsultarPorCodigo()
        {
            bool Resultado = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Codigo", Codigo));

            DataTable Consulta = MiCnn.EjecutarSelect("SpArticuloConsultarCodigo");

            if (Consulta != null && Consulta.Rows.Count > 0)
            {
                Resultado = true;
            }

            return Resultado;
        }

        //consulta un articulo por id
        public Articulo ConsultarPorId()
        {
            Articulo Resultado = new Articulo();

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Id", IdArticulo));

            DataTable Consulta = MiCnn.EjecutarSelect("SpArticuloConsultarId");

            if (Consulta != null && Consulta.Rows.Count > 0)
            {
                DataRow Fila = Consulta.Rows[0];
                Resultado.IdArticulo = (int)Fila["IDArticulo"];
                Resultado.Codigo = (string)Fila["Codigo"];
                Resultado.NombreArticulo = (string)Fila["NombreArticulo"];
                Resultado.Descripcion = (string)Fila["Descripcion"];
                Resultado.Activo = (bool)Fila["Activo"];
                Resultado.MiCategoria.IdCategoria = (int)Fila["IDCatergoria"];
            }

            return Resultado;
        }

        //lista los articulos desde la base de datos
        public DataTable Listar(bool VerActivos = false, string Busqueda = "")
        {
            DataTable Resultado = new DataTable();


            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@activos", VerActivos));
            MiCnn.ListaParametros.Add(new SqlParameter("@busqueda", Busqueda));

            DataTable Consulta = MiCnn.EjecutarSelect("SpArticuloListar");

            if (Consulta != null && Consulta.Rows.Count > 0)
            {
                Resultado = Consulta;
            }

            return Resultado;
        }

        //lista los articulos segun fecha que no se encuentre reservado
        public DataTable VerificarReservaListar(DateTime Inicio,DateTime Fin, string Busqueda = "")
        {
            DataTable Resultado = new DataTable();


            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@busqueda", Busqueda));
            MiCnn.ListaParametros.Add(new SqlParameter("@Inicio", Inicio));
            MiCnn.ListaParametros.Add(new SqlParameter("@Fin", Fin));

            DataTable Consulta = MiCnn.EjecutarSelect("SpArticuloVerificarReservaListar");

            if (Consulta != null && Consulta.Rows.Count > 0)
            {
                Resultado = Consulta;
            }

            return Resultado;
        }
    }
}
