using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Base;

namespace E_DataModel
{
    public class CuponesDTO : AuditoriaDTO
    {
        public string CodigoCupon { get; set; }
        public string CodigoPromocion { get; set; }
        public int TipoCupon { get; set; }
        public string DesTipoCupon { get; set; }
        public decimal Valor { get; set; }
        public DateTime FechaInicio  { get; set; }
        public DateTime FechaFin { get; set; }
        public Common.Operation Operation { get; set; }
        public string Accion { get; set; }

        public string FechaCreacionTexto
        {
            get
            {
                return FechaCreacion.ToString("dd/MM/yyyy");
            }
        }

        public string FechaInicioTexto
        {
            get
            {
                return FechaInicio.ToString("dd/MM/yyyy");
            }
        }

        public string FechaFinTexto
        {
            get
            {
                return FechaFin.ToString("dd/MM/yyyy");
            }
        }
    }


    public class ReqCuponesDTO : Request
    {
        public List<CuponesDTO> List { get; set; }
    }

    public class ReqFilterCuponesDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public CuponesDTO Item { get; set; }
        public Common.filterCaseCupones FilterCase { get; set; }
    }

    public class RespCuponesDTO : Response
    {

    }

    public class RespItemCuponesDTO : Response
    {
        public CuponesDTO Item { get; set; }
    }

    public class RespListCuponesDTO : Response
    {
        public List<CuponesDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }
}
