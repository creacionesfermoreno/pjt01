using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

using E_DataModel.CentroEntrenamiento;

namespace E_DataModel.Gimnasio
{

	public class ContratoDTO : AuditoriaDTO
	{
        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }

        
        public string NroSolicitud { get; set; }
        
        
        public string Motivo { get; set; }
        public string NombreEstado { get; set; }

        
        public int NroDias { get; set; }

       
        public int Tipo { get; set; }
        
        
        public int CodigoMensaje { get; set; }

        
        public int TamanioPagina { get; set; }

        
        public int CodigoDetalle { get; set; }

        
        public int CodigoMenbresia { get; set; }

        
        public int NroIngresoDia { get; set; }

        
        public int CantidadAsistencia { get; set; }

        
        public int CodigoHistorialMenbresia { get; set; }

        
        public string Observacion { get; set; }

        
        public string NombreMembresia { get; set; }

        
        public string FechasDelEstado { get; set; }
        
        
        public string AsesorComercial { get; set; }

        
        public int TipoDescuento { get; set; }

        
        public decimal MontoDescuento { get; set; }
        
		public int CodigoSocio { get; set; }

        
        public string NombreSocio { get; set; }

        
        public string Telefono { get; set; }

        
        public int TipoIngreso { get; set; }

        public string DesTipoIngreso { get; set; }

        public string DesTipoPaquete { get; set; }

        public string desSocio { get; set; }

        
        public int CodigoResponsable { get; set; }

        
        public string Descripcion { get; set; }

        
        public string NombreResponsable { get; set; }

        
        public int Cantidad { get; set; }

        
        public string Modelo { get; set; }

        
        public int tipoProducto { get; set; }

        public string desTipoPaquete { get; set; }
		public int CodigoPaquete { get; set; }
        
        
        public string NombrePaquete { get; set; }

		
		public DateTime FechaInicio { get; set; }

        
        public string DesFechaInicio { get; set; }

        
        public string Counter { get; set; }

		
		public DateTime FechaFin { get; set; }

        
        public DateTime Hoy { get; set; }

        
        public string DesFechaFin { get; set; }
	    
		
		public decimal Costo { get; set; }

        
        public decimal Pago { get; set; }

        
        public decimal MontoTotal { get; set; }

        
        public decimal MontoCuota { get; set; }

        public string strFechaCuota { get; set; }
        
