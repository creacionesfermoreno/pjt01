using E_DataModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace E_DataModel.Gimnasio
{
    
    public class HorarioClasesDetalleDTO : AuditoriaDTO
    {

        
        public string CodigoHorarioClases { get; set; }
        
        public string CodigoHorarioClasesDetalle { get; set; }
        
        public DateTime? FechaHoraReserva { get; set; }
        
        public int NroCupo { get; set; }
        
        public int CodigoSocio { get; set; }
        
        public int CodigoMembresia { get; set; }
        
        public int CodigoPaquete { get; set; }
        
        public int NroSala { get; set; }
        
        public string NombreCompletoSocio { get; set; }
        
        public string DNISocio { get; set; }
        
        public string PhotoSocio { get; set; }
        
        public int CodigoInvitado { get; set; }
        
        public string UsuarioReservacion { get; set; }
        
        public int Estado { get; set; }
        
        public string DescripcionEstado { get; set; }
        
        public Common.Operation Operation { get; set; }
        
        public ClientesDTO Socio { get; set; }
        
        public InvitadosDTO Invitado { get; set; }
        
        public List<ContratoDTO> ListaMembresias { get; set; }

        
        public int TK_ID { get; set; }
        
        public string TK_Latitude { get; set; }
        
        public string TK_Longitude { get; set; }

        
        public string Xml { get; set; }

    }


    public class HorarioClasesDetalleCalendarioDTO
    {
        public List<SalaHorarioDTO> ListaSalas { get; set; }
        public List<HorarioClasesDTO> ListaHorarios { get; set; }
        public bool ExisteHorario { get; set; }
        public List<HorarioClasesConfiguracionDTO> ListaConfiguracion { get; set; }
    }

    
    public class ReqHorarioClasesDetalleDTO : Request
    {
        
        public List<HorarioClasesDetalleDTO> List { get; set; }
    }

    
    public class ReqFilterHorarioClasesDetalleDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public HorarioClasesDetalleDTO Item { get; set; }
        
        public Common.filterCaseHorarioClasesDetalle FilterCase { get; set; }
    }

    
    public class RespHorarioClasesDetalleDTO : Response
    {
        
        public HorarioClasesDetalleDTO Item { get; set; }
    }

    
    public class RespItemHorarioClasesDetalleDTO : Response
    {
        
        public HorarioClasesDetalleDTO Item { get; set; }
        
        public HorarioClasesDetalleCalendarioDTO Data { get; set; }
    }



    
    public class RespListHorarioClasesDetalleDTO : Response
    {
        
        public List<HorarioClasesDetalleDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
}
