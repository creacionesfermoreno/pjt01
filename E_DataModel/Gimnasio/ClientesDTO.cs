using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	public class ClientesDTO : AuditoriaDTO
	{        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; } 
        public string Nota { get; set; }

        
        public int CantidadCitas { get; set; }

        
        public int CodigoSocio { get; set; }

        
        public string Codigo { get; set; }

       
        public int CodigoOrigen { get; set; }

        
        public int CodigoS { get; set; }

        
        public int SocioInvitado { get; set; }

        
        public int TamanioPagina { get; set; }

        
        public int CantidadRegistrosSocios { get; set; }
        
        
        public int CantidadTotalFilas  { get; set; }

        
        public int CantMujeres { get; set; }

        
        public int CantHombres { get; set; }

        
        public string DescSede { get; set; }

        
        public string UserAsesorVenta { get; set; }

        
        public string desNomSocio { get; set; }

        
        public int CodTiempoPaquete { get; set; }

        
        public string desTiempoPaquete { get; set; }

        
        public decimal Costo { get; set; }

        
        public decimal DeudaFiado { get; set; }
        
        public decimal DeudaSuplemento { get; set; }
        
        public decimal DeudaRopa { get; set; }
        /********************/

        
        public string Sexo { get; set; }

        
        public string Counter { get; set; }
        
        public string TipoIngreso { get; set; }
        
        
        public string AsesorComercial { get; set; }

        
        public int EdadRango1 { get; set; }

        
        public int EdadRango2 { get; set; }

        
        public int TipoCliente { get; set; }

        
        public int Tipo { get; set; }


        
        public string DescTipo { get; set; }


        
        public int EstadoClienteInt { get; set; }

        
        public int EstadoDeuda { get; set; }

        
        public int Origen { get; set; }

        
        public DateTime FechaCaidaDesde { get; set; }

        
        public DateTime FechaCaidaHasta { get; set; }

        
        public DateTime FiltroFechaInicio { get; set; }

        
        public DateTime FiltroFechaFin { get; set; }

        
        public string Nombre { get; set; }

        
        public string CantidadFilas { get; set; }
        
        /********************/

        
        public int EdadInicio { get; set; }

        
        public int EdadFin { get; set; }
        
        
        public DateTime FechaInicio { get; set; }

        
        public DateTime FechaFinaliza { get; set; }

        
        public string Descripcion { get; set; }

        public string IdHorario { get; set; }

        public int Cantidad { get; set; }

        
        public Decimal porCentajeTotal { get; set; }

        
        public DateTime FechaUltima { get; set; }


        
        public int Top { get; set; }


        /********************/
        
        
        public int TipoAgenda { get; set; }

        
        public int Turno { get; set; }

        
        public int FormaPago { get; set; } 
        public string strFormaPago { get; set; }
        
        
        public int CodigoMembresia { get; set; }

        
        public int CodigoAbrirCaja { get; set; }

        
        public string Nombres { get; set; }

        
        public string Vendedor { get; set; }
        
        
        public string VendedorRepartido { get; set; }

        
        public string VendedorSocios { get; set; }

        
        public DateTime FechaFinal { get; set; }

		
		public string Apellidos { get; set; }
        
        
        public string DesTipoSocio { get; set; }

	    
        public string NombreCompleto { get; set; }

        
        public string NombreApellido { get; set; }

        
        public string Ubicaciones { get; set; }

        
        public int TipoDocumento { get; set; }

        
        public int EstadoCivil { get; set; }

        
        public int EstadoHijos { get; set; }

        
        public int CantTotal { get; set; }

        
        public string TelefonoTrabajo { get; set; }

        
        public int TipoClienteAgenda { get; set; }

        
        public int CantidadVendedoresActivos { get; set; }

        
        public int CantidadRepartidoInactivosPorVendedor { get; set; }

        
        public string NomEstadoCliente { get; set; }

		
		public string DNI { get; set; }
        public string NroComprobante { get; set; }
        public string NroContrato { get; set; }


        public string desTipoCliente { get; set; }

        
        public string TelefonoCelular { get; set; }

		
		public string Telefono { get; set; }
	
		
		public string Celular { get; set; }
        
        public string EstadoCelular { get; set; }
        
        public string EstadoAsistencia { get; set; }
	    
		
		public string Correo { get; set; }

        
        public DateTime FechaFinStr { get; set; }
        
        
        public DateTime Fecha { get; set; }
        
        
        public string FormaPagoEfectivo { get; set; }
        
        public decimal TotalEfectivo { get; set; }
        
        public decimal TotalEfectivoDolares { get; set; }

        
        public string FormaPagoDebito { get; set; }
        
        public decimal TotalDebito { get; set; }
        
        public decimal TotalDebitoDolares { get; set; }

        
        public string FormaPagoCredito { get; set; }
        
        public decimal TotalCredito { get; set; }
        
        public decimal TotalCreditoDolares { get; set; }

        
        public string FormaPagoDeposito { get; set; }
        
        public decimal TotalDeposito { get; set; }
        
        public decimal TotalDepositoDolares { get; set; }

        
        public string FormaPagoWeb { get; set; }
        
        public decimal TotalWeb { get; set; }
        
        public decimal TotalWebDolares { get; set; }

        
        public string FormaPagoCuponera { get; set; }
        
        public decimal TotalCuponera { get; set; }
        
        public decimal TotalCuponeraDolares { get; set; }

        
        public decimal TotalVentaDia { get; set; }

        
        public decimal TotalVentaTarde { get; set; }

        
        public decimal TotalVentaNoche { get; set; }

        
        public string Ocupacion { get; set; }

	    
        public int ReferidoPor { get; set; }

        
        public string DesReferidoPor { get; set; }

        
        public string DesTipoCliente { get; set; }

        
        public string DescTipoCliente { get; set; }

        
        public string DescFechaCaida{ get; set; }

        
        public int flag { get; set; }

        
        public string flagReinslibre { get; set; }

        
        public string Filtro { get; set; }

        
        public string FiltroBusqueda { get; set; }

        
        public DateTime? FechaNacimiento { get; set; }

        
        public string DescFechaNacimiento { get; set; }
        
        
        public string FechaNacimientoDesc { get; set; }

        
        public string DesFechaNacimiento { get; set; }

        
        public string FechaFin { get; set; }

        
        public string DesFechaInicio { get; set; }

        
        public string DesFechaFin { get; set; }

        
        public DateTime Param_FechaAusente { get; set; }

        
        public DateTime FechaIngreso { get; set; }

	    
        public string DesFechaIngreso { get; set; }

		
		public string ImagenUrl { get; set; }

        public string ImagenUrlCarnetVacunacion { get; set; }

        public bool Estado { get; set; }
	    
		
		public string Genero { get; set; }

        
        public string desCita { get; set; }

        
        public string Huella { get; set; }

        
        public int Edad { get; set; }

        
        public int CantHijos { get; set; }

        
        public string UrlFacebook { get; set; }

        
        public string flagCumpleanios { get; set; }

        
        public string Direccion { get; set; }

        
        public string DireccionTrabajo { get; set; }

        
        public string desSede { get; set; }

        
        public string Distrito { get; set; }

        
        public string EstadoCliente { get; set; }

        
        public string StiloMarcarAsistencia { get; set; }

        
        public int NumeroEstadoCliente { get; set; }

        
        public string ColorNumeroEstadoCliente { get; set; }

        
        public int CodigoDato { get; set; }

        
        public string EstadoSocio { get; set; }

        
        public string NombreMembresia { get; set; }

        
        public string InformeTraspaso { get; set; }

        
        public string desNomProfesor { get; set; }

        
        public string ColorEstado { get; set; }

        
        public string FechasDelEstado { get; set; }

        
        public int Anio { get; set; }

        
        public int Mes { get; set; }

        
        public string DescTipoAgenda { get; set; }

        
        public string ColorAgenda { get; set; }

        
        public string AsuntoAgenda { get; set; }

        
        public string EncargadoAgenda { get; set; }

        
        public string DescEstadoAgenda { get; set; }

        
        public int NrDias { get; set; }

        
        public decimal Precio { get; set; }

        
        public decimal Pago { get; set; }

        
        public decimal Debe { get; set; }

        
        public int CantidadVencidos { get; set; }

        
        public int CantidadRenovaron { get; set; }

        
        public int CantidadInvitados { get; set; }

        
        public int CantidadMatriculados { get; set; }

        
        public int CantidadTodos { get; set; }

        
        public int CantidadRenovaciones { get; set; }
        
        
        public int CantidadActivos { get; set; }

        
        public int CantidadProspectos { get; set; }
        
        
        public int CantidadInactivos { get; set; }
        
        
        public int CantidadMembresias { get; set; }

        
        public decimal ImporteMembresias { get; set; }

        
        public decimal MontoTotalMenbresia { get; set; }

        
        public int CantidadLibres { get; set; }

        
        public decimal ImporteLibres { get; set; }
        
        
        public int CantidadProductos { get; set; }

        
        public decimal ImporteProductos { get; set; }

        
        public int CantidadServicios { get; set; }
        
        
        public decimal ImporteServicios { get; set; }

        
        public int CantidadMemConDeu { get; set; }

        
        public decimal MontoMemDeuda { get; set; }
        
        
        public decimal MontoTotalSalida { get; set; }

        
        public decimal ImporteEgresos { get; set; }

        
        public decimal ImporteEgresosEventos { get; set; }
         
        
        public int CantidadCierres { get; set; }

        
        public decimal ImporteCaja { get; set; }

        
        public decimal MontoDeuda { get; set; }

        
        public decimal MontoTotalSalidasSindeudas { get; set; }

        
        public int CantidadEgresosEventos { get; set; }

        
        public decimal ImporteEventos { get; set; }
        
        
        public int CantidadEventos { get; set; }
            
        
        public int CantidadEgresos { get; set; }

        
        public decimal TotalUtilidades { get; set; }

        
        public decimal MontoComprometido { get; set; }

        
        public string FechaCuota { get; set; }

        
        public DateTime HoraInicioAgenda { get; set; }

        
        public DateTime HoraFinAgenda { get; set; }

		
        public Common.Operation Operation { get; set; }

        
        public string  MembresiaDescripcion { get; set; }

        
        public string TipoClienteEstado{ get; set; }

        
        public int CantidadSociosMigracion { get; set; }

        /*proceso de migracion*/
        
        public string UsuMigrador { get; set; }

        
        public string UsuReceptor { get; set; }

        
        public int TipMigracion { get; set; }

        
        public int CantParcial { get; set; }

        
        public int CheckTodos { get; set; }

        
        public int CodigoPaquete { get; set; }

        
        public int NroIngreso { get; set; }

        
        public int NroIngresoActual { get; set; }

        
        public string DesEstadoMenbresias { get; set; }

        
        public string DesCalificacion { get; set; }

        
        public DateTime DteFechaFin { get; set; }

        
        public int CodigoProspecto { get; set; }

        
        public decimal dashboar_TotalVentaMesMembresias { get; set; }
        
        public decimal dashboar_TotalVentaMesNutricion { get; set; }
        
        public decimal dashboar_TotalVentaMesPersonalizado { get; set; }
        
        public decimal dashboar_TotalVentaMesDiario { get; set; }
        
        public decimal dashboar_TotalVentaMesJugueria { get; set; }
        
        public decimal dashboar_TotalVentaMesSuplementos { get; set; }
        
        public decimal dashboar_TotalVentaMesRopas { get; set; }
        
        public decimal dashboar_TotalVentaHoyMembresias { get; set; }
        
        public decimal dashboar_TotalVentaHoyNutricion { get; set; }
        
        public decimal dashboar_TotalVentaHoyPersonalizado { get; set; }
        
        public decimal dashboar_TotalVentaHoyDiario { get; set; }
        
        public decimal dashboar_TotalVentaHoyJugueria { get; set; }
        
        public decimal dashboar_TotalVentaHoySuplementos { get; set; }
        
        public decimal dashboar_TotalVentaHoyRopas { get; set; }
        
        public decimal dashboar_DebeMembresia { get; set; }
        
        public decimal dashboar_TotalGasto { get; set; }
        
        public decimal dashboar_CantidadActivos { get; set; }
        
        public decimal dashboar_CantidadPorVencer { get; set; }
        
        public decimal dashboar_CantidadClientesRenovaron { get; set; }
        
        public decimal dashboar_CantidadClientesNuevos { get; set; }
        public decimal dashboar_CantidadClientesTotalInactivos { get; set; }
        public decimal dashboar_CantidadClientesVencidosMes { get; set; }
        public decimal dashboar_CantidadClientesReinscritos { get; set; }
        public decimal dashboar_CantidadClientesPorIniciar { get; set; }
        public decimal dashboar_CantidadClientesTotalTraspasos { get; set; }
        
        public decimal dashboar_CantidadLlamadaEntrante { get; set; }
        
        public decimal dashboar_CantidadReferido { get; set; }
        
        public decimal dashboar_CantidadNuevos { get; set; }
        
        public decimal dashboar_CantidadInvitados { get; set; }
        
        public decimal dashboar_CantidadWeb { get; set; }
        
        public int OrigenMembresia { get; set; }
        
        public string Xml { get; set; }

        public string Tema { get; set; }

        public decimal Total { get; set; }

        public string GrupoEdades { get; set; }
    }
	
	public class ReqClientesDTO : Request
	{
		
        public List<ClientesDTO> List { get; set; }
	}
	
    public class ReqFilterClientesDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public ClientesDTO Item { get; set; }
        
        public Common.filterCaseClientes FilterCase { get; set; }
    }
	
    public class RespClientesDTO : Response
    {
      
    }	
	
    public class RespItemClientesDTO : Response
    {
        
        public ClientesDTO Item { get; set; } 
    }
    
    public class RespListClientesDTO : Response
    {
        
        public List<ClientesDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	