using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RefWCF;

public partial class ListadosSinAsignacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnfiltrarCiudad_Click(object sender, EventArgs e)
    {
        try
        {
            int anio;
            if (txtCiudad.Text == string.Empty)
                anio = 0;
            else
                anio = Convert.ToInt32(txtCiudad.Text);

            Session["Ciudades"] = new RefWCF.ServicioClient().ListarCiudadSinPronxAno(anio, (RefWCF.Empleado)Session["Usuario"]);
            grvCiudad.DataSource = Session["Ciudades"];
            grvCiudad.DataBind();
            lblMensaje.Text = string.Empty;

        }
        catch (Exception ex)
        {

            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnFiltrarMete_Click(object sender, EventArgs e)
    {
        try
        {
            int anio;
            if (txtMeteorologo.Text == string.Empty)
                anio = 0;
            else
                anio = Convert.ToInt32(txtMeteorologo.Text);

            Session["Meteorologos"] = new RefWCF.ServicioClient().ListarMeteorologoSinPronxAno(anio, (RefWCF.Empleado)Session["Usuario"]);
            grvMeteorologo.DataSource = Session["Meteorologos"];
            grvMeteorologo.DataBind();
            lblMensaje.Text = string.Empty;

        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            grvCiudad.DataSource = null;
            grvCiudad.DataBind();
            grvMeteorologo.DataSource = null;
            grvMeteorologo.DataBind();
            lblMensaje.Text = string.Empty;
        }
        catch ( Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
       
    }
}