
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	//Archivo     : AdFitnessAtencionAlClienteDTO.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 25/07/2017
	//Descripcion : Entidad de negocio
	
	
	public class AdFitnessAtencionAlClienteDTO : AuditoriaDTO
    {
		public int Codigo { get; set; }
	
		
		public int Estado { get; set; }

        
        public DateTime FechaDesde { get; set; }

        
        public DateTime FechaHasta{ get; set; }

        
        public int CantTotal { get; set; }

        
		public int TamanioPagina { get; set; }

		
		public string Nombres { get; set; }
	
		
		public string Telefono { get; set; }

    	
		public string CelularEnviarVoucher { get; set; }

        
		public string RazonSocial { get; set; }

        
		public string SubDominio { get; set; }
	
		
		public string Mensaje { get; set; }
	
		
		public int EstadoAtendido { get; set; }

        
        public int Existe { get; set; }

        
        public DateTime FechaPago { get; set; }

        
        public DateTime FechaVencimiento { get; set; }

        
        public string FechaPagoTexto { get; set; }

        
        public string FechaVencimientoPagoTexto { get; set; }
        public string FechaVencimientoDemoTexto { get; set; }

        public string TipoMoneda { get; set; }

        
        public decimal MontoMensualidad { get; set; }

        
        public string EntidadBancaria { get; set; }

        
        public string NroCuenta { get; set; }

        
        public string ResponsableCuenta { get; set; }

        
        public string CCI { get; set; }

        public string PlanEmpresa { get; set; }

        public string EstadoEmpresa { get; set; }

        public string EstadoFinPrueba { get; set; }


        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqAdFitnessAtencionAlClienteDTO : Request
	{
		
        public List<AdFitnessAtencionAlClienteDTO> List { get; set; }
	}
	
	
    public class ReqFilterAdFitnessAtencionAlClienteDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public AdFitnessAtencionAlClienteDTO Item { get; set; }
        
        public Common.filterCaseAdFitnessAtencionAlCliente FilterCase { get; set; }
    }
	
	
    public class RespAdFitnessAtencionAlClienteDTO : Response
    {
      
    }
	
	
    public class RespItemAdFitnessAtencionAlClienteDTO : Response
    {
        
        public AdFitnessAtencionAlClienteDTO Item { get; set; } 
    }

    
    public class RespListAdFitnessAtencionAlClienteDTO : Response
    {
        
        public List<AdFitnessAtencionAlClienteDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	