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


public partial class ListarPrestamosVencidos : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            List<Prestamo> oVencidos = LogicaPrestamo.ListarPrestamosVencidos();

            foreach (Prestamo p in oVencidos)
            {
                lstPrestamos.Items.Add(p.ToString());
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}

