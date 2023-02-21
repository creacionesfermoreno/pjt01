using E_DataModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_DataModel.Gimnasio
{
    public class PasarelaEmpresaDTO : AuditoriaDTO
    {
        public string CodigoPlantillaFormaPago { get; set; }
        public string Valor1 { get; set; }
        public string Valor2 { get; set; }
        public string Valor3 { get; set; }
        public bool Estado { get; set; }
        public bool EstadoProduccion { get; set; }
        public string DesFormaPago { get; set; }
        public string UrlImagenFormaPago { get; set; }

        public string DesFechaCreacion { get; set; }
        public string MonedaSistema { get; set; }

        public Common.Operation Operation { get; set; }
        public string DateParse(DateTime? date = null)
        {
            if (date != null)
            {
                return date?.ToString("dd/MM/yyyy HH:mm tt");
            }
            return "";
        }
    }


    public class ReqPasarelaEmpresaDTO : Request
    {
        public List<PasarelaEmpresaDTO> List { get; set; }
    }

    public class ReqFilterPasarelaEmpresaDTO : Request
    {
        public Common.Paging Paging { get; set; }

        public PasarelaEmpresaDTO Item { get; set; }

        public Common.FilterCasePasarelaEmpresa FilterCase { get; set; }
    }


    public class RespPasarelaEmpresaDTO : Response
    {

    }

    public class RespItemPasarelaEmpresaDTO : Response
    {
        public PasarelaEmpresaDTO Item { get; set; }
    }


    public class RespListPasarelaEmpresaDTO : Response
    {
        public List<PasarelaEmpresaDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }
}
