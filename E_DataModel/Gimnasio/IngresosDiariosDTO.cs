using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.Gimnasio
{    
    public class IngresosDiariosDTO
    {
        
        public string razonSocial_sr { get; set; }
        
        public string ruc_dni { get; set; }
        
        public string direccion { get; set; }
        
        public string DesTipoComprobante { get; set; }
        
        public string NroComprobante { get; set; }
        
        public decimal TotalEfectivosSoles { get; set; }
        
        public decimal TotalEfectivosDolares { get; set; }
        
        public decimal TotalTarjetaSoles { get; set; }
        
        public decimal TotalTarjetaDolares { get; set; }
        
        public string NroTarjeta { get; set; }

        
        public DateTime FechaCreacion { get; set; }  //Acabo de crearlo

        
        public int param_Tipo { get; set; }
        
        public int param_Anio { get; set; }
        
        public int param_Mes { get; set; }
        
        public int param_Dia { get; set; }

        
        public DateTime Fecha { get; set; }
        
        public string Vendedor { get; set; }
        
        public int CodigoSede { get; set; }

        
        public int Cantidad { get; set; }

        
        public decimal PrecioUnitario { get; set; }

        
        public string Descripcion { get; set; }

        
        public decimal Importe { get; set; }

        
        public string UsuarioCreacion { get; set; }

        
        public decimal MontoTotal { get; set; }
        
        public string TotalPagosVentas { get; set; }
        
        public decimal DesTotalPagosVentas { get; set; }

        
        public string FormaPagoEfectivo { get; set; }
        
        public decimal TotalEfectivo { get; set; }
        
        public string FormaPagoDebito { get; set; }
        
        public decimal TotalDebito { get; set; }
        
        public string FormaPagoCredito { get; set; }
        
        public decimal TotalCredito { get; set; }
    }

    
}
