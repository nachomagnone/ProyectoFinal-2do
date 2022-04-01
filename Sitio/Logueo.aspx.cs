using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RefWCF;

public partial class Logueo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Usuario"] = null;
    }

    protected void btnIngresar_Click(object sender, EventArgs e)
    {
        try
        {
            RefWCF.Usuario _Usu = new RefWCF.ServicioClient().Logueo(txtUsu.Text, txtPass.Text);

            if (_Usu is Meteorologo)
            {
                Session["Usuario"] = _Usu;
                Response.Redirect("~/Menu_M.aspx");
            }
            else if (_Usu is Empleado)
            {
                Session["Usuario"] = _Usu;
                Response.Redirect("~/Menu_E.aspx");
            }
            else
                lblError.Text = "Usuario o pass  incorrecto";
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            txtUsu.Text = string.Empty;
            txtPass.Text = string.Empty;
            txtUsu.Focus();
        }
    }
}