using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace E_DataModel.Gimnasio
{
    
    public class ExportarResumenFacturacionExcelDTO
    {
        
        public string DescripcionCategoria { get; set; }
        
        public decimal Total { get; set; }
        
        public decimal Efectivo { get; set; }
        
        public decimal TarjetaDebito { get; set; }
        
        public decimal TarjetaCredito { get; set; }
        
        public decimal Deposito { get; set; }
        
        public decimal Web { get; set; }
        
        public string Deuda { get; set; }
    }
}
