using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Base;

namespace E_DataModel
{
    public class TarifasEnvioDTO : AuditoriaDTO
    {
       public string CodigoTarifaEnvio { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public string Ubigeo { get; set; }
        public int TipoUbigeo { get; set; }
        public int CodigoUbigeo { get; set; }
        public decimal PrecioEnvio { get; set; }
       public int TiempoEntrega { get; set; }
       public int TipoTiempoEntrega { get; set; }
       public Boolean Estado { get; set; }
        public Common.Operation Operation { get; set; }
        public string Accion { get; set; }
    }

    public class ReqTarifasEnvioDTO : Request
    {
        public List<TarifasEnvioDTO> List { get; set; }
    }

    public class ReqFilterTarifasEnvioDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public TarifasEnvioDTO Item { get; set; }
        public Common.filterCaseTarifasEnvio FilterCase { get; set; }
    }

    public class RespTarifasEnvioDTO : Response
    {

    }

    public class RespItemTarifasEnvioDTO : Response
    {
        public TarifasEnvioDTO Item { get; set; }
    }

    public class RespListTarifasEnvioDTO : Response
    {
        public List<TarifasEnvioDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }

}
