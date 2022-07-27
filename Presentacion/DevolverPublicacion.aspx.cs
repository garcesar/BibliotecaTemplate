using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Logica;
using EntidadesCOmpartidas;



public partial class DevolverPublicacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.CargoDatos();
        }   
    }

    private void CargoDatos()
    {
        try
        {
            List<Prestamo> oPrestamosActivos = LogicaPrestamo.ListarPrestamosNoDevueltos();

            cboPrestamos.DataSource = oPrestamosActivos;
            cboPrestamos.DataTextField = "Desplegar";
            cboPrestamos.DataValueField = "Numero";
            cboPrestamos.DataBind();

            Session["Prestamos"] = oPrestamosActivos;
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
 
    protected void btnDevolver_Click(object sender, EventArgs e)
    {
        try
        {
            int numero = Convert.ToInt32(cboPrestamos.SelectedValue);

            if(Session["Prestamos"] != null && ((List<Prestamo>)Session["Prestamos"]).Count > 0)
            {
                Prestamo p = ((List<Prestamo>)Session["Prestamos"]).Find(x => x.Numero == numero);
                LogicaPrestamo.Devolver(p);

                lblError.Text = "Se devolvio correctamente el prestamo numero: " + numero;

                this.CargoDatos();
            }
            else
            {
                throw new Exception("No hay prestamos registrados para devolver. ");
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}
