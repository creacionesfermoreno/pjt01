using System;

namespace BotComers.ViewModels.Ingresos
{
    public class ComprobantePagoViewModel
    {
        public int CodigoUnidadNegocio { get; set; }
        public int CodigoSede { get; set; }
        public int CodigoComprobantePago { get; set; }
        public int CodigoComprobante { get; set; }
        public int CodigoCuentaBancaria { get; set; }
        public int CodigoMetodoPago { get; set; }
        public Decimal TipoMoneda { get; set; }
        public Decimal Monto { get; set; }
        public string Nota { get; set; }
        public int Estado { get; set; }
        public string Accion { get; set; }
        public string UsuarioCreacion { get; set; }
    }
}