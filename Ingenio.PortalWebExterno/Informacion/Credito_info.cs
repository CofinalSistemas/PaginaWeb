using Ingenio.PortalWebExterno.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ingenio.PortalWebExterno.Models
{
    public class Credito_info
    {
        public List<Credito> ObtenerCreditos()
        {
            return new List<Credito>
            {
                new Credito()
                {
                    Id = "#creditoEducativo",
                    Nombre = "Credito Educativo",
                    Texto = "",
                    Caracteristicas = "",
                    Img = "",
                    Icono = "EDUCATIVO.png",
                },
                new Credito()
                {
                    Id = "#creditoVivienda",
                    Nombre = "",
                    Texto = "",
                    Caracteristicas = "",
                    Img = "",
                    Icono = "VIVIENDA.png",
                },
                new Credito()
                {
                    Id = "#creditoCartera",
                    Nombre = "",
                    Texto = "",
                    Caracteristicas = "",
                    Img = "",
                    Icono = "CARTERA.png",
                },
                new Credito()
                {
                    Id = "#creditoLibranza",
                    Nombre = "Credito Educativo",
                    Texto = "",
                    Caracteristicas = "",
                    Img = "",
                    Icono = "LIBRANZA.png",
                },
                new Credito()
                {
                    Id = "#creditoConsumo",
                    Nombre = "",
                    Texto = "",
                    Caracteristicas = "",
                    Img = "",
                    Icono = "CONSUMO.png",
                },
                new Credito()
                {
                    Id = "#microcredito",
                    Nombre = "",
                    Texto = "",
                    Caracteristicas = "",
                    Img = "",
                    Icono = "MICROCRE.png",
                },
                new Credito()
                {
                    Id = "#rotativos",
                    Nombre = "Credito Educativo",
                    Texto = "",
                    Caracteristicas = "",
                    Img = "",
                    Icono = "ROTATIVO.png",
                },
                new Credito()
                {
                    Id = "#cofinagro",
                    Nombre = "",
                    Texto = "",
                    Caracteristicas = "",
                    Img = "",
                    Icono = "cofinagro.png",
                },
            };
        }
    }
}