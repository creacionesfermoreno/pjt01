
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.CentroEntrenamiento
{
    public class CentroEntrenamiento_Presencial_SalaDTO : AuditoriaDTO
    {
        //TIPO SALA 1 = SALAS GRUPALES, 2 SALA MAQUINA
        public int TipoSala { get; set; }
        public int CodigoSala { get; set; }
        public string Descripcion { get; set; }
        public int NroSala { get; set; }
        public string Color { get; set; }
        public bool Estado { get; set; }
        public Common.Operation Operation { get; set; }
        public string Accion { get; set; }
    }

    public class ReqCentroEntrenamiento_Presencial_SalaDTO : Request //Peticion de un CRUD
    {
        public List<CentroEntrenamiento_Presencial_SalaDTO> List { get; set; }
    }

    public class ReqFilterCentroEntrenamiento_Presencial_SalaDTO : Request //Peticion de un List o Items
    {
        public Common.Paging Paging { get; set; }
        public CentroEntrenamiento_Presencial_SalaDTO Item { get; set; }
        public Common.filterCaseCentroEntrenamiento_Presencial_Sala FilterCase { get; set; }
    }

    public class RespCentroEntrenamiento_Presencial_SalaDTO : Response //respuesta de un CRUD
    {

    }

    public class RespItemCentroEntrenamiento_Presencial_SalaDTO : Response
    {
        public CentroEntrenamiento_Presencial_SalaDTO Item { get; set; }
    } //respuesta de un ITEM

    public class RespListCentroEntrenamiento_Presencial_SalaDTO : Response
    {
        public List<CentroEntrenamiento_Presencial_SalaDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    } //respuesta de un LIST

}
