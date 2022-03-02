using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ingenio.PortalWebExterno
{
    public partial class WebF1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            ObjectDataSource2.SelectParameters["cedulasociado"].DefaultValue = Request.QueryString["cedula"];
            ObjectDataSource2.SelectParameters["fechainicio"].DefaultValue = Request.QueryString["fechai"];
            ObjectDataSource2.SelectParameters["fechafinal"].DefaultValue = Request.QueryString["fechaf"] + " 23:59";

            ObjectDataSource3.SelectParameters["cedulasociado"].DefaultValue = Request.QueryString["cedula"];
            ObjectDataSource3.SelectParameters["fechainicio"].DefaultValue = Request.QueryString["fechai"];
            ObjectDataSource3.SelectParameters["fechafinal"].DefaultValue = Request.QueryString["fechaf"] + " 23:59";

            ObjectDataSource1.SelectParameters["CEDULA"].DefaultValue = Request.QueryString["cedula"];
            ObjectDataSource1.SelectParameters["TIPO"].DefaultValue = "1";
            ObjectDataSource1.SelectParameters["FECHAINICIAL"].DefaultValue = Request.QueryString["fechai"];
            ObjectDataSource1.SelectParameters["FECHAFINAL"].DefaultValue = Request.QueryString["fechaf"] + " 23:59";

            ObjectDataSource4.SelectParameters["CEDULA"].DefaultValue = Request.QueryString["cedula"];
            ObjectDataSource4.SelectParameters["TIPO"].DefaultValue = "2";
            ObjectDataSource4.SelectParameters["FECHAINICIAL"].DefaultValue = Request.QueryString["fechai"];
            ObjectDataSource4.SelectParameters["FECHAFINAL"].DefaultValue = Request.QueryString["fechaf"] + " 23:59";

            ObjectDataSource5.SelectParameters["CEDULA"].DefaultValue = Request.QueryString["cedula"];
            ObjectDataSource5.SelectParameters["TIPO"].DefaultValue = "3";
            ObjectDataSource5.SelectParameters["FECHAINICIAL"].DefaultValue = Request.QueryString["fechai"];
            ObjectDataSource5.SelectParameters["FECHAFINAL"].DefaultValue = Request.QueryString["fechaf"] + " 23:59";

            //ObjectDataSource6.SelectParameters["CEDULA"].DefaultValue = Request.QueryString["cedula"];
            //ObjectDataSource6.SelectParameters["TIPO"].DefaultValue = "4";
            //ObjectDataSource6.SelectParameters["FECHAINICIAL"].DefaultValue = Request.QueryString["fechai"];
            //ObjectDataSource6.SelectParameters["FECHAFINAL"].DefaultValue = Request.QueryString["fechaf"];

            ObjectDataSource7.SelectParameters["CEDULA"].DefaultValue = Request.QueryString["cedula"];
            ObjectDataSource7.SelectParameters["TIPO"].DefaultValue = "5";
            ObjectDataSource7.SelectParameters["FECHAINICIAL"].DefaultValue = Request.QueryString["fechai"];
            ObjectDataSource7.SelectParameters["FECHAFINAL"].DefaultValue = Request.QueryString["fechaf"] + " 23:59";

            ObjectDataSource8.SelectParameters["CEDULA"].DefaultValue = Request.QueryString["cedula"];
            ObjectDataSource8.SelectParameters["TIPO"].DefaultValue = "6";
            ObjectDataSource8.SelectParameters["FECHAINICIAL"].DefaultValue = Request.QueryString["fechai"];
            ObjectDataSource8.SelectParameters["FECHAFINAL"].DefaultValue = Request.QueryString["fechaf"] + " 23:59";

            ObjectDataSource9.SelectParameters["CEDULA"].DefaultValue = Request.QueryString["cedula"];
            ObjectDataSource9.SelectParameters["TIPO"].DefaultValue = "7";
            ObjectDataSource9.SelectParameters["FECHAINICIAL"].DefaultValue = Request.QueryString["fechai"];
            ObjectDataSource9.SelectParameters["FECHAFINAL"].DefaultValue = Request.QueryString["fechaf"] + " 23:59";

            ObjectDataSource10.SelectParameters["CEDULA"].DefaultValue = Request.QueryString["cedula"];
            ObjectDataSource10.SelectParameters["TIPO"].DefaultValue = "8";
            ObjectDataSource10.SelectParameters["FECHAINICIAL"].DefaultValue = Request.QueryString["fechai"];
            ObjectDataSource10.SelectParameters["FECHAFINAL"].DefaultValue = Request.QueryString["fechaf"] + " 23:59";

            ObjectDataSource12.SelectParameters["cedulasociado"].DefaultValue = Request.QueryString["cedula"];
            ObjectDataSource12.SelectParameters["fechainicio"].DefaultValue = Request.QueryString["fechai"];
            ObjectDataSource12.SelectParameters["fechafinal"].DefaultValue = Request.QueryString["fechaf"] + " 23:59";

            ObjectDataSource14.SelectParameters["CEDULA"].DefaultValue = Request.QueryString["cedula"];
            ObjectDataSource14.SelectParameters["TIPO"].DefaultValue = "4";
            ObjectDataSource14.SelectParameters["FECHAINICIAL"].DefaultValue = Request.QueryString["fechai"];
            ObjectDataSource14.SelectParameters["FECHAFINAL"].DefaultValue = Request.QueryString["fechaf"] + " 23:59";
        }
    }
}