using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Base;

namespace E_DataModel.Corporativo
{
    public class EmpresaDTO: AuditoriaDTO
    {       
        public string NombreDuenio { get; set; }
        public string ApellidosDuenio { get; set; }
        public string CorreoDuenio { get; set; }
        public string CodigoPais { get; set; }
        public string CelularDuenio { get; set; }
        public int TipoDocumentoEmpresa { get; set; }
        public string DesTipoDocumentoEmpresa { get; set; }
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
        public string DesEstado { get; set; }

        public string IdEmpresa { get; set; }
        public string IdUser { get; set; }
        public string ColorEmpresa { get; set; }  
        
        public string Ubigeo { get; set; }  
        public bool TieneFacturacionElectronica { get; set; }  
        public bool TiendaAplicacion { get; set; }  
        public bool AplicacionDisponible { get; set; }  
        public bool RutinasAplicacion { get; set; }  
        public Common.Operation Operation { get; set; }


    }
    
    public class ReqEmpresaDTO : Request
    {
        public List<EmpresaDTO> List { get; set; }
    }

    public class ReqFilterEmpresaDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public EmpresaDTO Item { get; set; }
        public Common.filterCaseEmpresa FilterCase { get; set; }
    }
    
    public class RespEmpresaDTO : Response
    {

    }
    
    public class RespItemEmpresaDTO : Response
    {
        public EmpresaDTO Item { get; set; }
    }

    public class RespListEmpresaDTO : Response
    {       
        public List<EmpresaDTO> List { get; set; }       
        public Common.Paging Paging { get; set; }
    }

}
