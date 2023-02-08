using System.Web;

namespace BotComers.ViewModels.CentroEntrenamiento
{
    public class CentroEntrenamiento_ConfiguracionViewModel
    {
        public int CodigoUnidadNegocio { get; set; }
        public int CodigoSede { get; set; }
        public string UrlUmagen { get; set; }
        public HttpPostedFileWrapper file { get; set; }

        public string Ticket_RazonSocial { get; set; }
        public string Ticket_RUC { get; set; }
        public string Ticket_Direccion { get; set; }
        public string Ticket_Celular { get; set; }
        public string Ticket_Telefono { get; set; }
        public string Ticket_Frase { get; set; }
    }
}