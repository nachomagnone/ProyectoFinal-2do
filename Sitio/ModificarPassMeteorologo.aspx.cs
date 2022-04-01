using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RefWCF;

public partial class ModificarPassMeteorologo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnModificar.Enabled = false;
        txtPass.Enabled = false;
        txtUsulog.Enabled = true;
        txtUsulog.Focus();
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            RefWCF.Usuario usu = null;
            usu = new RefWCF.ServicioClient().BuscarUsuario(txtUsulog.Text, (RefWCF.Usuario)Session["Usuario"]);

            if (usu == null)
            {
                lblError.Text = "No se encontro el Meteorologo";
                btnModificar.Enabled = false;
                txtPass.Enabled = false;
                txtUsulog.Enabled = true;
                txtUsulog.Focus();
            }
            else
            {
                RefWCF.Usuario usuLogueo = (RefWCF.Usuario)Session["Usuario"];
                RefWCF.Meteorologo m = (RefWCF.Meteorologo)usu;
                Session["Meteorologo"] = usu;
                if (m.UsuLog.ToLower() == usuLogueo.UsuLog.ToLower())
                {
                    txtPass.Text = usu.Contrasena;
                    btnBuscar.Enabled = false;
                    btnModificar.Enabled = true;
                    txtPass.Enabled = true;
                    txtUsulog.Enabled = false;
                }
                else
                {
                    lblError.Text = "No tiene los permisos necesarios";
                    btnModificar.Enabled = false;
                    txtPass.Enabled = false;
                    txtUsulog.Enabled = true;
                    txtUsulog.Focus();
                }
                
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            RefWCF.Meteorologo M = (RefWCF.Meteorologo)Session["Meteorologo"];

            M = new RefWCF.Meteorologo()
            {
                UsuLog = txtUsulog.Text.Trim(),
                Contrasena = txtPass.Text.Trim(),
                Nombre_Completo = M.Nombre_Completo.Trim(),
                Mail = M.Mail.Trim(),
                Telefono = M.Telefono.Trim()
            };

            new RefWCF.ServicioClient().ModificarPassMeteorologo((RefWCF.Meteorologo)M, (RefWCF.Meteorologo)Session["Usuario"]);
            Response.Redirect("~/Logueo.aspx");

        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}