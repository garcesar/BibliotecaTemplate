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


using EntidadesCOmpartidas;


public partial class ABMPublicacionDigital : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
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
       
    }

    protected void BtnModificar_Click(object sender, EventArgs e)
    {

    }

    protected void btnBaja_Click(object sender, EventArgs e)
    {
        
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        this.LimpioFormulario();
        this.DesactivoBotones();

    }
}