        public decimal Debe { get; set; }

        
        public int NroIngreso { get; set; }

        
        public int NroIngresoActual { get; set; }

        
        public string NroContrato { get; set; }

        
        public int MatriculadoPor { get; set; }

        
        public int FrezenDisponibles { get; set; }

        
        public string DesMatriculadoPor { get; set; }

        
        public string colorEstado { get; set; }

        
        public DateTime? FechaDesCongelacion { get; set; }

        
        public string DescFechaDesCongelacion { get; set; }

        
        public DateTime? FechaCongelacionProgramada { get; set; }

        
        public string DescFechaCongelacionProgramada { get; set; }

        
        public string MotivoCongelamiento { get; set; }

		
		public int Estado { get; set; }

        
        public string desEstado { get; set; }

        
        public int CantidadFreezing { get; set; }

        
        public int CantidadFreezingTomados { get; set; }

        
        public int CodigoSocioReceptor { get; set; }

        
        public string desProfesor { get; set; }

        
        public string Puntaje { get; set; }

        
        public string ObservacionCalificacion { get; set; }

        
        public string ObservacionAdicional { get; set; }

        
        public string Semana { get; set; }

        
        public string EstadoCogelado { get; set; }

        
        public int EstadoInfoCogelado { get; set; }

        
        public string EstadoColor { get; set; }

        
        public bool EstadoPaquete { get; set; }
        
        
        public string EstadoDescripcion { get; set; }
        
        
        public string colorInputCongelado { get; set; }

        
        public string ObservacionTraspaso { get; set; }

        
        public string SocioTraspasoReceptor { get; set; }
        
        
        public string codigoValorPaquete { get; set; }

        
        public int TipoContrato { get; set; }

        
        public int OrigenSociosTraspaso { get; set; }

        
        public int OrigenMembresiaTraspaso { get; set; }
            
        
        public int CodigoSocioTraspasado { get; set; }
        
        
        public string MembresiaTraspasada { get; set; }
        
        
        public int EstadoTraspaso { get; set; }

        
        public string UsuarioEmisor { get; set; }

        
        public int CodigoProfesor { get; set; }

        
        public int CodTiempoMenbresia { get; set; }

        
        public string UsuarioReceptor { get; set; }

        
        public string flagIngresar { get; set; }

        
        public string imageURL { get; set; }

		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }

        
        public string flagTermino { get; set; }

        
        public string Mensaje { get; set; }

        
        public int TipoMembresia { get; set; }

        
        public string IndTraspaso { get; set; }
        
        /* Reporte Traspaso */
        
        public int CodigoSocioTraslado { get; set; }
        
        public string NombresTraslado { get; set; }
        
        public int SedeTraslado { get; set; }
        
        public int CodigoMembresiaTraslado { get; set; }
        
        public string PaqueteTraslado { get; set; }
        
        public DateTime FechaInicioTraslado { get; set; }
        
        public DateTime FechaFinTraslado { get; set; }
        
        public decimal CostoTraslado { get; set; }
        
        public int EstadoTraslado { get; set; }
        
        public int NroIngresosTraslado { get; set; }
        /*Datos de Origen*/
        
        public int CodigoSocioOrigen { get; set; }
        
        public int CodigoMebresiaOrigen { get; set; }
        
        public string NombresOrigen { get; set; }
        
        public int SedeOrigen { get; set; }
        
        public string PaqueteOrigen { get; set; }
        
        public DateTime FechaInicioOrigen { get; set; }
        
        public DateTime FechaFinOrigen { get; set; }
        
        public decimal CostoOrigen { get; set; }
        
        public int EstadoOrigen { get; set; }
        
        public int NroIngresosOrigen { get; set; }
        
        public DateTime FechaTraspaso { get; set; }

        
        public int CodigoPaqueteTraspaso { get; set; }
        
        public string UsuarioResponsable { get; set; }

        
        public string ObtenerTiempoVencimiento { get; set; }

        
        public string ObtenerEstadoCitaNutrional { get; set; }
        public int ObtenerDisponibilidadHorarioPaquete { get; set; }
        /* ---------------- */
        
        public string DNI { get; set; }
        
        public string Distrito { get; set; }
        
        public string Direccion { get; set; }
        
        public int CantidadRegistros { get; set; }
        
        public decimal TotalDeuda { get; set; }
        
        public int flagPaqueteSedePermiso { get; set; }

        
        public string Nombres { get; set; }
        
        public string Apellidos { get; set; }
        
        public string desPlanMembresia { get; set; }
        
        public string desOrigenMembresia { get; set; } 
        public string desTipoMembresia { get; set; }

        
        public string desTiempoPaquete { get; set; }
        
        public string EstadoCelular { get; set; }
        
        public string ImagenUrl { get; set; }
        
        public string Celular { get; set; }
        
        public string Correo { get; set; }
        
        public int CodigoMembresia { get; set; }
        
        public string Vendedor { get; set; }
      
        public string DesCalificacion { get; set; }

         public string NombreComercial { get; set; }
        public int EstadoPermisoReservar { get; set; }

        public List<PagosContratoDTO> ListPagosContrato { get; set; }
        public List<ContratoMensajeDTO> ListContratoMensaje { get; set; }
        public List<ContratoCuotaDTO> ListContratoCuota { get; set; }
        public List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> ListReservas { get; set; }
	}
	
	
	public class ReqContratoDTO : Request
	{
		
        public List<ContratoDTO> List { get; set; }
	}
	
	
    public class ReqFilterContratoDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public ContratoDTO Item { get; set; }
        
        public Common.filterCaseContrato FilterCase { get; set; }
    }
	
	
    public class RespContratoDTO : Response
    {
      
    }
	
	
    public class RespItemContratoDTO : Response
    {
        
        public ContratoDTO Item { get; set; } 
    }

    
    public class RespListContratoDTO : Response
    {
        
        public List<ContratoDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	