namespace BotComers.ViewModels.Ingresos
{
    public class ClientesViewModel
    {
        public int CodigoUnidadNegocio { get; set; }
        public int CodigoSede { get; set; }
        public int CodigoCliente { get; set; }
        public string NombreCompleto { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }

        public string Celular { get; set; }
        public string CorreoElectronico { get; set; }

        public int CodigoTipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Direccion { get; set; }
        public string Departamento { get; set; }
        public string provincia { get; set; }
        public string Distrito { get; set; }
        public string Urbanizacion { get; set; }
        public string CodigoUbigeo { get; set; }

        public int CodigoVendedor { get; set; }
        public string DireccionDelivery { get; set; }

        public string Accion { get; set; }
        public string UsuarioCreacion { get; set; }

    }
}