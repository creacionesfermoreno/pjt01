using E_DataModel.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_DataModel.Gimnasio
{
    public class EmailCampaingDTO : AuditoriaDTO
    {
        public string CodigoCorreoCampania { get; set; }
        public string NombreCorreoCampania { get; set; }
        public string UrlDestinatarios { get; set; }
        public string Content_html { get; set; }
        public bool EstadoCorreoCampania { get; set; }
        public bool SendCorreo { get; set; }
        public string Action { get; set; }
        public Common.Operation Operation { get; set; }
        public string DateParse(DateTime? date = null)
        {
            if (date != null)
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

        //files
        public string UrlArchivosAdjunto { get; set; }
        public int CodigoCorreoCampaniaArchivosAdjuntos { get; set; }


        //detail
        public string CodigoCorreoCampaniaDetalle { get; set; }
        public string Destinatario { get; set; }


        //*************************END CAMPAÑA***********************

    }


    public class ReqEmailCampaingDTO : Request
    {
        public List<EmailCampaingDTO> List { get; set; }
    }

    public class ReqFilterEmailCampaingDTO : Request
    {
        public Common.Paging Paging { get; set; }

        public EmailCampaingDTO Item { get; set; }

        public Common.FilterEmailCampaing FilterCase { get; set; }
    }


    public class RespEmailCampaingDTO : Response
    {

    }

    public class RespItemEmailCampaingDTO : Response
    {
        public EmailCampaingDTO Item { get; set; }
    }


    public class RespListEmailCampaingDTO : Response
    {
        public List<EmailCampaingDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }
}
