using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Base;

namespace E_DataModel
{
    public class EnvioGratisDTO: AuditoriaDTO
    {
        public decimal  Valor { get; set; }
        public DateTime  FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public Boolean Estado { get; set; }
        public string FechaInicioTexto
        {
            get
            {
                if (FechaInicio != null)
                {
                    return FechaInicio.ToString("dd/MM/yyyy");
                }
                else
                {
                    return string.Empty;
                }
                
            }
        }

        public string FechaFinTexto
        {
            get
            {
                if (FechaFin != null)
                {
                    return FechaFin.ToString("dd/MM/yyyy");
                }
                else
                {
                    return string.Empty;
                }

            }
        }
        public Common.Operation Operation { get; set; }
    }


    public class ReqEnvioGratisDTO : Request
    {
        public List<EnvioGratisDTO> List { get; set; }
    }

    public class ReqFilterEnvioGratisDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public EnvioGratisDTO Item { get; set; }
        public Common.filterCaseEnvioGratis FilterCase { get; set; }
    }

    public class RespEnvioGratisDTO : Response
    {

    }

    public class RespItemEnvioGratisDTO : Response
    {
        public EnvioGratisDTO Item { get; set; }
    }

    public class RespListEnvioGratisDTO : Response
    {
        public List<EnvioGratisDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }

}
