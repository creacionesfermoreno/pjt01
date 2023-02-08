using System.Web;

namespace BotComers.ViewModels.CentroEntrenamiento
{
    public class CentroEntrenamiento_EditorPaginaWebViewModel
    {
        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
        public string UrlUmagen { get; set; }
        public string LinkPago { get; set; }
        public HttpPostedFileWrapper file { get; set; }
    }
}