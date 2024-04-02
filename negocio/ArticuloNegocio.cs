using accesoDatos;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("select Id, Codigo,Nombre,Descripcion, ImagenUrl, Precio from ARTICULOS");

                datos.ejecutarLectura();

                while (datos.lectorData.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.lectorData["Id"];
                    aux.Codigo = (string)datos.lectorData["Codigo"];
                    aux.Nombre = (string)datos.lectorData["Nombre"];
                    aux.Descripcion = (string)datos.lectorData["Descripcion"];
                    aux.ImagenUrl = (string)datos.lectorData["ImagenUrl"];
                    //aux.Precio = Math.Round((decimal)datos.lectorData["Precio"], 2);
                    //aux.Precio = Math.Truncate(100 * (decimal)datos.lectorData["Precio"]) / 100;
                    aux.Precio = Math.Floor((decimal)datos.lectorData["Precio"] * 100) / 100;

                    lista.Add(aux);

                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { 
                datos.cerrarConexion();
            }

           

        }


    }
}
