using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RefWCF;

public partial class ABMEmpleado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnEliminar.Enabled = false;
        btnModificar.Enabled = false;
        txtPass.Enabled = false;
        txtNombre.Enabled = false;
        txtHoras.Enabled = false;
        txtUsuario.Focus();
        btnAgregar.Enabled = false;
        btnBuscar.Enabled = true;
        txtUsuario.Enabled = true;
    }

    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {

    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            RefWCF.Usuario usu = null;
            usu = new RefWCF.ServicioClient().BuscarUsuario(txtUsuario.Text, (RefWCF.Usuario)Session["Usuario"]);


            if (usu == null)
            {
                lblMensaje.Text = "No se encontro Empleado";
                btnEliminar.Enabled = false;
                btnModificar.Enabled = false;
                txtPass.Enabled = true;
                txtNombre.Enabled = true;
                txtHoras.Enabled = true;
                btnAgregar.Enabled = true;
                txtUsuario.Enabled = false;
                btnBuscar.Enabled = false;
            }
            else
            {
                RefWCF.Usuario usuLogueo = (RefWCF.Usuario)Session["Usuario"];
                RefWCF.Empleado empleado = (RefWCF.Empleado)usu;
                txtNombre.Text = usu.Nombre_Completo;
                txtHoras.Text = empleado.Horas_semanales.ToString();
                txtPass.Text = empleado.Contrasena.ToString();
                Session["Empleado"] = usu;

                if (usu.UsuLog == usuLogueo.UsuLog)
                    txtPass.Enabled = true;
                else
                    txtPass.Enabled = false;

                btnEliminar.Enabled = true;
                btnModificar.Enabled = true;
                txtNombre.Enabled = true;
                txtHoras.Enabled = true;
                txtUsuario.Enabled = false;
                btnAgregar.Enabled = false;
                btnBuscar.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        RefWCF.Empleado E = null;

        try
        {
            E = new RefWCF.Empleado()
            {
                UsuLog = txtUsuario.Text.Trim(),
                Contrasena = txtPass.Text.Trim(),
                Nombre_Completo = txtNombre.Text.Trim(),
                Horas_semanales = Convert.ToInt32(txtHoras.Text.Trim())
            };

        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
            return;
        }
        try
        {
            new RefWCF.ServicioClient().AgregarUsuario((RefWCF.Usuario)E, (RefWCF.Empleado)Session["Usuario"]);
            lblMensaje.Text = "Empleado Agregado con Exito!";
            txtUsuario.Text = string.Empty;
            txtPass.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtHoras.Text = string.Empty;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            txtPass.Enabled = false;
            txtNombre.Enabled = false;
            txtHoras.Enabled = false;
            txtUsuario.Focus();
            btnAgregar.Enabled = false;
            btnBuscar.Enabled = true;
            txtUsuario.Enabled = true;
        }
        catch (Exception Ex)
        {
            lblMensaje.Text = Ex.Message;
        }
    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            RefWCF.Empleado E = (RefWCF.Empleado)Session["Empleado"];

            RefWCF.Usuario usuLogueo = (RefWCF.Usuario)Session["Usuario"];

            if (E.UsuLog.ToLower() == usuLogueo.UsuLog.ToLower())
            {

                E = new RefWCF.Empleado()
                {
                    UsuLog = txtUsuario.Text.Trim(),
                    Contrasena = txtPass.Text.Trim(),
                    Nombre_Completo = txtNombre.Text.Trim(),
                    Horas_semanales = Convert.ToInt32(txtHoras.Text.Trim())
                };

                new RefWCF.ServicioClient().ModificarUsuario((RefWCF.Usuario)E, (RefWCF.Empleado)Session["Usuario"]);

                Response.Redirect("~/Logueo.aspx");
            }
            else
            {
                E = new RefWCF.Empleado()
                {
                    UsuLog = txtUsuario.Text.Trim(),
                    Contrasena = E.Contrasena.ToString(),
                    Nombre_Completo = txtNombre.Text.Trim(),
                    Horas_semanales = Convert.ToInt32(txtHoras.Text.Trim())
                };

                new RefWCF.ServicioClient().ModificarUsuario((RefWCF.Usuario)E, (RefWCF.Empleado)Session["Usuario"]);

                lblMensaje.Text = "Empleado Modificado!";

                txtUsuario.Enabled = true;
                txtUsuario.Text = string.Empty;
                txtPass.Text = string.Empty;
                txtNombre.Text = string.Empty;
                txtHoras.Text = string.Empty;
                btnEliminar.Enabled = false;
                btnModificar.Enabled = false;
                txtPass.Enabled = false;
                txtNombre.Enabled = false;
                txtHoras.Enabled = false;
                txtUsuario.Focus();
                btnAgregar.Enabled = false;
                btnBuscar.Enabled = true;
            }
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
            RefWCF.Empleado E = (RefWCF.Empleado)Session["Empleado"];
           
            new RefWCF.ServicioClient().EliminarUsuario(E, (RefWCF.Empleado)Session["Usuario"]);
            lblMensaje.Text = "Empleado Eliminado con Exito!";
            txtUsuario.Text = string.Empty;
            txtPass.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtHoras.Text = string.Empty;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            txtPass.Enabled = false;
            txtNombre.Enabled = false;
            txtHoras.Enabled = false;
            txtUsuario.Focus();
            btnAgregar.Enabled = false;
            btnBuscar.Enabled = true;
            txtUsuario.Enabled = true;
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
        txtHoras.Text = string.Empty;
        btnEliminar.Enabled = false;
        btnModificar.Enabled = false;
        txtPass.Enabled = false;
        txtNombre.Enabled = false;
        txtHoras.Enabled = false;
        txtUsuario.Focus();
        btnAgregar.Enabled = false;
        btnBuscar.Enabled = true;
        txtUsuario.Enabled = true;
    }
}