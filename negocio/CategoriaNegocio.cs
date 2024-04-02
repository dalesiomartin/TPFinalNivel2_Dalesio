using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using accesoDatos;

namespace negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar() 
        {
            List<Categoria> lista = new List<Categoria>();
            ConexionDatos datos = new ConexionDatos();

            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            
            return listar();
        
        }
        

    }
}
