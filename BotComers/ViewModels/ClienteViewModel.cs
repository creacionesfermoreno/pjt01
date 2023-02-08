using System.Web;

namespace BotComers.ViewModels
{
    public class ClienteViewModel
    {
        public int CodigoSocio { get; set; }
        public string SubDominio { get; set; }
        public string ImageFile_texto { get; set; }
        public HttpPostedFileWrapper ImageFile { get; set; }
    }
}