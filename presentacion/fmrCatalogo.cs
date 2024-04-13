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

                ocularColumnas();

               // cargarImagen(ListaArticulo[0].ImagenUrl);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        
        }


        private void ocularColumnas() {
            dgwCatalogo.Columns["ImagenUrl"].Visible=false;
            dgwCatalogo.Columns["Id"].Visible = false;
        }

        private void dgwCatalogo_SelectionChanged(object sender, EventArgs e)
        {
            if(dgwCatalogo.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgwCatalogo.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.ImagenUrl);
            }


        }


        private void cargarImagen(string imagen) {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception)
            {

                pbxArticulo.Load("https://t3.ftcdn.net/jpg/02/48/42/64/360_F_248426448_NVKLywWqArG2ADUxDq6QprtIzsF82dMF.jpg");
            }
        
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaProducto alta = new frmAltaProducto();
            alta.ShowDialog();

            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgwCatalogo.CurrentRow.DataBoundItem;

            frmAltaProducto modificar = new frmAltaProducto(seleccionado);
            modificar.ShowDialog();
            cargar();

        }

        private void btnEliminarD_Click(object sender, EventArgs e)
        {

            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;

            try
            {
                DialogResult respuesta = MessageBox.Show("¿Seguro que quieres eliminar?", "ELIMINADO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgwCatalogo.CurrentRow.DataBoundItem;

                    negocio.eliminar(seleccionado.Id);

                    cargar();
                }



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

      

       
    }
}
