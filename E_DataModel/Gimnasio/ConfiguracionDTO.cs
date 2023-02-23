using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{

    public class ConfiguracionDTO : AuditoriaDTO
    {

        public int TK_ID { get; set; }


        public string TK_Latitude { get; set; }


        public string TK_Longitude { get; set; }


        public int CodigoPaquete { get; set; }


        public int Codigo { get; set; }


        public string SubDominio { get; set; }


        public int TipoDia { get; set; }


        public int CantTotal { get; set; }


        public int TamanioPagina { get; set; }


        public int EstadoDividir { get; set; }


        public DateTime FechaDividir { get; set; }


        public int DiaDividir { get; set; }

        public int DiaCitasCaida { get; set; }


        public int DiasDemora { get; set; }


        public int EstadoActivarCorreoBienvenida { get; set; }


        public int EstadoMostrarVentaOtros { get; set; }


        public int EstadoImprimirContrato { get; set; }


        public decimal Igv { get; set; }


        public int TipoDescuento { get; set; }


        public bool GenerarSerie { get; set; }


        public string filtro { get; set; }


        public bool GenerarComprobante { get; set; }


        public int Int_GenerarComprobante { get; set; }


        public string ConexionDB { get; set; }



        public string Descripcion { get; set; }


        public string RutaCarpetaImagen { get; set; }


        public string LongitudSerie { get; set; }


        public string NombreTiquetera { get; set; }


        public int Tipo_Configuracion { get; set; }


        public int CantDiasDeuda { get; set; }


        public string Frase { get; set; }





        public string Ubicaciones { get; set; }


        public bool NotificarDeudasXDia { get; set; }


        public string RazonSocial { get; set; }


        public int CodigoPerfil { get; set; }


        public string Pais { get; set; }


        public string Ruc { get; set; }


        public string Telefono { get; set; }


        public string NombreComercial { get; set; }


        public string Departamento { get; set; }


        public string Direccion { get; set; }


        public string Distrito { get; set; }


        public string Correo { get; set; }


        public string Usuario { get; set; }


        public string Contrasenia { get; set; }


        public int Estado { get; set; }


        public string DesEstado { get; set; }


        public int PermitirMuchasAsistenciaPordia { get; set; }
        public bool ConsultasNumeroDocumentoEntidades { get; set; }


        public int TiempoMarcarAsistencia { get; set; }


        public int DescontarFreezingDisponiblesFlag { get; set; }


        public int DescontarFreezingDisponiblesNumero { get; set; }


        public int ClientesxVendedorAleatorio { get; set; }


        public int NumeroDiaMesEjecucionAleatorio { get; set; }


        public int Tipo { get; set; }


        public DateTime FechaPago { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public string str_FechaPago { get; set; }
        public string str_FechaVencimiento { get; set; }

        public int CodigoPermiso { get; set; }


        



        public List<ConfiguracionDTO> Lista { get; set; }

        
        public int Anio { get; set; }

        
        public string Mes { get; set; }

        
        public int MesEntero { get; set; }

        
        public string NroOperacion { get; set; }

        
        public string CodigoNroCuenta { get; set; }

       
        public string CodigoCuenta { get; set; }

        
        public string CodigoPago { get; set; }

        
        public string CCI { get; set; }

        
        public string RUC { get; set; }
        
        public string RUCSunat { get; set; }
        
        public string USUARIO { get; set; }
        
        public string CLAVE { get; set; }

        
        public decimal MontoMes { get; set; }
        
        
        public decimal Importe { get; set; }

        
        public int EstadoRecibo { get; set; }

        
        public string DesEstadoRecibo { get; set; }

        
        public string flafEstadoRecibo { get; set; }

        
        public string UrlRecibo { get; set; }


        
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }

        
        public int DiaPago { get; set; }

        
        public int CantidadActivosSocios { get; set; }

        
        public decimal MontoMatricula { get; set; }


        
        public decimal MontoMensualidad { get; set; }

        
        public decimal MontoGasto { get; set; }

        
        public string Logo { get; set; }
        public bool ObligatorioMarcarIngresoSalaClase { get; set; }
        public bool CodigoClienteAuto { get; set; }
        public bool ActivarImprimirContrato { get; set; }
        public bool ActivarGenerarContratoMembresias { get; set; }
        


        public int CantidadEmpresasActivas { get; set; }
        
        public int CantidadEmpresasPrueba { get; set; }
        
        public int CantidadEmpresasInactivas { get; set; }

        
        public int CantidadEmpresasRetirados { get; set; }
        public int CantidadEmpresasProspectos { get; set; }
        
        public decimal SumaTotalDeuda { get; set; }
        
        public decimal SumaTotalAcuenta { get; set; }
        
        public decimal SumaMatricula { get; set; }
        
        public decimal SumaGasto { get; set; }
        
        public decimal Utilidad { get; set; }
        
        public decimal TotalCobrar { get; set; }
        
        public decimal MontoAcuenta { get; set; }
        
        public string EntidadBancaria { get; set; }
        
        public string NroCuenta { get; set; }
       
        public string ResponsableCuenta { get; set; }

        public int CodigoPlan { get; set; }

        public string TipoMoneda { get; set; }

        public DateTime FechaCreacionEmpresa { get; set; }

        public string ReservasNormativa { get; set; }
        public string ReservasNotas { get; set; }

        public string Ticket_RazonSocial { get; set; }
        public string Ticket_RUC { get; set; }
        public string Ticket_Direccion { get; set; }
        public string Ticket_Celular { get; set; }
        public string Ticket_Telefono { get; set; }

        public bool TieneFacturacionElectronica { get; set; }
        public string UrlAPISunafact { get; set; }
        public string TokenSunafact { get; set; }



        public bool ObligatorioDNIProspectos { get; set; }


        public string ConsultasNumeroDocumento_Correo { get; set; }
        public string ConsultasNumeroDocumento_Clave { get; set; }
        public string ConsultasNumeroDocumento_ApiUrl { get; set; }
        public string ConsultasNumeroDocumento_ApiToken { get; set; }
        public DateTime ConsultasNumeroDocumento_FechaPago { get; set; }
        public string ConsultasNumeroDocumento_FechaPago_Texto { get; set; }
        public decimal ConsultasNumeroDocumento_PrecioAnual { get; set; }
        public string ConsultasNumeroDocumento_UsuarioCreacion { get; set; }
        public DateTime ConsultasNumeroDocumento_FechaCreacion { get; set; }
        public string ConsultasNumeroDocumento_FechaCreacion_Texto { get; set; }
        public bool ConsultasNumeroDocumento_Estado { get; set; }

        public string ConsultasNumeroDocumento_ConsultaNroDocumento { get; set; }


        public string NombreGerente { get; set; }
        public string ContactoCobranza { get; set; }
        public string CelularCobranza { get; set; }

        public string EmailHost { get; set; }
        public string EmailPort { get; set; }
        public string EmailUser { get; set; }
        public string EmailKey { get; set; }


        public bool AplicacionDisponible { get; set; }
        public bool TiendaAplicacion { get; set; }
        public bool RutinasAplicacion { get; set; }
    }

    
	public class ReqConfiguracionDTO : Request
	{
		
        public List<ConfiguracionDTO> List { get; set; }
	}
	
	
    public class ReqFilterConfiguracionDTO : Request
    {
        
        public Common.Paging Paging { get; set; }

        
        public ConfiguracionDTO Item { get; set; }

        
        public Common.filterCaseConfiguracion FilterCase { get; set; }
    }
	
	
    public class RespConfiguracionDTO : Response
    {
      
    }
	
	
    public class RespItemConfiguracionDTO : Response
    {
        
        public ConfiguracionDTO Item { get; set; } 
    }

    
    public class RespListConfiguracionDTO : Response
    {
        
        public List<ConfiguracionDTO> List { get; set; }

        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	