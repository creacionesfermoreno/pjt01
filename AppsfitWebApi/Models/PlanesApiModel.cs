using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppsfitWebApi.Models
{
    public class PlanesApiModel
    {
        public int CodigoPaquete { get; set; }
        public string DesTiempoPlan { get; set; }
        public string Descripcion { get; set; }
        public int CodigoTipoPaquete { get; set; }
        public string DesTipoPaquete { get; set; }
        public bool EstadoMembresiaInterdiaria { get; set; }
        public int CongelamientoVigente { get; set; }
        public int ValorTiempoPlan { get; set; }
        public int ValorSesiones { get; set; }
        public decimal Costo { get; set; }
        public int NroCupo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string DesNomProfesor { get; set; }
        public string UrlImage { get; set; }
        public bool Suscripcion { get; set; }
    }
}
