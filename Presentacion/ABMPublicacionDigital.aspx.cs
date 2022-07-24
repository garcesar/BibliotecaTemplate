using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Logica;
using EntidadesCOmpartidas;


public partial class ABMPublicacionDigital : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
        {
            this.LimpioFormulario();
            this.DesactivoBotones();
        }
    }

    private void LimpioFormulario()
    {
        txtFormato.Text = "";
        txtISBN.Text = "";
        txtTitulo.Text = "";
        chkProtegida.Checked = false;
    }

    private void DesactivoBotones()
    {
        btnAgregar.Enabled = false;
        BtnModificar.Enabled = false;
        btnBaja.Enabled = false;

        btnBuscar.Enabled = true;
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        int isbn = 0;

        try
        {
            isbn = Convert.ToInt32(txtISBN.Text);
        }
        catch (Exception)
        {
            lblError.Text = "El isbn no es un numero";
            return;
        }

        try
        {
            Publicacion pub = LogicaPublicaciones.Buscar(isbn);

            if (pub != null)
            {
                if (pub is Digital)
                {
                    txtTitulo.Text = pub.Titulo;
                    txtFormato.Text = ((Digital)pub).Formato;
                    chkProtegida.Checked = ((Digital)pub).Protegida;

                    Session["UnaPubDigital"] = pub;

                    btnBaja.Enabled = true;
                    BtnModificar.Enabled = true;
                    txtISBN.Enabled = false;
                }
                else
                {
                    lblError.Text = "La publicación no es Digital";
                    this.LimpioFormulario();
                }
            }
            else
            {
                btnAgregar.Enabled = true;
                txtISBN.Enabled = false;

                Session["unaPubDigital"] = null;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void BtnModificar_Click(object sender, EventArgs e)
    {
        Digital d = (Digital)Session["UnaPubDigital"];
        d.Titulo = txtTitulo.Text;
        d.Formato = txtFormato.Text;
        d.Protegida = chkProtegida.Checked;

        try
        {
            LogicaPublicaciones.Modificar(d);
            lblError.Text = "Modificación Éxitosa";

            this.LimpioFormulario();
            this.DesactivoBotones();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void btnBaja_Click(object sender, EventArgs e)
    {
        try
        {
            Digital d = (Digital)Session["unaPubDigital"];

            LogicaPublicaciones.Eliminar(d);
            lblError.Text = "Eliminación Éxitosa";

            this.LimpioFormulario();
            this.DesactivoBotones();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            throw;
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        string oMensaje = "";
        int isbn = 0;

        try
        {
            isbn = Convert.ToInt32(txtISBN.Text);
        }
        catch
        {
            oMensaje = "El isbn debe ser un numero";
        }

        string titulo = txtTitulo.Text;
        string formato = txtFormato.Text;
        bool protegida = chkProtegida.Checked;

        if (oMensaje != "")
        {
            lblError.Text = oMensaje;
        }
        else
        {
            Digital d = new Digital(isbn, titulo, formato, protegida);

            try
            {
                LogicaPublicaciones.Agregar(d);

                lblError.Text = "Alta con Éxito";

                this.LimpioFormulario();
                this.DesactivoBotones();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        this.LimpioFormulario();
        this.DesactivoBotones();
    }
}