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


public partial class IngresarPrestamo : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<Publicacion> oPublicaciones = LogicaPublicaciones.ListarPublicaciones();

            if (oPublicaciones.Count > 0)
            {
                cboPublicaciones.DataSource = oPublicaciones;
                cboPublicaciones.DataTextField = "Titulo";
                cboPublicaciones.DataValueField = "ISBN";
                cboPublicaciones.DataBind();

                Session["Publicaciones"] = oPublicaciones;
            }
            else
            {
                lblError.Text = "Error: no existe ninguna publicación. Debe ingresar una primero.";
                txtDias.Enabled = false;
                txtUsuario.Enabled = false;
                btnAgregar.Enabled = false;
                cboPublicaciones.Enabled = false;
            }
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Publicacion pub1 = null;
        string oMensaje = "";
        string usuario = txtUsuario.Text;
        int isbn = 0;
        int dias = 0;

        try
        {
            isbn = Convert.ToInt32(cboPublicaciones.SelectedValue);
            dias = Convert.ToInt32(txtDias);
        }
        catch
        {
            oMensaje = "El isbn no es un numero. ";
        }

        if (oMensaje != "")
        {
            lblError.Text = oMensaje;
        }
        else
        {
            try
            {
                //Publicacion pub = LogicaPublicaciones.Buscar(isbn);
                if (Session["Publicaciones"] != null)
                {
                    //Opcion 1
                    List<Publicacion> lista = (List<Publicacion>) Session["Publicaciones"];
                    foreach (Publicacion p in lista)
                    {
                        if (p.ISBN == isbn)
                            pub1 = p;
                    }

                    //Opcion2
                    Publicacion pub = ((List<Publicacion>)Session["Publicaciones"]).Find(x => x.ISBN == isbn);
                    Prestamo pre = new Prestamo(0, clnFechaPrestado.SelectedDate, dias, usuario, false, pub);

                    int numero = LogicaPrestamo.Agregar(pre);
                    lblError.Text = "Alta con Éxito del Prestamo N° " + numero.ToString();

                    txtUsuario.Text = "";
                    txtDias.Text = "";
                }
                else
                {
                    lblError.Text = "Error: no existe ninguna publicación. Debe ingresar una primero. ";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}