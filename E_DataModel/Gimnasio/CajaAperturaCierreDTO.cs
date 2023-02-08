
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class CajaAperturaCierreDTO : AuditoriaDTO
	{
	
		
        public int Codigo { get; set; }
        
		
		public string NombreCompleto { get; set; }
	
		
        public string Descripcion { get; set; }

        
        public string funtion { get; set; }
	    
        
        public string DescEstado { get; set; }

		
		public int CodigoTurno { get; set; }
        
        
        public int CodigPerfil { get; set; }

        
        public DateTime Fecha { get; set; }

        
        public string desFechaAbrirCaja { get; set; }

        
        public string desFechaCerrarCaja { get; set; }

        
        public string desFecha { get; set; }
	
		
		public decimal MontoApertura { get; set; }

		
		public int Estado { get; set; }

        
        public string DescFechaApertura { get; set; }

        
        public string DescFechaCierre { get; set; }

        
        public decimal TotalMenbresias { 
            get {
                decimal _TotalMenbresias = 0;
                _TotalMenbresias = (TotalMenbresias_efectivo + TotalMenbresias_debito + TotalMenbresias_credito + TotalMenbresias_deposito + TotalMenbresias_web);
                return _TotalMenbresias;
            }
            set { } 
        }

        public decimal TotalMenbresias_efectivo { get; set; }
        
        public decimal TotalMenbresias_debito { get; set; }
        
        public decimal TotalMenbresias_credito { get; set; }
        
        public decimal TotalMenbresias_deposito { get; set; }
        
        public decimal TotalMenbresias_web { get; set; }

        
        public decimal TotalProductos { 
            get {
                decimal _TotalProductos = 0;
                _TotalProductos = (TotalProductos_efectivo + TotalProductos_debito + TotalProductos_credito + TotalProductos_deposito+ TotalProductos_web);
                return _TotalProductos;
            } 
            set { }
        }

        
        public decimal TotalProductos_efectivo { get; set; }
        
        public decimal TotalProductos_debito { get; set; }
        
        public decimal TotalProductos_credito { get; set; }
        
        public decimal TotalProductos_deposito { get; set; }
        
        public decimal TotalProductos_web { get; set; }
        
        
        public decimal TotalServicios { get; set; }

        
        public decimal TotalLibres { 
            get {
                decimal _TotalLibres = 0;
                _TotalLibres = (TotalLibres_efectivo + TotalLibres_debito + TotalLibres_credito + TotalLibres_deposito + TotalLibres_web);
                return _TotalLibres;
            } set { } }

        
        public decimal TotalLibres_efectivo { get; set; }
        
        public decimal TotalLibres_debito { get; set; }
        
        public decimal TotalLibres_credito { get; set; }
        
        public decimal TotalLibres_deposito { get; set; }
        
        public decimal TotalLibres_web { get; set; }

        
        public decimal TotalRopa { 
            get { 
                decimal _TotalRopa = 0;
                _TotalRopa = (TotalRopa_efectivo + TotalRopa_debito + TotalRopa_credito + TotalRopa_deposito + TotalRopa_web);
                return _TotalRopa;
            } 
            set { } }

        
        public decimal TotalRopa_efectivo { get; set; }
        
        public decimal TotalRopa_debito { get; set; }
        
        public decimal TotalRopa_credito { get; set; }
        
        public decimal TotalRopa_deposito { get; set; }
        
        public decimal TotalRopa_web { get; set; }
                
        public decimal TotalSuplementos { 
            get {
                decimal _TotalSuplementos = 0;
                _TotalSuplementos = (TotalSuplementos_efectivo +TotalSuplemento_debito + TotalSuplementos_credito + TotalSuplementos_deposito + TotalSuplementos_web);
                return _TotalSuplementos;   
            } 
            set { } }
        
        public decimal TotalSuplementos_efectivo { get; set; }
        
        public decimal TotalSuplemento_debito { get; set; }
        
        public decimal TotalSuplementos_credito { get; set; }
        
        public decimal TotalSuplementos_deposito { get; set; }
        
        public decimal TotalSuplementos_web { get; set; }

        public decimal TotalAccesorios { 
            get {
                decimal _TotalAccesorios = 0;
                _TotalAccesorios = (TotalAccesorios_efectivo + TotalAccesorios_debito + TotalAccesorios_credito + TotalAccesorios_deposito+ TotalAccesorios_web);
                return _TotalAccesorios;
            } 
            set { } 
        }

        public decimal TotalAccesorios_efectivo { get; set; }

        public decimal TotalAccesorios_debito { get; set; }

        public decimal TotalAccesorios_credito { get; set; }

        public decimal TotalAccesorios_deposito { get; set; }

        public decimal TotalAccesorios_web { get; set; }


        public decimal TotalEventos { get; set; }

        
        public decimal TotalEgresos { get; set; }

        
        public decimal TotalEgresosEventos { get; set; }

        
        public decimal SumaTotal { 
            get {
                decimal _sumatoriaTotal = 0;
                _sumatoriaTotal = (TotalMenbresias_efectivo + TotalProductos_efectivo + TotalLibres_efectivo + TotalSuplementos_efectivo + TotalRopa_efectivo + TotalAccesorios_efectivo);
                return _sumatoriaTotal;
            } 
            set { } 
        }

        
        public decimal TotalDeudas { get; set; }
        
        public decimal Total { 
            get {
                decimal _total = 0;
                _total = (TotalMenbresias_efectivo + TotalProductos_efectivo + TotalLibres_efectivo + TotalSuplementos_efectivo + TotalRopa_efectivo + TotalAccesorios_efectivo + DineroAjusteCaja + MontoApertura) - TotalEgresos;
                return _total;
            } 
            set { } 
        }

        
        public decimal GastosCaja { get; set; }

        
        public decimal DineroDejadoCajaChica { get; set; }

        
        public decimal DineroRetirado { get; set; }

       
        public decimal DineroAjusteCaja { get; set; }

        
        public int CodigoResponsable { get; set; }

        
        public decimal MontoCierre { get; set; }

        
        public string desEstado { get; set; }

        
        public decimal Faltante { get; set; }

      
        
        public string Observacion { get; set; }

        
        public int TipoApertura { get; set; }

        
        public string DescEstadoTipoApertura { get; set; }

        
        public int CantidadTotalFilas { get; set; }

        
        public int TamanioPagina { get; set; }

        
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqCajaAperturaCierreDTO : Request
	{
		
        public List<CajaAperturaCierreDTO> List { get; set; }
	}
	
	
    public class ReqFilterCajaAperturaCierreDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public CajaAperturaCierreDTO Item { get; set; }
        
        public Common.filterCaseCajaAperturaCierre FilterCase { get; set; }
    }
	
	
    public class RespCajaAperturaCierreDTO : Response
    {
      
    }
	
	
    public class RespItemCajaAperturaCierreDTO : Response
    {
        
        public CajaAperturaCierreDTO Item { get; set; } 
    }

    
    public class RespListCajaAperturaCierreDTO : Response
    {
        
        public List<CajaAperturaCierreDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	