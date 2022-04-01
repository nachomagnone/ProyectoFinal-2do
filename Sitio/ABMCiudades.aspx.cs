using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ABMCiudades : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtCiudad.Enabled = false;
        txtPais.Enabled = false;
        txtCodigo.Focus();
        btnAgregar.Enabled = false;
        btnModificar.Enabled = false;
        btn.Enabled = false;
        btnBuscar.Enabled = true;
    }
                 
    //ELIMINAR
    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            RefWCF.Ciudades ciudad = (RefWCF.Ciudades)Session["Ciudad"];

            new RefWCF.ServicioClient().EliminarCiudad(ciudad, (RefWCF.Empleado)Session["Usuario"]);
            lblMensaje.Text = "Ciudad Eliminada con Exito!";
            txtCiudad.Text = string.Empty;
            txtPais.Text = string.Empty;
            txtCodigo.Text = string.Empty;
            btnAgregar.Enabled = false;
            btnBuscar.Enabled = true;
            txtCodigo.Enabled = true;
            btnModificar.Enabled = false;
            btn.Enabled = false;
        }

        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
            txtCiudad.Enabled = false;
            txtPais.Enabled = false;
            txtCodigo.Focus();
            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btn.Enabled = false;
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            RefWCF.Ciudades ciudad = new RefWCF.ServicioClient().BuscarCiudad(txtCodigo.Text, (RefWCF.Usuario)Session["Usuario"]);

            if (ciudad == null)
            {
                lblMensaje.Text = "No se encontro Ciudad";
                btn.Enabled = false;
                btnModificar.Enabled = false;
                txtCiudad.Enabled = true;
                txtPais.Enabled = true;
                txtCodigo.Enabled = false;
                btnAgregar.Enabled = true;
            }
            else
            {
                txtCiudad.Text = ciudad.Nombre_ciudad;
                txtPais.Text = ciudad.Nombre_pais;
                Session["Ciudad"] = ciudad;

                txtCiudad.Enabled = true;
                txtPais.Enabled = true;
                btnAgregar.Enabled = false;
                txtCodigo.Enabled = false;
                btnModificar.Enabled = true;
                btn.Enabled = true;
            }
        }
        catch (Exception ex) 
        {
            lblMensaje.Text = ex.Message;
            txtCiudad.Enabled = false;
            txtPais.Enabled = false;
            txtCodigo.Focus();
            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btn.Enabled = false;
        }
    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            RefWCF.Ciudades ciudad = (RefWCF.Ciudades)Session["Ciudad"];
            ciudad.Nombre_ciudad = txtCiudad.Text.Trim();
            ciudad.Nombre_pais = txtPais.Text.Trim();

            new RefWCF.ServicioClient().ModificarCiudad(ciudad, (RefWCF.Empleado)Session["Usuario"]);

            lblMensaje.Text = "Ciudad Modificada!";
            txtCiudad.Text = "";
            txtCodigo.Enabled = true;
            txtPais.Text = "";
            txtCodigo.Text = "";
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        RefWCF.Ciudades C = null;

        try
        {
            C = new RefWCF.Ciudades()
            {
                Codigo_Ciudad = txtCodigo.Text.Trim(),
                Nombre_ciudad = txtCiudad.Text.Trim(),
                Nombre_pais = txtPais.Text.Trim()
            };

        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
            return;
        }
        try
        {
            new RefWCF.ServicioClient().AgregarCiudad(C, (RefWCF.Empleado)Session["Usuario"]);
            lblMensaje.Text = "Ciudad Agregada con Exito!";
            txtCodigo.Enabled = true;
            txtCodigo.Focus();
            txtCodigo.Text = string.Empty;
            txtCiudad.Text = string.Empty;
            txtPais.Text = string.Empty;
            btnBuscar.Enabled = true;
            btnModificar.Enabled = false;
            btn.Enabled = false;
            btnAgregar.Enabled = false;
        }
        catch (Exception Ex)
        {
            lblMensaje.Text = Ex.Message;
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        txtCodigo.Enabled = true;
        txtCodigo.Text = string.Empty;
        txtCiudad.Text = string.Empty;
        txtPais.Text = string.Empty;
        txtCiudad.Enabled = false;
        txtPais.Enabled = false;
        txtCodigo.Focus();
        btnAgregar.Enabled = false;
        btnModificar.Enabled = false;
        btn.Enabled = false;
        btnBuscar.Enabled = true;
    }
}