
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.CentroEntrenamiento
{
    public class CentroEntrenamiento_Presencial_DisciplinaSalaDTO : AuditoriaDTO
    {
        public int CodigoSala { get; set; }
        public int CodigoDisciplinaSala { get; set; }
        public string Disciplina { get; set; }
        public int Capacidad { get; set; }
        public int Orden { get; set; }
        public string Color { get; set; }
        public bool Estado { get; set; }
        public Common.Operation Operation { get; set; }
        public string Accion { get; set; }
    }


    public class ReqCentroEntrenamiento_Presencial_DisciplinaSalaDTO : Request 
    {
        public List<CentroEntrenamiento_Presencial_DisciplinaSalaDTO> List { get; set; }
    }

    public class ReqFilterCentroEntrenamiento_Presencial_DisciplinaSalaDTO : Request 
    {
        public Common.Paging Paging { get; set; }
        public CentroEntrenamiento_Presencial_DisciplinaSalaDTO Item { get; set; }
        public Common.filterCaseCentroEntrenamiento_Presencial_DisciplinaSala FilterCase { get; set; }
    }

    public class RespCentroEntrenamiento_Presencial_DisciplinaSalaDTO : Response 
    {

    }

    public class RespItemCentroEntrenamiento_Presencial_DisciplinaSalaDTO : Response
    {
        public CentroEntrenamiento_Presencial_DisciplinaSalaDTO Item { get; set; }
    } 

    public class RespListCentroEntrenamiento_Presencial_DisciplinaSalaDTO : Response
    {
        public List<CentroEntrenamiento_Presencial_DisciplinaSalaDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }



}
