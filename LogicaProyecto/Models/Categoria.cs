using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaProyecto.Models
{
    public class Categoria
    {

        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public bool Activo { get; set; }

        //agrega una categoria en la base de datos
        public bool Agregar()
        {
            bool Resultado = false;


            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@nombre", NombreCategoria));

            int Consulta = MiCnn.EjecutarUpdateDeleteInsert("SpCategoriaGuardar");

            if (Consulta > 0)
            {
                Resultado = true;
            }


            return Resultado;
        }


        //modifica una categoria en la base de datos
        public bool Modificar()
        {
            bool Resultado = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Id", IdCategoria));
            MiCnn.ListaParametros.Add(new SqlParameter("@Nombre", NombreCategoria));

            int Consulta = MiCnn.EjecutarUpdateDeleteInsert("SpCategoriaModificar");

            if (Consulta > 0)
            {
                Resultado = true;
            }

            return Resultado;
        }

        //elimina o reactiva una categoria desde la base de datos
        public bool Eliminar()
        {
            bool Resultado = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Id", this.IdCategoria));

            int Consulta = MiCnn.EjecutarUpdateDeleteInsert("SpCategoriaEliminar");

            if (Consulta > 0)
            {
                Resultado = true;
            }

            return Resultado;
        }

        //consulta una categoria por nombre
        public bool ConsultarPorNombre()
        {
            bool Resultado = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@nombre", NombreCategoria));

            DataTable Consulta = MiCnn.EjecutarSelect("SpCategoriaConsultarNombre");

            if (Consulta != null && Consulta.Rows.Count > 0)
            {
                Resultado = true;
            }

            return Resultado;
        }

        //consulta una categoria por id
        public Categoria ConsultarPorId()
        {
            Categoria Resultado = new Categoria();

            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@Id", IdCategoria));

            DataTable Consulta = MiCnn.EjecutarSelect("SpCategoriaConsultarId");

            if (Consulta != null && Consulta.Rows.Count > 0)
            {
                DataRow Fila = Consulta.Rows[0];
                Resultado.IdCategoria = (int)Fila["IDCategoria"];
                Resultado.NombreCategoria = (string)Fila["NombreCategoria"];
                Resultado.Activo = (bool)Fila["Activo"];
            }

            return Resultado;
        }

        //lista las categorias
        public DataTable Listar(bool VerActivos = true, string Busqueda = "")
        {
            DataTable Resultado = new DataTable();


            Conexion MiCnn = new Conexion();

            MiCnn.ListaParametros.Add(new SqlParameter("@activos", VerActivos));
            MiCnn.ListaParametros.Add(new SqlParameter("@busqueda", Busqueda));

            DataTable Consulta = MiCnn.EjecutarSelect("SpCategoriaListar");

            if (Consulta != null && Consulta.Rows.Count > 0)
            {
                Resultado = Consulta;
            }


            return Resultado;
        }


    }
}
