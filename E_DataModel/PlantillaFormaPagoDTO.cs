using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Base;

namespace E_DataModel
{
    public class PlantillaFormaPagoDTO:AuditoriaDTO
    {
       public string CodigoPlantillaFormaPago { get; set; }
       public int? Codigo { get; set; }
       public string Descripcion { get; set; }
       public string Observacion { get; set; }
       public int Estado { get; set; }
       public string UrlImagen { get; set; }

        public string MercadoPago_Publickey { get; set; }
        public string MercadoPago_Accesstoken { get; set; }
        public Boolean? MercadoPago_Estado { get; set; }

        public string Yape_NroCelular { get; set; }
        public string Yape_CodigoQR { get; set; }
        public Boolean? Yape_Estado { get; set; }

        public string ContraEntrega_InstruccionesCorreo { get; set; }
        public string ContraEntrega_InstruccionesCheckout { get; set; }
        public Boolean? ContraEntrega_Estado { get; set; }

        public Common.Operation Operation { get; set; }

    }

    public class ReqPlantillaFormaPagoDTO : Request
    {
        public List<PlantillaFormaPagoDTO> List { get; set; }
    }

    public class ReqFilterPlantillaFormaPagoDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public PlantillaFormaPagoDTO Item { get; set; }
        public Common.filterCasePlantillaFormaPago FilterCase { get; set; }
    }

    public class RespPlantillaFormaPagoDTO : Response
    {

    }

    public class RespItemPlantillaFormaPagoDTO : Response
    {
        public PlantillaFormaPagoDTO Item { get; set; }
    }

    public class RespListPlantillaFormaPagoDTO : Response
    {
        public List<PlantillaFormaPagoDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }
}
