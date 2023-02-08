using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
using System.Web;

namespace E_DataModel.CentroEntrenamiento
{
    public class CentroEntrenamiento_MenuPlantillaDTO : AuditoriaDTO
    {
         public string  CodigoMenu          { get; set; } 
         public string  CodigoMenuSuperior  { get; set; }
         public string  Descripcion         { get; set; }
         public string  Observacion         { get; set; }
         public string  IdControl           { get; set; }
         public string  Url                 { get; set; }
         public int  Orden                  { get; set; }
         public Boolean Estado { get; set; }
         public Common.Operation Operation { get; set; }

        public int Planes_CodigoPlan { get; set; }
        public string Planes_Titulo { get; set; }

        public string Planes_TituloCompleto { get; set; }
        public string Planes_Descripcion { get; set; }

        public Decimal Planes_PrecioPEN { get; set; }
        public Decimal Planes_PrecioDolares { get; set; }
        public Decimal Planes_PrecioEuros { get; set; }

        public Decimal Planes_PrecioPEN_Promo { get; set; }
        public Decimal Planes_PrecioDolares_Promo { get; set; }
        public Decimal Planes_PrecioEuros_Promo { get; set; }

        public int Planes_CantidadUsuarios { get; set; }

        public Boolean EstadoMenuPlan { get; set; }
        public int CodigoPerfil { get; set; }

    }


    public class ReqCentroEntrenamiento_MenuPlantillaDTO : Request
    {
        public List<CentroEntrenamiento_MenuPlantillaDTO> List { get; set; }
    }

    public class ReqFilterCentroEntrenamiento_MenuPlantillaDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public CentroEntrenamiento_MenuPlantillaDTO Item { get; set; }
        public Common.filterCaseCentroEntrenamiento_MenuPlantilla FilterCase { get; set; }
    }

    public class RespCentroEntrenamiento_MenuPlantillaDTO : Response
    {

    }

    public class RespItemCentroEntrenamiento_MenuPlantillaDTO : Response
    {
        public CentroEntrenamiento_MenuPlantillaDTO Item { get; set; }
    }

    public class RespListCentroEntrenamiento_MenuPlantillaDTO : Response
    {
        public List<CentroEntrenamiento_MenuPlantillaDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }




}
