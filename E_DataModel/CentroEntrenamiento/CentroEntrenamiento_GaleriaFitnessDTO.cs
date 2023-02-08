using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
using System.Web;

namespace E_DataModel.CentroEntrenamiento
{
    public class CentroEntrenamiento_GaleriaFitnessDTO : AuditoriaDTO
    {
       public string Codigo     { get; set; }
       public int Tipo       { get; set; }
       public int Privacidad { get; set; }
       public string UrlImagen  { get; set; }
       public Boolean Estado { get; set; }
       public Common.Operation Operation { get; set; }
    }


    public class ReqCentroEntrenamiento_GaleriaFitnessDTO : Request
    {
        public List<CentroEntrenamiento_GaleriaFitnessDTO> List { get; set; }
    }

    public class ReqFilterCentroEntrenamiento_GaleriaFitnessDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public CentroEntrenamiento_GaleriaFitnessDTO Item { get; set; }
        public Common.filterCaseCentroEntrenamiento_GaleriaFitness FilterCase { get; set; }
    }

    public class RespCentroEntrenamiento_GaleriaFitnessDTO : Response
    {

    }

    public class RespItemCentroEntrenamiento_GaleriaFitnessDTO : Response
    {
        public CentroEntrenamiento_GaleriaFitnessDTO Item { get; set; }
    }

    public class RespListCentroEntrenamiento_GaleriaFitnessDTO : Response
    {
        public List<CentroEntrenamiento_GaleriaFitnessDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }



}
