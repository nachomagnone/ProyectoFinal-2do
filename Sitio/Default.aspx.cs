using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RefWCF;
using System.Xml.Linq;
using System.Xml;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session["Doc"] = new RefWCF.ServicioClient().ListarPronosticodeHoy();

                XElement midoc = (XElement)Session["Doc"];

                List<object> lista = (from unP in midoc.Elements("Pronosticos")
                                      group unP by unP.Element("Nombre_Ciudad").Value into Grupito
                                      select new
                                      {
                                          Ciudad = Grupito.Key.ToString(),
                                          Pais = Grupito.First().Element("Nombre_Pais").Value,
                                      }).ToList<Object>();


                grdPronostico.DataSource = lista;
                grdPronostico.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }

    protected void btnLogueo_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Logueo.aspx");
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void grdPronostico_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            XElement midoc = (XElement)Session["Doc"];
            string ciudad = grdPronostico.SelectedRow.Cells[1].Text;
            List<object> resultado = (from unN in midoc.Elements("Pronosticos")
                                      where (string)unN.Element("Nombre_Ciudad").Value == ciudad
                                      select unN).ToList<object>();
            string _resultado = "<Raiz>";
            foreach (var unNodo in resultado)
            {
                _resultado += unNodo.ToString();
            }
            _resultado += "</Raiz>";

            XmlListado.DocumentContent = _resultado;
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}