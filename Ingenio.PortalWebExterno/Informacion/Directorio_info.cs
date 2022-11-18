using Ingenio.PortalWebExterno.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ingenio.PortalWebExterno.Informacion
{
    public class Directorio_info
    {
        public List<Telefono> ObtenerTelefonos()
        {
            return new List<Telefono>()
            {
                new Telefono()
                {
                    Id = "D_Gere",
                    Nombre = "Dra. Esperanza Rojas de Bastidas",
                    Cargo = "Gerencia General",
                    Extensiones = "249",
                    Celular = "",
                },
                new Telefono()
                {
                    Id = "Pasto",
                    Nombre = "Dra. Esperanza Rojas de Bastidas",
                    Cargo = "Cra 29 No 18–41",
                    Extensiones = "249",
                    Celular = "",
                },
            };
        }
    }
}