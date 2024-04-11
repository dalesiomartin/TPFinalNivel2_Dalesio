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
                datos.setearConsulta("select a.Id,Codigo,Nombre,a.Descripcion,a.IdMarca,m.Descripcion Marca,a.IdCategoria,c.Descripcion Categoria,ImagenUrl,Precio  from ARTICULOS a, CATEGORIAS c, MARCAS m where a.IdMarca = m.Id and a.IdCategoria=c.Id");

                datos.ejecutarLectura();

                while (datos.lectorData.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.lectorData["Id"];
                    aux.Codigo = (string)datos.lectorData["Codigo"];
                    aux.Nombre = (string)datos.lectorData["Nombre"];
                    aux.Descripcion = (string)datos.lectorData["Descripcion"];

                    if (!(datos.lectorData["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.lectorData["ImagenUrl"];
                    
                    //aux.Precio = Math.Round((decimal)datos.lectorData["Precio"], 2);
                    //aux.Precio = Math.Truncate(100 * (decimal)datos.lectorData["Precio"]) / 100;
                    aux.Precio = Math.Floor((decimal)datos.lectorData["Precio"] * 100) / 100;

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.lectorData["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.lectorData["Marca"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.lectorData["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.lectorData["Categoria"];




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

        public void agregar(Articulo nuevo) 
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta(" insert into ARTICULOS (Codigo,Nombre,Descripcion,IdMarca,IdCategoria,ImagenUrl,Precio) values (' "+nuevo.Codigo+" ',' "+nuevo.Nombre+ " ',' "+nuevo.Descripcion+ " ', @IdMarca, @IdCategoria, @ImagenUrl, "+nuevo.Precio+")");

                datos.setearParametros("@IdMarca",nuevo.Marca.Id);
                datos.setearParametros("@IdCategoria", nuevo.Categoria.Id);
                datos.setearParametros("@ImagenUrl", nuevo.ImagenUrl);

                datos.ejecutarAccion();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally 
            {
                datos.cerrarConexion();
            } 
        
        }

        public void modificar(Articulo artModif) 
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo=@codigo,Nombre=@nombre,Descripcion=@descrip,IdMarca=@idMarcar,IdCategoria=@idCateg,ImagenUrl=@imag,Precio=@precio where Id=@id");

                datos.setearParametros("@codigo", artModif.Codigo);
                datos.setearParametros("@nombre", artModif.Nombre);
                datos.setearParametros("@descrip", artModif.Descripcion);
                datos.setearParametros("@idMarcar", artModif.Marca.Id);
                datos.setearParametros("@idCateg", artModif.Categoria.Id);
                datos.setearParametros("@imag", artModif.ImagenUrl);
                datos.setearParametros("@precio", artModif.Precio);
                datos.setearParametros("@id", artModif.Id);

                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }


        }


    }
}
