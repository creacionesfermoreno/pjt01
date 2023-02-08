using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.Gimnasio
{
    
    public class ReporteTotalVentaMembresiasDTO
    {

        public int Anio { get; set; }
        
        public int Mes { get; set; }
        
        public int Dia { get; set; }
        
        public string Paquete { get; set; }
        
        public string UsuarioCreacion { get; set; }
        
        public decimal SumaMonto { get; set; }
        
        public string Fecha { get; set; }
    }

    public class ReporteTotalVentaMembresiasMesesDTO
    {

        
        public int Anio { get; set; }
        
        public int Mes { get; set; }
        
        public int Dia { get; set; }

        
        public string Paquete { get; set; }
        
        public string Fecha { get; set; }
        
        public decimal SumaMonto { get; set; }
    }
        

    public class ReporteTotalVentasEventosDTO
    {

        
        public string Evento { get; set; }
        
        public string Fecha { get; set; }
        
        public decimal Total { get; set; }
    }

    public class ReporteTotalVentasLibreDTO
    {

        
        public string DesLibre { get; set; }
        
        public decimal Monto { get; set; }
        
        public string Fecha { get; set; }
    }

    public class ReporteTotalVentasDTO
    {

        
        public int Tipo { get; set; }
        
        public decimal MontoMembresia { get; set; }
        
        public decimal MontoLibre { get; set; }
        
        public decimal MontoEvento { get; set; }
        
        public string Fecha { get; set; }
    }

    public class ReporteTotalAsistenciaDTO
    {

        
        public int CantidadMem { get; set; }
        
        public int CantidadLib { get; set; }
        
        public string Fecha { get; set; }
        
        public string DesLibre { get; set; }
        
        public string DesMem { get; set; }
        
        public string Paquete { get; set; }
    }


    public class ReporteRankinCantidadMembresiasPorUsuarioDTO
    {

        
        public string UsuarioCreacion { get; set; }
        
        public int Cantidad { get; set; }
    }

    public class ReporteRankinCantidadLibresPorUsuarioDTO
    {

        
        public string UsuarioCreacion { get; set; }
        
        public int Cantidad { get; set; }
    }

    public class ReporteCantidadVentasMenbresiasPorUsuarioDTO
    {

        
        public string UsuarioCreacion { get; set; }
        
        public int Cantidad { get; set; }
        
        public string Fecha { get; set; }
    }

    public class ReporteCantidadVentasLibrePorUsuarioDTO
    {

        
        public string UsuarioCreacion { get; set; }
        
        public int Cantidad { get; set; }
        
        public string Fecha { get; set; }
    }


    public class ReporteRankinPagoembresiasPorUsuarioDTO
    {

        
        public string UsuarioCreacion { get; set; }
        
        public decimal Monto { get; set; }
    }

    public class ReporteRankinPagoLibresPorUsuarioDTO
    {

        
        public string UsuarioCreacion { get; set; }
        
        public decimal Monto { get; set; }
    }

    public class ReportePagoVentasMenbresiasPorUsuarioDTO
    {

        
        public string UsuarioCreacion { get; set; }
        
        public decimal Monto { get; set; }
        
        public string Fecha { get; set; }
    }

    public class ReportePagoVentasLibrePorUsuarioDTO
    {

        
        public string UsuarioCreacion { get; set; }
        
        public decimal Monto { get; set; }
        
        public string Fecha { get; set; }
    }

    public class ReporteMetasDTO
    {

        
        public int CodigoVendedor { get; set; }
        
        public string NombreCompleto { get; set; }
        
        public decimal Meta { get; set; }
        
        public decimal Monto { get; set; }
        
        public decimal Falta { get; set; }

        
        public int p_anio { get; set; }
        
        public int p_mes { get; set; }
        
        public int p_dia { get; set; }
        
        public int p_flag { get; set; }
    }

    public class ReporteResumenVendedoresDTO
    {

        
        public int codigo { get; set; }
        
        public string nombre { get; set; }
        
        public decimal cantidad { get; set; }
        
        public int Vendedor { get; set; }

        
        public int p_anio { get; set; }
        
        public int p_mes { get; set; }
    }

    public class ReporteCantidadVentaPaquetesDTO
    {

        
        public string nombre { get; set; }
        
        public int cantidad { get; set; }

        
        public int p_anio { get; set; }
        
        public int p_mes { get; set; }
        
        public int p_dia { get; set; }
    }

    public class ReportePorcentajeRetencionInsercionNuevosDTO
    {

        
        public string nombre { get; set; }
        
        public int cantidad { get; set; }

        
        public int p_anio { get; set; }
        
        public int p_mes { get; set; }
    }

    public class ReporteMembresiasVencidoEsteMes
    {

        
        public int CodigoMenbresia { get; set; }
        
        public int CodigoSocio { get; set; }
        
        public string NomSocio { get; set; }
        
        public string ImagenUrl { get; set; }
        
        public int CodigoPaquete { get; set; }
        
        public string NombrePaquete { get; set; }
        
        public string Telefono { get; set; }
        
        public string FechaInicio { get; set; }
        
        public string FechaFin { get; set; }
        
        public decimal Costo { get; set; }
        
        public decimal MontoTotal { get; set; }
        
        public decimal Debe { get; set; }
        
        public string DesEstado { get; set; }
        
        public string UsuarioCreacion { get; set; }
    }

    public class ReporteClientesComprometidosPagos
    {

        
        public int CodigoMenbresia { get; set; }
        
        public int CodigoSocio { get; set; }
        
        public string Cliente { get; set; }
        
        public string Telefono { get; set; }
        
        public string Membresia { get; set; }
        
        public decimal Precio { get; set; }
        
        public decimal Pago { get; set; }
        
        public decimal Debe { get; set; }
        
        public string FechaCuota { get; set; }
        
        public decimal MontoComprometido { get; set; }
        
        public string Facebook { get; set; }
        
        public string ImagenUrl { get; set; }

    }

    public class ReporteSeguimientoDTO {


        
        public string FechaCreacion { get; set; }
        
        public string Colaborador { get; set; }
        
        public int CantidadInformacion { get; set; }
        
        public int CantidadCitasConfirmadas { get; set; }
        
        public int CantidadCitasNoConfirmadas { get; set; }
        
        public int CantidadLlamadasConcretas { get; set; }
        
        public int CantidadLlamadasNoConcretas { get; set; }

    }

    public class ReporteTotalVentaProductos {


        
        public string Fecha { get; set; }
        
        public string Categoria { get; set; }
        
        public string Producto { get; set; }
        
        public int Cantidad { get; set; }
        
        public decimal Importe { get; set; }

    }

    public class ReporteCompras
    {

        
        public string Fecha { get; set; }
        
        public decimal Percepcion { get; set; }
        
        public decimal TotalNeto { get; set; }
    }

    public class ReporteMembreciasVencidas
    {

        
        public int CodigoMenbresia { get; set; }
        
        public int CodigoSocio { get; set; }
        
        public string NomSocio { get; set; }
        
        public string ImagenUrl { get; set; }
        
        public int CodigoPaquete { get; set; }
        
        public string NombrePaquete { get; set; }
        
        public string Telefono { get; set; }
        
        public string DNI { get; set; }
        
        public string Distrito { get; set; }
        
        public string Direccion { get; set; }
        
        public string UsuarioCreacion { get; set; }
        
        public string AsesorComercial { get; set; }
        
        public string FechaMatricula { get; set; }
        
        public string FechaInicio { get; set; }
        
        public string FechaFin { get; set; }
        
        public string FechaHoy { get; set; }
        
        public decimal Costo { get; set; }
        
        public decimal MontoTotal { get; set; }
        
        public decimal MontoTotalEsteMes { get; set; }
        
        public decimal Debe { get; set; }
        
        public string DesEstado { get; set; }
        
        public DateTime FechaEdicion { get; set; }
        
        public string FechaEdicionStr { get; set; }
        
        public string FechaCongelacionProgramadaStr { get; set; }
        
        public DateTime FechaCongelacionProgramada { get; set; }

        
        public string FechaDesCongelacionProgramadaStr { get; set; }
        
        public DateTime FechaDesCongelacionProgramada { get; set; }

        
        public string timeMostrar { get; set; }

        
        public int FrezenDisponibles { get; set; }

        
        public int Dias { get; set; }
    }

    public class ReporteSeguimientoDetalleDTO {

        
        public string Cliente { get; set; }
        
        public string Telefono { get; set; }
        
        public string Comentario { get; set; }
        
        public string DesEstado { get; set; }
    }

    public class ReporteDetalleComprasSocioDTO {

        
        public int CodigoSocio { get; set; }
        
        public int Cantidad { get; set; }
        
        public string Tipo { get; set; }
        
        public string Descripcion { get; set; }
        
        public decimal Importe { get; set; }

    }

    public class ReporteVentasDTO
    {

        
        public int CodigoCliente { get; set; }
        
        public string NroComprobante { get; set; }
        
        public string Cliente { get; set; }
        
        public string Telefono { get; set; }
        
        public string Direccion { get; set; }
        
        public string Correo { get; set; }
        
        public string Facebook { get; set; }
        
        public string ImgFacebook { get; set; }
        
        public string FechaVenta { get; set; }
        
        public string Mes { get; set; }
        
        public decimal Ingresos { get; set; }
        
        public decimal Egresos { get; set; }
        
        public string Cantidad { get; set; }
        
        public decimal PrecioUnitario { get; set; }
        
        public string Descripcion { get; set; }
        
        public decimal Importe { get; set; }
        
        public string UsuarioCreador { get; set; }
        
        public string Tipo { get; set; }
        
        public decimal Soles { get; set; }
        
        public string NroTarjeta { get; set; }
        
        public string DescFormaPago { get; set; }

        
        public string Counter { get; set; }
        
        public string AsesorComercial { get; set; }
    }


    public class ReporteVentaDeMembresiasDTO
    {

        
        public int CodigoPaquete { get; set; }
        
        public string DescPaquete { get; set; }
        
        public int CantMatriculados { get; set; }
        
        public decimal ImporteNuevos { get; set; }
        
        public int CantidadMembrePasadas { get; set; }
        
        public decimal ImporteMebresiasPasada { get; set; }
        
        public decimal TotalIngresos { get; set; }
        

    }
    public class ReporteVentasAnuladas
    {

        
        public int CodigoIngreso { get; set; }

        
        public int CodigoSede { get; set; }

        
        public int CodigoSocio { get; set; }

        
        public string RazonSocial_Sr { get; set; }

        
        public string RUC_DNI { get; set; }

        
        public string Direccion { get; set; }

        
        public DateTime FechaVenta { get; set; }

        
        public string DescFechaVenta { get; set; }

        
        public int CodigoTipoComprobante { get; set; }

        
        public string NroComprobante { get; set; }

        
        public string NombreComprobante { get; set; }

        
        public string NroTarjeta { get; set; }

        
        public decimal SubTotal { get; set; }

        
        public decimal IGV { get; set; }

        
        public decimal TotalNeto { get; set; }

        
        public int TipoMoneda { get; set; }

        
        public int FormaPago { get; set; }

        
        public string DescFormaPago { get; set; }

        
        public decimal tipoCambio { get; set; }

        
        public bool Estado { get; set; }

        
        public string DesEstado { get; set; }

        
        public string UsuarioCreacion { get; set; }

        
        public DateTime FechaCreacion { get; set; }

        
        public string Comentario { get; set; }

        
        public string Hora { get; set; }

        
        public int EstadoImpresora { get; set; }

        ////////////////////////////////
        //////// PARA IMPRIMIR /////////
        ////////////////////////////////

        
        public int Cantidad { get; set; }

        
        public string DescripcionProducto { get; set; }

        
        public decimal ImporteProducto { get; set; }

        
        public int CodigoVenta { get; set; }

        
        public string NombreEmpresa { get; set; }

        
        public string NombreCliente { get; set; }

        
        public string DireccionEmpresa { get; set; }

        
        public string DireccionCliente { get; set; }

        
        public string DistritoEmpresa { get; set; }

        
        public string Distrito { get; set; }

    }

    public class ReporteAgendaClienteDTO
    {

        
        public string NombreCompleto { get; set; }

        
        public string Nombre { get; set; }

        
        public string Apellidos { get; set; }

        
        public string Facebook { get; set; }
        
        public string UsuarioCreacion { get; set; }
        
        public string Telefono { get; set; }

        
        public string Celular { get; set; }

        
        public string Asunto { get; set; }

        
        public string DesPaquete { get; set; }

        
        public string DesTiempoPaquete { get; set; }

        
        public Decimal Costo { get; set; }

        
        public string Vendedor { get; set; }

        
        public string DescTipoAgenda { get; set; }

        
        public string ColorAgenda { get; set; }

        
        public string DesTipoCliente { get; set; }

        
        public DateTime HoraInicioAgenda { get; set; }
        
        public DateTime HoraFinAgenda { get; set; }

       
        public string DesFechaHoraInicio { get; set; }

        
        public string AsuntoAgenda { get; set; }
        
        public string DescEstadoAgenda { get; set; }
        
        public int EstadoAgenda { get; set; }
        
        public string imagenActualizar { get; set; }
        
        public string BotonActualizar { get; set; }
        
        public string BotonVerAgenda { get; set; }
        
        public string DescTotalTipoAgenda { get; set; }
        
        public int Cantidad { get; set; }
        
        public int CodigoSocio { get; set; }
        
        public int Codigo { get; set; }
        
        public int TipoCliente { get; set; }

        
        public int TipoAgenda { get; set; }

        
        public string DesFechaCreacion { get; set; }
        
        
        public DateTime FechaCreacion { get; set; }

    }

    public class ReporteVentasMembresiaSaldosDTO
    {

        
        public string NombresCliente { get; set; }
        
        public string Celular { get; set; }
        
        public string NombrePaquete { get; set; }
        
        public string FechaInicio { get; set; }
        
        public string FechaFin { get; set; }
        
        public decimal Costo { get; set; }
        
        public decimal MontoTotal { get; set; }
        
        public decimal Deben { get; set; }
        
        public string Mes { get; set; }
        
        public string UsuarioCreacion { get; set; }
    }

    public class ReporteVentasProductosDTO
    {
        
        public int CodigoProducto { get; set; }
        
        public string DescProducto { get; set; }
        
        public int CantProducto { get; set; }
        
        public decimal MontoProducto { get; set; }
    }


    public class ReporteSociosDTO
    {
        
        public int CodigoSocio { get; set; }

        
        public string DescSede { get; set; }

        
        public int CodigoSede { get; set; }

        
        public int TipoAgenda { get; set; }

        
        public int CodigoMembresia { get; set; }

        
        public string Nombres { get; set; }

        
        public string Vendedor { get; set; }

        
        public string Apellidos { get; set; }

        
        public string NombreCompleto { get; set; }

        
        public string NombreApellido { get; set; }

        
        public string Ubicaciones { get; set; }

        
        public string DNI { get; set; }

        
        public string TelefonoCelular { get; set; }

        
        public string Telefono { get; set; }

        
        public string Celular { get; set; }

        
        public string Correo { get; set; }

        
        public DateTime Fecha { get; set; }

        
        public string Nuevos { get; set; }
        
        
        public string Bono { get; set; }

        
        public string Renovacion { get; set; }

        
        public decimal TotalNuevo { get; set; }
        
        
        public decimal TotalBono { get; set; }

        
        public decimal TotalRenovacion { get; set; }
        

        
        public string FormaPagoEfectivo { get; set; }

        
        public decimal TotalEfectivo { get; set; }
        
        public string FormaPagoDebito { get; set; }
        
        public decimal TotalDebito { get; set; }
        
        public string FormaPagoCredito { get; set; }
        
        public decimal TotalCredito { get; set; }
             
        
        public string FormaPagoDeposito { get; set; }
        
        public decimal TotalDeposito { get; set; }

        
        public string FormaPagoWeb { get; set; }
        
        public decimal TotalWeb { get; set; }

        
        public string FormaPagoCuponera { get; set; }
        
        public decimal TotalCuponera { get; set; }

        
        public string Ocupacion { get; set; }

        
        public int ReferidoPor { get; set; }

        
        public string DesReferidoPor { get; set; }

        
        public int TipoCliente { get; set; }

        
        public string DesTipoCliente { get; set; }

        
        public string DescTipoCliente { get; set; }

        
        public int flag { get; set; }

        
        public DateTime FechaNacimiento { get; set; }

        
        public string DesFechaNacimiento { get; set; }

        
        public string FechaInicio { get; set; }

        
        public string FechaFin { get; set; }

        
        public DateTime Param_FechaAusente { get; set; }

        
        public DateTime FechaIngreso { get; set; }

        
        public string ImagenUrl { get; set; }

        
        public bool Estado { get; set; }

        
        public string Genero { get; set; }

        
        public string Huella { get; set; }

        
        public int Edad { get; set; }

        
        public string UrlFacebook { get; set; }

        
        public string flagCumpleanios { get; set; }

        
        public string Direccion { get; set; }

        
        public string Distrito { get; set; }

        
        public string EstadoCliente { get; set; }

        
        public string StiloMarcarAsistencia { get; set; }

        
        public int NumeroEstadoCliente { get; set; }

        
        public string ColorNumeroEstadoCliente { get; set; }

        
        public int CodigoDato { get; set; }

        
        public string EstadoSocio { get; set; }

        
        public string NombreMembresia { get; set; }

        
        public string FechasDelEstado { get; set; }

        
        public int Anio { get; set; }

        
        public int Mes { get; set; }

        
        public string DescTipoAgenda { get; set; }

        
        public string ColorAgenda { get; set; }

        
        public string AsuntoAgenda { get; set; }

        
        public string EncargadoAgenda { get; set; }

        
        public string DescEstadoAgenda { get; set; }

        
        public decimal Precio { get; set; }

        
        public decimal Pago { get; set; }

        
        public decimal Debe { get; set; }

        
        public int CantidadVencidos { get; set; }

        
        public int CantidadRenovaron { get; set; }

        
        public int CantidadMatriculados { get; set; }

        
        public int CantidadActivos { get; set; }

        
        public int CantidadProspectos { get; set; }

        
        public int CantidadInactivos { get; set; }

        
        public int CantidadMembresias { get; set; }

        
        public decimal ImporteMembresias { get; set; }

        
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

        
        public int CantidadEgresosEventos { get; set; }

        
        public int CantidadEgresos { get; set; }

        
        public decimal TotalUtilidades { get; set; }

        
        public decimal MontoComprometido { get; set; }

        
        public string FechaCuota { get; set; }

        
        public DateTime HoraInicioAgenda { get; set; }

        
        public DateTime HoraFinAgenda { get; set; }

    }

    public class ReporteVentasServiciosDTO
    {
        
        public int CodigoServicio { get; set; }
        
        public string DescServicio { get; set; }
        
        public int CantServicio { get; set; }
        
        public decimal MontoServicio { get; set; }
    }


    public class ReporteVentasEventosDTO
    {
        
        public int CodigoEvento { get; set; }
        
        public string DescEvento { get; set; }
        
        public int CantEvento { get; set; }
        
        public decimal MontoEvento { get; set; }
    }
    

    public class ReporteVentasLibresDTO
    {
        
        public int CodigoLibre { get; set; }
        
        public string DescLibre { get; set; }
        
        public int CantLibre { get; set; }
        
        public decimal MontoLibre { get; set; }
    }


}
