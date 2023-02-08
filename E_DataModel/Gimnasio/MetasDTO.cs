using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class MetasDTO : AuditoriaDTO
	{
        
        public int CodigoEntidadNegocio { get; set; }

		public int CodigoMeta { get; set; }

        
        public decimal Meta { get; set; }
        public int CantidadMetaPlan { get; set; }
        public int CantidadPlanesVendido { get; set; }
        public decimal MetaMinima { get; set; }

        
        public decimal Bono { get; set; }

        
        public int CantidadRepartidosMes { get; set; }

        
        public int CantidadRepartidosPorVendedorMes { get; set; }

        
        public DateTime FechaInicio { get; set; }

        
        public DateTime FechaFin { get; set; }

        
        public int CantidadVendedores { get; set; }
        
        
        public int CodigoSupervisorVenta { get; set; }

        
        public decimal B_TicketPromedio_MontoMinimo { get; set; }

        
        public decimal B_TicketPromedio_Bono { get; set; }

        
        public int B_Nuevos_PorcentajeMinimo { get; set; }

        
        public decimal B_Nuevos_Bono { get; set; }

        
        public decimal B_Reinscripciones_MontoMinimo { get; set; }

        
        public decimal B_Reinscripciones_Bono { get; set; }

        
        public int B_Renovaciones_PorcentajeMinimo { get; set; }

        
        public decimal B_Renovaciones_Bono { get; set; }

        
        public int B_ContratosAnuales_CantidadMinima { get; set; }

        
        public decimal B_ContratosAnuales_Bono { get; set; }

        
        public decimal B_VentaSemanal_Bono { get; set; }

        
        public decimal B_VentaAdicionalMeta10porciento_Bono { get; set; }

        
        public int B_AmpliacionContrato_Cantidad { get; set; }

        
        public decimal B_AmpliacionContrato_Bono { get; set; }

        
        public int CantidadClientesVendidos { get; set; }

        
        public decimal VentaMenbresiasAnuales { get; set; }

        
        public int CantidadVentaMenbresiasAnuales { get; set; }
        
        
        public int CantidadMenbresiasRenovaciones { get; set; }

        
        public decimal VentaMenbresiasRenovaciones { get; set; }

        
        public int CantidadMenbresiasRenovacionPorVendedorRepartido { get; set; }

        
        public decimal VentaRenovacionesPorcentaje { get; set; }

        
        public int CantidadMenbresiasReinscripcion { get; set; }

        
        public int IngresoCantidadCitasReinscripcion { get; set; }

        
        public decimal PorcentajeEfectividadReinscripcion { get; set; }

        
        public decimal VentaMenbresiasReinscripcion { get; set; }

        
        public decimal VentaNuevasPorcentaje { get; set; }

        
        public int CantidadVentasNuevas { get; set; }

        
        public int CantidadClientesNuevos { get; set; }

        
        public int CantidadMenbresiasAmplacion { get; set; }

        
        public int CantidadMenbresiasAmplacionRenovacion { get; set; }

        
        public int CantidadMenbresiasAmpliacionReinscripcion { get; set; }

        
        public decimal VentaMenbresiasAmplacion { get; set; }

        
        public List<MetasDetalleDTO> ListaDetalle { get; set; }
                
        
        public string AsesorDeVentas { get; set; }
        
		
		public int CodigoVendedor { get; set; }

        
        public int CodigoUsuario { get; set; }

        
        public string NombreCompleto { get; set; }

        
        public string imagenUrl { get; set; }

        
        public decimal VentaTotal { get; set; }
        
        
        public decimal DiferenciaFaltante { get; set; }

        
        public decimal VentaDiaria { get; set; }

        
        public decimal NecesidadDiaria { get; set; }

        
        public decimal PorAcumulado { get; set; }

        
        public decimal PorFaltante { get; set; }

        
        public decimal Proyeccion { get; set; }


        //comisiones

        
        public decimal MetaSemanal { get; set; }

        
        public decimal Comision1a { get; set; }
        
        public decimal Comision1b { get; set; }
        
        public decimal Comision1porc { get; set; }

        
        public decimal Comision2a { get; set; }
        
        public decimal Comision2b { get; set; }
        
        public decimal Comision2porc { get; set; }

        
        public decimal Comision3a { get; set; }
        
        public decimal Comision3b { get; set; }
        
        public decimal Comision3porc { get; set; }

        
        public decimal Comision4a { get; set; }
        
        public decimal Comision4b { get; set; }
        
        public decimal Comision4porc { get; set; }

        
        public decimal Comision5a { get; set; }
        
        public decimal Comision5b { get; set; }
        
        public decimal Comision5porc { get; set; }

        
        public decimal Comision6a { get; set; }
        
        public decimal Comision6b { get; set; }
        
        public decimal Comision6porc { get; set; }

        
        public decimal PorcenBonoAdicional1 { get; set; }
        
        public decimal PorcenBonoAdicional3 { get; set; }
        
        public decimal PorcenBonoAdicional5 { get; set; }
        
        public decimal Monto_BonoAdicional1 { get; set; }
        
        public decimal Monto_BonoAdicional3 { get; set; }
        
        public decimal Monto_BonoAdicional5 { get; set; }
        
        public decimal PorcenBonoAdicional2 { get; set; }
        
        public decimal PorcenBonoAdicional4 { get; set; }
        
        public decimal PorcenBonoAdicional6 { get; set; }
        
        public decimal Monto_BonoAdicional2 { get; set; }
        
        public decimal Monto_BonoAdicional4 { get; set; }
        
        public decimal Monto_BonoAdicional6 { get; set; }
    
        
        public decimal Pago_ComisionSoles { get; set; }
        
        public decimal Pago_BonoGrupalPorcentaje { get; set; }
        
        public decimal Pago_BonoGrupalSoles { get; set; }
        
        public decimal Pago_BonoTicketPromedioSoles { get; set; }
        
        public decimal Pago_BonoAnualesSoles { get; set; }
        
        public decimal Pago_BonoEfectividadNuevos { get; set; }
        
        public decimal Pago_BonoSemanal { get; set; }
        
        public decimal Pago_BonoRenovacion { get; set; }
        
        public decimal Pago_BonoReinscripcion { get; set; }
        
        public decimal Pago_Bono10PorcientoAdicional { get; set; }
        
        public decimal Pago_ComisionBonoAdicional { get; set; }
        
        public decimal Pago_BonoAmpliacion { get; set; }
        
        public decimal Pago_TotalCobrar { get; set; }

        
        public DateTime FechaSemanal1a { get; set; }
        
        public DateTime FechaSemanal1b { get; set; }
        
        public decimal CuotaSemanalBono1 { get; set; }
        
        public DateTime FechaSemanal2a { get; set; }
        
        public DateTime FechaSemanal2b { get; set; }
        
        public decimal CuotaSemanalBono2 { get; set; }
        
        public DateTime FechaSemanal3a { get; set; }
        
        public DateTime FechaSemanal3b { get; set; }
        
        public decimal CuotaSemanalBono3 { get; set; }
        
        public DateTime FechaSemanal4a { get; set; }
        
        public DateTime FechaSemanal4b { get; set; }
        
        public decimal CuotaSemanalBono4 { get; set; }
        
        public decimal MetaMinimaPorc { get; set; }

        
        public int Verificador_Nuevo_Cant { get; set; }

        
        public int Verificador_Renov_Cant { get; set; }

        
        public int Verificador_Reins_Cant { get; set; }

        
        public int Verificador_Ampli_Cant { get; set; }

        
        public int Verificador_Anual_Cant { get; set; }

        
        public int Verificador_TipoIngreso { get; set; }

        
        public string Verificador_NombreTipoIngreso { get; set; }

        
        public int Verificador_CodigoSocio { get; set; }

        
        public string Verificador_AsesorDeVentas { get; set; }

        
        public string Verificador_NombresCliente { get; set; }

        
        public string Verificador_ApellidoCliente { get; set; }

        
        public string Verificador_DNICliente { get; set; }

        
        public string Verificador_PaqueteCliente { get; set; }

        
        public DateTime Verificador_FechaFinCliente { get; set; }

        
        public DateTime Verificador_FechaInicioCliente { get; set; }

        
        public int Verificador_CantidadTotalFilas { get; set; }

        
        public int Verificador_CodigoSeguimiento { get; set; }

        
        public int Verificador_Seguimiento { get; set; }

        
        public int Productividad_Nuevos { get; set; }
        
        public int Productividad_Invitados { get; set; }
        
        public int Productividad_Referidos { get; set; }
        
        public int Productividad_Llamada { get; set; }
        
        public int Productividad_Web { get; set; }
        
        public int Productividad_TotalProspectos { get; set; }
        
        public int Productividad_TotalInscritos { get; set; }
        
        public decimal Productividad_Efectividad { get; set; }
        
        public decimal Productividad_MontoTotal { get; set; }
        
        public decimal Productividad_TPromedio { get; set; }
        
        public int Productividad_CantidadCitas { get; set; }
        
        public int Productividad_EfectividadCitas { get; set; }
        
        public decimal Productividad_TPromedioCitas { get; set; }
        
        public int TamanioPagina { get; set; }

        public int ConversionLeads_CantidadTotalBD 
        {
            get{
                return ConversionLeads_CantidadWalking + ConversionLeads_CantidadRenovaciones + ConversionLeads_CantidadReinscripciones + ConversionLeads_CantidadProspeccion + ConversionLeads_CantidadDigital + ConversionLeads_CantidadLlamadaE;
            }
        }
        public int ConversionLeads_CantidadTotalActividadesCitas
        {
            get
            {
                return ConversionLeads_ActividadCitas_walking + ConversionLeads_ActividadCita_Renovaciones + ConversionLeads_ActividadCita_Reinscripciones +
                       ConversionLeads_ActividadCitas_prospeccion + ConversionLeads_ActividadCita_digital + ConversionLeads_ActividadCita_llamadaE;
            }
        }

        public int ConversionLeads_CantidadTotalActividadesReunion
        {
            get
            {
                return ConversionLeads_ActividadReunion_walking + ConversionLeads_ActividadReunion_Renovaciones + ConversionLeads_ActividadReunion_Reinscripciones +
                       ConversionLeads_ActividadReunion_prospeccion + ConversionLeads_ActividadReunion_digital + ConversionLeads_ActividadReunion_llamadaE;
            }
        }
        public int ConversionLeads_CantidadWalking { get; set; }
        public int ConversionLeads_CantidadRenovaciones { get; set; }
        public int ConversionLeads_CantidadReinscripciones { get; set; }
        public int ConversionLeads_CantidadProspeccion { get; set; }
        public int ConversionLeads_CantidadDigital { get; set; }
        public int ConversionLeads_CantidadLlamadaE { get; set; }

        public int ConversionLeads_ActividadCitas_walking { get; set; }
        public int ConversionLeads_ActividadReunion_walking { get; set; }
        public int ConversionLeads_ActividadCita_Renovaciones { get; set; }
        public int ConversionLeads_ActividadReunion_Renovaciones { get; set; }
        public int ConversionLeads_ActividadCita_Reinscripciones { get; set; }
        public int ConversionLeads_ActividadReunion_Reinscripciones { get; set; }
        public int ConversionLeads_ActividadCitas_prospeccion { get; set; }
        public int ConversionLeads_ActividadReunion_prospeccion { get; set; }
        public int ConversionLeads_ActividadCita_digital { get; set; }
        public int ConversionLeads_ActividadReunion_digital { get; set; }
        public int ConversionLeads_ActividadCita_llamadaE { get; set; }
        public int ConversionLeads_ActividadReunion_llamadaE { get; set; }
        public decimal ConversionLeads_VentaTotal { get; set; }

        public int ConversionLeads_CantidadClientesVenta { get; set; }
        public int ConversionLeads_CantidadClientesVenta_walking { get; set; }
        public int ConversionLeads_CantidadClientesVenta_renovacion { get; set; }
        public int ConversionLeads_CantidadClientesVenta_reinscripcion { get; set; }
        public int ConversionLeads_CantidadClientesVenta_prospeccion { get; set; }
        public int ConversionLeads_CantidadClientesVenta_digital { get; set; }
        public int ConversionLeads_CantidadClientesVenta_llamadaentrante { get; set; }
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }

        public bool flagRepartirEquitativamenteSegunMeta { get; set; }
        public bool flagRepartirInactivos { get; set; }
        public bool flagRepartirRenovaciones { get; set; }

        public bool flagRepartirProspectosSinCitaVendedoresInactivos { get; set; }

        public bool flagRepartirProspectosSinActividadVendedoresActivos { get; set; }

    }
	
	
	public class ReqMetasDTO : Request
	{
		
        public List<MetasDTO> List { get; set; }
	}
	
	
    public class ReqFilterMetasDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public MetasDTO Item { get; set; }
        
        public Common.filterCaseMetas FilterCase { get; set; }
    }
	
	
    public class RespMetasDTO : Response
    {
      
    }
	
	
    public class RespItemMetasDTO : Response
    {
        
        public MetasDTO Item { get; set; } 
    }

    
    public class RespListMetasDTO : Response
    {
        
        public List<MetasDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	