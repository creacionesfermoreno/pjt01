using System;
using System.Web;

namespace BotComers.ViewModels.CentroEntrenamiento
{
    public class CentroEntrenamiento_GaleriaFitnessViewModel
    {
        public string Codigo { get; set; }
        public int Tipo { get; set; }
        public int Privacidad { get; set; }
        public string UrlImagen { get; set; }
        public Boolean Estado { get; set; }
        public HttpPostedFileWrapper file { get; set; }
    }
}