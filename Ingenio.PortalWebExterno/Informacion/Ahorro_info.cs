using Ingenio.PortalWebExterno.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ingenio.PortalWebExterno.Informacion
{
    public class Ahorro_info
    {
        public List<Ahorro> ObtenerAhorro()
        {
            return new List<Ahorro>
            {
                new Ahorro()
                {
                    Id = "#creditoEducativo",
                    Nombre = "Credito Educativo",
                    Texto = "",
                    Caracteristicas = "",
                    Img = "",
                    Icono = "EDUCATIVO.png",
                },
                new Ahorro()
                {
                    Id = "#creditoVivienda",
                    Nombre = "",
                    Texto = "",
                    Caracteristicas = "",
                    Img = "",
                    Icono = "VIVIENDA.png",
                },
                new Ahorro()
                {
                    Id = "#creditoCartera",
                    Nombre = "",
                    Texto = "",
                    Caracteristicas = "",
                    Img = "",
                    Icono = "CARTERA.png",
                },
                new Ahorro()
                {
                    Id = "#creditoLibranza",
                    Nombre = "Credito Educativo",
                    Texto = "",
                    Caracteristicas = "",
                    Img = "",
                    Icono = "LIBRANZA.png",
                },
                new Ahorro()
                {
                    Id = "#creditoConsumo",
                    Nombre = "",
                    Texto = "",
                    Caracteristicas = "",
                    Img = "",
                    Icono = "CONSUMO.png",
                },
                new Ahorro()
                {
                    Id = "#microcredito",
                    Nombre = "",
                    Texto = "",
                    Caracteristicas = "",
                    Img = "",
                    Icono = "MICROCRE.png",
                },
                new Ahorro()
                {
                    Id = "#rotativos",
                    Nombre = "Credito Educativo",
                    Texto = "",
                    Caracteristicas = "",
                    Img = "",
                    Icono = "ROTATIVO.png",
                },
                new Ahorro()
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