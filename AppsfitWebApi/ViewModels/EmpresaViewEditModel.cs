using System;

namespace AppsfitWebApi.ViewModels
{
    public class EmpresaViewEditModel
    {
        public int? CodigoUnidadNegocio { get; set; }
        public int? CodigoSede { get; set; }
        public string NombreDuenio { get; set; }
        public string ApellidosDuenio { get; set; }
        public string CorreoDuenio { get; set; }
        public string CodigoPais { get; set; }
        public string CelularDuenio { get; set; }
        public int TipoDocumentoEmpresa { get; set; }
        public string NroDocumentoEmpresa { get; set; }
        public string RazonSocialEmpresa { get; set; }
        public string DireccionEmpresa { get; set; }
        public string NombreComercialEmpresa { get; set; }
        public string TelefonoEmpresa { get; set; }
        public DateTime FechaAniversarioEmpresa { get; set; }
        public string CorreoEmpresa { get; set; }
        public string SubDominio { get; set; }
        public string LogoTipo { get; set; }
        public string Ubigeo { get; set; }
        public string IdUser { get; set; }

        public int Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string Accion { get; set; }
        public string ColorEmpresa { get; set; }
        public string IdEmpresa { get; set; }
        public bool TieneFacturacionElectronica { get; set; }
        public bool TiendaAplicacion { get; set; }
        public bool AplicacionDisponible { get; set; }
        public bool RutinasAplicacion { get; set; }

        public string DefaultKeyEmpresa { get; set; }

        public string DefaultKeyUser { get; set; }
    }
}