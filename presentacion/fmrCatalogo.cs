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
    public partial class lbBuscar : Form
    {
        private List<Articulo> listaArticulo;

        public lbBuscar()
        {
            InitializeComponent();
        }


        private void fmrCatalogo_Load(object sender, EventArgs e)
        {
            cargar();
           
            cboCampo.Items.Add("Código de Artículo");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Descripción");
            cboCampo.Items.Add("Marca");
            cboCampo.Items.Add("Categoria");
            cboCampo.Items.Add("Precio");

        }

        private void cargar() { 

            ArticuloNegocio negocio = new ArticuloNegocio();
      
               try
            {
                listaArticulo = negocio.listar();
               
                dgwCatalogo.DataSource = listaArticulo;

                ocularColumnas();

                cargarImagen(listaArticulo[0].ImagenUrl);
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

        private bool validarFiltro() 
        {
            if (cboCampo.SelectedIndex < 0)  
            {
                MessageBox.Show("Por favor, seleccione el CAMPO para filtrar");
                return true;
            }
            if (cboCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione el CRITERIO para filtrar");
                return true;
            }

            if (cboCampo.SelectedItem.ToString() == "Precio")
            {

                if (string.IsNullOrEmpty(txtFiltro.Text))
                {
                    MessageBox.Show("Debes cargar el filtro para numericos...");
                    return true;
                }
                if (!(soloNumeros(txtFiltro.Text)))
                {
                    MessageBox.Show("Solo nros para filtrar por un campo numerico.....");
                    return true;
                }
            }
            return false;
        }

        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter) || caracter == '.'))
                {
                    return false;
                }

            }
            return true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (validarFiltro())
                {
                    return;
                }
                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltro.Text;

                dgwCatalogo.DataSource = negocio.filtrar(campo, criterio,filtro);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtFiltroRapido.Text;

            if (filtro.Length >= 3)
            {
                listaFiltrada = listaArticulo.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Descripcion.ToUpper().Contains(filtro.ToUpper()) || x.Codigo.ToUpper().Contains(filtro.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper()) || x.Categoria.Descripcion.ToUpper().Contains(filtro.ToUpper()) );
            }
            else {
                listaFiltrada = listaArticulo;
            }

            dgwCatalogo.DataSource = null;
            dgwCatalogo.DataSource = listaFiltrada;

            ocularColumnas();

        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboCampo.SelectedItem != null) // Verificar si hay un elemento seleccionado
            {
                string opcion = cboCampo.SelectedItem.ToString();

                if (opcion == "Precio")
                {
                    cboCriterio.Items.Clear();
                    cboCriterio.Items.Add("Mayor a");
                    cboCriterio.Items.Add("Menor a");
                    cboCriterio.Items.Add("Igual a");
                }
                else
                {
                    cboCriterio.Items.Clear();
                    cboCriterio.Items.Add("Termina con");
                    cboCriterio.Items.Add("Empieza con");
                    cboCriterio.Items.Add("Igual a");
                }
            }
            else
            {
                // Si no hay ningún elemento seleccionado, maneja esta situación según tu lógica
            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                listaArticulo = negocio.LimpiarFiltros();
                dgwCatalogo.DataSource= listaArticulo;
                cboCampo.SelectedIndex = -1;
                cboCriterio.SelectedIndex = -1;
                txtFiltro.Text = string.Empty;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
