using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RefWCF;

public partial class GenerarPronosticoTiempo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            CargoCiudades();
            CargoTiposDeCielo();
            Session["Horas"] = new List<RefWCF.Pronostico_Hora>();
        }
    }

    public void CargoCiudades()
    {
        try
        {
            List<RefWCF.Ciudades> ListaCiudades = new RefWCF.ServicioClient().ListarCiudades((RefWCF.Meteorologo)Session["Usuario"]).ToList();
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
    public void CargoTiposDeCielo()
    {
        ddlCielo.Items.Add("despejado");
        ddlCielo.Items.Add("parcialmente nuboso");
        ddlCielo.Items.Add("nuboso");
    }

    protected void btnAltaPHora_Click(object sender, EventArgs e)
    {
        try
        {
            List<RefWCF.Pronostico_Hora> horas = (List<RefWCF.Pronostico_Hora>)Session["Horas"];

            int hora = Convert.ToInt32(txbHora.Text);
            string tipo_cielo = ddlCielo.SelectedValue.ToString();
            int tempMax = Convert.ToInt32(txbTempMax.Text);
            int tempMin = Convert.ToInt32(txbTempMin.Text);
            int probLluvia = Convert.ToInt32(txbLluvia.Text);
            int probTorm = Convert.ToInt32(txbTormenta.Text);
            int viento = Convert.ToInt32(txbViento.Text);

            if (horas != null)
            {
                bool repetido = horas.Where(a => a.Hora_pronostico == hora).Any();
                if (repetido)
                {
                    lblMensaje.Text = "No se puede repetir la misma hora";
                    return;
                }
            }
            RefWCF.Pronostico_Hora pronH = new RefWCF.Pronostico_Hora();

            pronH.Hora_pronostico = hora;

              
                pronH.Tipo_Cielo = tipo_cielo;
                pronH.Temp_Max = tempMax;
                pronH.Temp_Min = tempMin;
                pronH.Probabilidad_Lluvias = probLluvia;
                pronH.Probabilidad_Tormentas = probTorm;
                pronH.Velocidad_Viento = viento;

            horas.Add(pronH);
            grvPronosticosHora.DataSource = horas;
            grvPronosticosHora.DataBind();

            //habilito crear Pronostico
            btnAltaPronostico.Enabled = true;

        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnAltaPronostico_Click(object sender, EventArgs e)
    {
        RefWCF.Pronosticos P = null;

        try
        {
            List <RefWCF.Pronostico_Hora > horas = (List<RefWCF.Pronostico_Hora>)Session["Horas"];
             
            RefWCF.Ciudades ciudad = new RefWCF.ServicioClient().BuscarCiudad(ddlCiudades.SelectedValue, (RefWCF.Usuario)Session["Usuario"]);
            RefWCF.Usuario usuario = (RefWCF.Usuario)Session["Usuario"];
           
            P = new RefWCF.Pronosticos()
            {
                Codigo_interno = 0,
                Fecha = Convert.ToDateTime(txtFecha.Text),
                Ciudad = ciudad,
                Usu = (RefWCF.Meteorologo)usuario,
                ProH = horas.ToArray()
                

            };
            
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
            return;
        }
        try
        {
            new RefWCF.ServicioClient().AgregarPronostico(P, (RefWCF.Meteorologo)Session["Usuario"]);
            lblMensaje.Text = "Alta con exito";
            Session["Horas"] = new List<RefWCF.Pronostico_Hora>();
            grvPronosticosHora.DataSource = Session["Horas"];
            grvPronosticosHora.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }
}