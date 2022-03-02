using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using System.Diagnostics;

namespace Ingenio.PortalWebExterno
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // ConfigureAuth(app);
            Trace.WriteLine("Aplicacion PortalWeb ", "Inicio " + DateTime.Now);
        }
    }
}