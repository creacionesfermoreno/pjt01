using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace BotComers.ViewModels
{

    public class EmpresaViewModel
    {
        public EmpresaViewModelLoad loadEmpresa { get; set; }
    }

    public class EmpresaViewModelLoad
    {
        public List<EmpresaViewModelGrid> listGridEmpresa { get; set; }
        public List<MaestroViewModel> listEstadoEmpresa { get; set; }
        public List<MaestroViewModel> listPaisEmpresa { get; set; }
        public List<MaestroViewModel> listTipoDocumentoEmpresa { get; set; }
    }

    public class EmpresaViewModelGrid
    {
        public int CodigoUnidadNegocio { get; set; }
        public int CodigoSede { get; set; }
        [Display(Name = "Tipo Documento")]
        public string DesTipoDocumentoEmpresa { get; set; }
        [Display(Name = "Nro. Documento")]
        public string NroDocumentoEmpresa { get; set; }
        [Display(Name = "Razon Social")]
        public string RazonSocialEmpresa { get; set; }
        [Display(Name = "Nombre Comercial")]
        public string NombreComercialEmpresa { get; set; }
        [Display(Name = "Telefono")]
        public string TelefonoEmpresa { get; set; }
        [Display(Name = "Correo")]
        public string CorreoEmpresa { get; set; }
        public string SubDominio { get; set; }
        public string LogoTipo { get; set; }
        [Display(Name = "Estado")]
        public string DesEstado { get; set; }


    }

    public class EmpresaViewInsertModel
    {
        public int CodigoUnidadNegocio { get; set; }
        public int CodigoSede { get; set; }
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
        public int Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string Accion { get; set; }
        public string IdEmpresa { get; set; }
        public string ColorEmpresa { get; set; }
        public HttpPostedFileWrapper ImageFileLogo { get; set; }
    }

    public class EmpresaViewEditModel
    {
        public int CodigoUnidadNegocio { get; set; }
        public int CodigoSede { get; set; }
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
        public int Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string Accion { get; set; }
        public string ColorEmpresa { get; set; }
        public string IdEmpresa { get; set; }

    }



    public class MaestroViewModel
    {
        public string Filter { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public string valor { get; set; }
        public string urlImagen { get; set; }
    }

}
