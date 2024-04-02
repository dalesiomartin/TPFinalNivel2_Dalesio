using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using accesoDatos;
using negocio;

namespace presentacion
{
    public partial class frmCatalogo : Form
    {
        private List<Articulo> ListaArticulo;

        public frmCatalogo()
        {
            InitializeComponent();
        }


        private void fmrCatalogo_Load(object sender, EventArgs e)
        {
            cargar();


        }

        private void cargar() { 

            ArticuloNegocio negocio = new ArticuloNegocio();
      

               try
            {
                ListaArticulo = negocio.listar();
               
                dgwCatalogo.DataSource = ListaArticulo;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        
        }


        private void dgwCatalogo_SelectionChanged(object sender, EventArgs e)
        {
            if(dgwCatalogo.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgwCatalogo.CurrentRow.DataBoundItem;

            }



        }
    }
}
