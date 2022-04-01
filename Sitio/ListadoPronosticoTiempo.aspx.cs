using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RefWCF;
using System.Collections;

public partial class ListadoPronosticoTiempo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session["PronosticosA"] = new RefWCF.ServicioClient().ListarPronosticoAnio((RefWCF.Empleado)Session["Usuario"]).ToList();
                //Cargo la grilla con la info
                grvPronosticos.DataSource = Session["PronosticosA"];
                grvPronosticos.DataBind();
                CargoCiudades();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;

            }
        }
    }
    public void CargoCiudades()
    {
        try
        {
            List<RefWCF.Ciudades> ListaCiudades = new RefWCF.ServicioClient().ListarTodasLasCiudades((RefWCF.Empleado)Session["Usuario"]).ToList();
            ddlCiudades.DataSource = ListaCiudades;
            ddlCiudades.DataValueField = "Codigo_Ciudad";
            ddlCiudades.DataTextField = "Nombre_ciudad";
            ddlCiudades.DataBind();
            Session["Ciudad"] = ListaCiudades;

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
            grvPronosticos.DataSource = Session["PronosticosA"];
            grvPronosticos.DataBind();
            grvPronosticoHora.DataSource = null;
            grvPronosticoHora.DataBind();
            grvMeteorologos.DataSource = null;
            grvMeteorologos.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void grvPronosticos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<RefWCF.Pronosticos> pronosticos = (List<RefWCF.Pronosticos>)Session["PronosticosA"];
            List<RefWCF.Pronostico_Hora> pronosticosHora = pronosticos[grvPronosticos.SelectedIndex].ProH.ToList();

            grvPronosticoHora.DataSource = pronosticosHora;
            grvPronosticoHora.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void BtnFiltrarCiudad_Click(object sender, EventArgs e)
    {
        try
        {
            List<RefWCF.Pronosticos> pronostico = (List<RefWCF.Pronosticos>)Session["PronosticosA"];

            List<RefWCF.Pronosticos> resultado = (from unP in pronostico
                                                  where unP.Ciudad.Codigo_Ciudad == ddlCiudades.SelectedValue.ToString()
                                                  select unP).ToList<RefWCF.Pronosticos>();

            grvPronosticos.DataSource = resultado;
            grvPronosticos.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void BtnFiltrarFecha_Click(object sender, EventArgs e)
    {
        try
        {
            List<RefWCF.Pronosticos> pronostico = (List<RefWCF.Pronosticos>)Session["PronosticosA"];

            List<RefWCF.Pronosticos> resultado = (from unP in pronostico
                                                  where unP.Fecha == Convert.ToDateTime(TxtFecha.Text)
                                                  select unP).ToList<RefWCF.Pronosticos>();

            grvPronosticos.DataSource = resultado;
            grvPronosticos.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void BtnFiltroMeteorologo_Click(object sender, EventArgs e)
    {
        try
        {           
            List<RefWCF.Pronosticos> pronostico = (List<RefWCF.Pronosticos>)Session["PronosticosA"];

            List<object> resultado = (from unP in pronostico

                                      group unP by unP.Usu.UsuLog
                            into Cantidad
                                      select new
                                      {
                                          _Meteorologo = Cantidad.Key,
                                          Cantidad = Cantidad.Count()
                                      }).ToList<object>();

            grvMeteorologos.DataSource = resultado;
            grvMeteorologos.DataBind();
            grvPronosticos.DataSource = null;
            grvPronosticos.DataBind();
            grvPronosticoHora.DataSource = null;
            grvPronosticoHora.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }

    }

    protected void grvMeteorologos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<RefWCF.Pronosticos> pronostico = (List<RefWCF.Pronosticos>)Session["PronosticosA"];
            DateTime fechaLimite = DateTime.Now.AddYears(-1);
            string meteorologo = grvMeteorologos.SelectedRow.Cells[1].Text;
            List<RefWCF.Pronosticos> resultadoP = (from unP in pronostico
                                                   where unP.Usu.UsuLog == meteorologo && unP.Fecha > fechaLimite
                                                   select unP).ToList<RefWCF.Pronosticos>();

            grvPronosticos.DataSource = resultadoP;
            grvPronosticos.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }
}