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
       public int CodigoEntidadNegocio { get; set; }

        public string CodigoNotificacionesApp { get; set; }
        public int TipoEnvio { get; set; }
        public string Asunto { get; set; }
        public DateTime FechaHoraEnvio { get; set; }
        public string DesFechaHoraEnvio { get; set; }
        public string Mensaje { get; set; }

        public bool Estado { get; set; }

        public bool Recurrente { get; set; }
        public int GrupoPersonas { get; set; }


        
       public bool Enviado { get; set; }

        public Common.Operation Operation { get; set; }



        public string DateParse(DateTime date)
        {
            
            return date.ToString("dd/MM/yyyy HH:mm tt");
        }

       
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
