using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DiplomadoMVC_Crud_HTML_Helps_NoTipados.Models
{
    public class MantenimientoProducto
    {
        private SqlConnection con;

        private void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConexionDb"].ToString();
            con = new SqlConnection(constr);
        }

        public int Agregar(Productos prod)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("insert into Producto(descripcion,precio) values (@descripcion,@precio)", con);
            comando.Parameters.Add("@descripcion", SqlDbType.VarChar);
            comando.Parameters.Add("@precio", SqlDbType.Float);
            comando.Parameters["@descripcion"].Value = prod.Descripcion;
            comando.Parameters["@precio"].Value = prod.Precio;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return 1;
        }
        public List<Productos> RecuperarTodos()
        {
            Conectar();
            List<Productos> productos = new List<Productos>();

            SqlCommand com = new SqlCommand("select codigo,descripcion,precio from Productos", con);
            con.Open();
            SqlDataReader registros = com.ExecuteReader();
            while (registros.Read())
            {
                Productos prod = new Productos
                {
                    Codigo = int.Parse(registros["codigo"].ToString()),
                    Descripcion = registros["descripcion"].ToString(),
                    Precio = float.Parse(registros["precio"].ToString())
                };
                productos.Add(prod);
            }
            con.Close();
            return productos;
        }

        public Productos Recuperar(int codigo)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("select codigo,descripcion, precio from Producto where codigo=@codigo", con);
            comando.Parameters.Add("@codigo", SqlDbType.Int);
            comando.Parameters["@codigo"].Value = codigo;
            con.Open();
            SqlDataReader registros = comando.ExecuteReader();
            Productos producto = new Productos();
            if (registros.Read())
            {
                producto.Codigo = int.Parse(registros["codigo"].ToString());
                producto.Descripcion = registros["descripcion"].ToString();
                producto.Precio = float.Parse(registros["precio"].ToString());
            }
            else

                producto = null;
            con.Close();
            return producto;

        }

        public int Modificar(Productos prod)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("update Producto set descripcion=@Descripcion,precio=@Precio where codigo=@codigo", con);
            comando.Parameters.Add("@descripcion", SqlDbType.VarChar);
            comando.Parameters["@descripcion"].Value = prod.Descripcion;
            comando.Parameters.Add("@Precio", SqlDbType.Float);
            comando.Parameters["@Precio"].Value = prod.Precio;
            comando.Parameters.Add("@codigo", SqlDbType.Int);
            comando.Parameters["@codigo"].Value = prod.Codigo;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }


        public int Borrar(int codigo)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("delete from Producto where Id=@Id", con);
            comando.Parameters.Add("@codigo", SqlDbType.Int);
            comando.Parameters["@codigo"].Value = codigo;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }


    }
}
