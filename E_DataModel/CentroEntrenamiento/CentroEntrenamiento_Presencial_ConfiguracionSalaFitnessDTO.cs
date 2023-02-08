
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.CentroEntrenamiento
{
    public class CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO:AuditoriaDTO
    {

        public string CodigoHorarioClasesConfiguracion { get; set; }
        public string CodigoConfiguracionSalaFitness { get; set; }
        public int CodigoSala { get; set; }
        public int DiaNumero{ get; set; }
        public string DiaNombre{ get; set; }
        public DateTime HoraInicio{ get; set; }
        public DateTime HoraFin { get; set; }

        public string HoraInicioTexto
        {
            get
            {
                return HoraInicio.ToString("hh:mm tt");
            }
        }

        public string HoraFinTexto
        {
            get
            {
                return HoraFin.ToString("hh:mm tt");
            }
        }

        public int Tiempo { get; set; }
        public int Minutos{ get; set; }
        public int CapacidadPermitida { get; set; }
        public bool Estado { get; set; }
        public Common.Operation Operation { get; set; }

        public int NroHorarios { get; set; }
        public int AforoxHorario { get; set; }

        public string Accion { get; set; }

        public List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> lista { get; set; }
    }



    public class ReqCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO : Request
    {
        public List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> List { get; set; }
    }

    public class ReqFilterCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO Item { get; set; }
        public Common.filterCaseCentroEntrenamiento_Presencial_ConfiguracionSalaFitness FilterCase { get; set; }
    }

    public class RespCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO : Response
    {

    }

    public class RespItemCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO : Response
    {
        public CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO Item { get; set; }
    }

    public class RespListCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO : Response
    {
        public List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }
}
