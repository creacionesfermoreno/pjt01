using E_DataModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
namespace E_DataModel.Gimnasio
{
  

    public class WhatsappConfigDTO : AuditoriaDTO
    {
        public int CodigoEntidadNegocio { get; set; }
        public string CodigoWhatsappConfiguracion { get; set; }
        public string IdentificadorApp { get; set; }
        public string IdAccount { get; set; }
        public string IdPhone { get; set; }
        public string Token { get; set; }
        public string SDK { get; set; }
        public string NumberPhone { get; set; }
        public string DesDateCreate { get; set; }
        public string DateGlobalization { get; set; }
        public bool Estado { get; set; }
        public Common.Operation Operation { get; set; }
        public string DateParse(DateTime? date = null)
        {
            if(date != null)
            {
                return date?.ToString("dd/MM/yyyy HH:mm tt");
            }
            return "";
        }
        

        public string DateParseT(DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm tt");
        }
        //date globalization es
        public string DateParseText(DateTime date)
        {
            CultureInfo esC = new CultureInfo("es-ES");
            return date.ToString("f", esC);
        }


        //************************* START CAMPAÑA***********************

        public string CodigoWhatsappCampania { get; set; }
        public string NombreWhatsappCampania { get; set; }
        public string UrlDestinatarios { get; set; }
        public string IdTemplate { get; set; }
        public string NameTemplate { get; set; }
        public string Languaje { get; set; }
        public int ColaDestino { get; set; }
        public int TiempoRespuesta { get; set; }
        public string DesColaDestino { get; set; }
        public bool EstadoWhatsappCampania { get; set; }
        public string TypeHeader { get; set; }
        public string ParametersHeader { get; set; }
        public string ParametersBody { get; set; }
        public DateTime? FechaHoraProgramado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string DescFechaHoraProgramado { get; set; }
        public int Total { get; set; }
        public int TotalEnviado { get; set; }
        public int TotalError { get; set; }

        //detail
        public string CodigoWhatsappCampaniaDetalle { get; set; }
        public string Destinatario { get; set; }
        public string Phone { get; set; }
        
        //*************************END CAMPAÑA***********************

    }


    public class ReqWhatsappConfigDTO : Request
    {
        public List<WhatsappConfigDTO> List { get; set; }
    }

    public class ReqFilterWhatsappConfigDTO : Request
    {
        public Common.Paging Paging { get; set; }

        public WhatsappConfigDTO Item { get; set; }

        public Common.FilterCaseGlobal FilterCase { get; set; }
    }


    public class RespWhatsappConfigDTO : Response
    {

    }

    public class RespItemWhatsappConfigDTO : Response
    {
       public WhatsappConfigDTO Item { get; set; }
    }


    public class RespListWhatsappConfigDTO : Response
    {
        public List<WhatsappConfigDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }

}
