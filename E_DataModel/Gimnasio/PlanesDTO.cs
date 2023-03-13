using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class PlanesDTO : AuditoriaDTO
	{
        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }

        
		public int CodigoPaquete { get; set; }

       
        public int CodigoPaqueteCursoProfesor { get; set; }

        
        public int CodigoTipoPaquete { get; set; }

        
        public int TiempoMembresia { get; set; }
        public string DesTiempoMembresia { get; set; }

        public int CantidaIngresos { get; set; }

        
        public int CantCupos { get; set; }

        
        public int NroCupo { get; set; }

        
        public int NroIngresoDia { get; set; }

        
        public int CodigoProfesor { get; set; }

        
        public string Busqueda { get; set; }

       
        public int Anio { get; set; }

        
        public int Mes { get; set; }

        
        public int Dia { get; set; }

	    
        public int CantidadRegistros { get; set; }

		
		public string Descripcion { get; set; }

        
        public int ValorDias_Tiempo { get; set; }

        
        public string DesPaquete { get; set; }
        public string UrlImage { get; set; }

        
        public decimal ImporteTotal { get; set; }
	    
		
		public int valor { get; set; }

        
        public decimal Costo { get; set; }

        
        public string Codigovalor { get; set; }

        
        public bool Estado { get; set; }  
        public bool EstadoMembresiaInterdiaria { get; set; }

        
        public int EstadoPaquete { get; set; }

        
        public string Check { get; set; }

        
        public DateTime FechaInicio{ get; set; }

        
        public int EstadoFecha { get; set; }

        
        public DateTime FechaVencimiento { get; set; }

        
        public string DesEstado { get; set; }

        
        public string ColorDesEstado { get; set; }

        
        public string DescTipoPaquete { get; set; }

       
        public string desNomProfesor { get; set; }

        
        public int CodigoCliente { get; set; }

        
        public int CodigoMenbresia { get; set; }

        
        public int CodigoTipoCliente { get; set; }

        
        public string DescripcionTipoCliente { get; set; }

        
        public int CongelamientoVigente { get; set; }

		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
        
        
        public int TamanioPagina { get; set; }
        public bool ShowApp { get; set; }

        //for app
        public int ValorTiempoPlan { get; set; }
        public int ValorSesiones { get; set; }
        public string DesTipoPaquete { get; set; }

        //suscripcion
        public string CodigoPlantillaFormaPago { get; set; }
        public string IdSuscripcionPasarela { get; set; }
        public string CodigoPaqueteSuscripcion { get; set; }
        public string DesPasarelaPago { get; set; }
        public string DesSuscripcionPlan { get; set; }
        public bool Suscripcion { get; set; }
        public string CodigoMembresiasSuscripcion { get; set; }
        public string IdClientePasarela { get; set; }
        public string DataJsonPasarela { get; set; }
        public string NroDocumento { get; set; }
        public int CodigoMembresia { get; set; }
        public int CodigoSocio { get; set; }
    }
	
	
	public class ReqPlanesDTO : Request
	{
		
        public List<PlanesDTO> List { get; set; }
	}
	
	
    public class ReqFilterPlanesDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public PlanesDTO Item { get; set; }
        
        public Common.filterCasePlanes FilterCase { get; set; }

    }
	
	
    public class RespPlanesDTO : Response
    {
      
    }
	
	
    public class RespItemPlanesDTO : Response
    {
        
        public PlanesDTO Item { get; set; } 
    }

    
    public class RespListPlanesDTO : Response
    {
        
        public List<PlanesDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	