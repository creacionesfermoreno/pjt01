using E_DataModel.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace E_DataModel.Gimnasio
{

    public class NotificacionDTO : AuditoriaDTO
    {
        public string CodigoNotificacionesApp { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }
        public string UrlImage { get; set; }
        public string DescriptionHtml { get; set; }
        public string Group { get; set; }
        public int TipeNoty { get; set; }
        public bool Recurrent { get; set; }
        public bool Send { get; set; }
        public Common.Operation Operation { get; set; }


        public string DateParse(DateTime date)
        {

            return date.ToString("dd/MM/yyyy HH:mm tt");
        }


        //for user token devices
        public string TokenDevice { get; set; }
        public string IdUser { get; set; }
        public int Days { get; set; }

        //details
        public int CodigoNotificacionesAppDestinatarios { get; set; }
        public bool Read { get; set; }
        public string FullName { get; set; }
        
        
        


    }

    public class ReqNotificacionDTO : Request
    {
        public List<NotificacionDTO> List { get; set; }
    }

    public class ReqFilterNotificacionDTO : Request
    {

        public Common.Paging Paging { get; set; }

        public NotificacionDTO Item { get; set; }

        public Common.filterCaseNotificacionApp FilterCase { get; set; }
    }






    public class RespNotificacionDTO : Response
    {

    }

    public class RespItemNotificacionDTO : Response
    {

        public NotificacionDTO Item { get; set; }
    }


    public class RespListNotificacionDTO : Response
    {

        public List<NotificacionDTO> List { get; set; }

        public Common.Paging Paging { get; set; }
    }



}
