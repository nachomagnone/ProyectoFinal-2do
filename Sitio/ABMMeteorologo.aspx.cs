using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ABMMeteorologo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnEliminar.Enabled = false;
        btnModificar.Enabled = false;
        txtPass.Enabled = false;
        txtNombre.Enabled = false;
        txtMail.Enabled = false;
        txtTelefono.Enabled = false;
        txtUsuario.Enabled = true;
        txtUsuario.Focus();
        btnAgregar.Enabled = false;
        btnBuscar.Enabled = true;
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        RefWCF.Meteorologo M = null;

        try
        {
            M = new RefWCF.Meteorologo()
            {
                UsuLog = txtUsuario.Text.Trim(),
                Contrasena = txtPass.Text.Trim(),
                Nombre_Completo = txtNombre.Text.Trim(),
                Mail = txtMail.Text.Trim().ToString(),
                Telefono = txtTelefono.Text.Trim().ToString()
            };

        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
            return;
        }
        try
        {
            new RefWCF.ServicioClient().AgregarUsuario((RefWCF.Usuario)M, (RefWCF.Empleado)Session["Usuario"]);
            lblMensaje.Text = "Meteorologo Agregado con Exito!";
            txtUsuario.Text = string.Empty;
            txtPass.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtMail.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            txtPass.Enabled = false;
            txtNombre.Enabled = false;
            txtMail.Enabled = false;
            txtTelefono.Enabled = false;
            txtUsuario.Enabled = true;
            txtUsuario.Focus();
            btnAgregar.Enabled = false;
            btnBuscar.Enabled = true;
        }
        catch (Exception Ex)
        {
            lblMensaje.Text = Ex.Message;
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            RefWCF.Usuario usu = null;
            usu = new RefWCF.ServicioClient().BuscarUsuario(txtUsuario.Text, (RefWCF.Usuario)Session["Usuario"]);

            if (usu == null)
            {
                lblMensaje.Text = "No se encontro el Meteorologo";
                btnBuscar.Enabled = true;
                btnEliminar.Enabled = false;
                btnModificar.Enabled = false;
                txtPass.Enabled = true;
                txtNombre.Enabled = true;
                txtMail.Enabled = true;
                txtTelefono.Enabled = true;
                btnAgregar.Enabled = true;
            }
            else
            {
                RefWCF.Meteorologo m = (RefWCF.Meteorologo)usu;
                txtPass.Text = usu.Contrasena;
                txtNombre.Text = usu.Nombre_Completo;
                txtMail.Text = m.Mail;
                txtTelefono.Text = m.Telefono;
                Session["Meteorologo"] = usu;
                btnBuscar.Enabled = false;
                btnEliminar.Enabled = true;
                btnModificar.Enabled = true;
                txtNombre.Enabled = true;
                txtMail.Enabled = true;
                txtTelefono.Enabled = true;
                btnAgregar.Enabled = false;
                txtPass.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            RefWCF.Meteorologo M = (RefWCF.Meteorologo)Session["Meteorologo"];

            M = new RefWCF.Meteorologo()
            {
                UsuLog = txtUsuario.Text.Trim(),
                Contrasena = M.Contrasena.Trim(),
                Nombre_Completo = txtNombre.Text.Trim(),
                Mail = txtMail.Text.Trim(),
                Telefono = txtTelefono.Text.Trim()
            };

            new RefWCF.ServicioClient().ModificarUsuario((RefWCF.Usuario)M, (RefWCF.Empleado)Session["Usuario"]);

            lblMensaje.Text = "Meteorologo Modificado!";
            txtUsuario.Text = string.Empty;
            txtPass.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtMail.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            txtPass.Enabled = false;
            txtNombre.Enabled = false;
            txtMail.Enabled = false;
            txtTelefono.Enabled = false;
            txtUsuario.Enabled = true;
            txtUsuario.Focus();
            btnAgregar.Enabled = false;
            btnBuscar.Enabled = true;

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
            RefWCF.Meteorologo M = (RefWCF.Meteorologo)Session["Meteorologo"];

            new RefWCF.ServicioClient().EliminarUsuario(M, (RefWCF.Empleado)Session["Usuario"]);
            lblMensaje.Text = "Meteorologo Eliminado con Exito!";
            txtUsuario.Text = string.Empty;
            txtPass.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtMail.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            txtPass.Enabled = false;
            txtNombre.Enabled = false;
            txtMail.Enabled = false;
            txtTelefono.Enabled = false;
            txtUsuario.Enabled = true;
            txtUsuario.Focus();
            btnAgregar.Enabled = false;
            btnBuscar.Enabled = true;
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        txtUsuario.Text = string.Empty;
        txtPass.Text = string.Empty;
        txtNombre.Text = string.Empty;
        txtMail.Text = string.Empty;
        txtTelefono.Text = string.Empty;
        btnEliminar.Enabled = false;
        btnModificar.Enabled = false;
        txtPass.Enabled = false;
        txtNombre.Enabled = false;
        txtMail.Enabled = false;
        txtTelefono.Enabled = false;
        txtUsuario.Enabled = true;
        txtUsuario.Focus();
        btnAgregar.Enabled = false;
        btnBuscar.Enabled = true;
    }
}