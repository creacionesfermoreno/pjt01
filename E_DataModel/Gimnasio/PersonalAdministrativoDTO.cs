using E_DataModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace E_DataModel.Gimnasio
{
    
    
    public class PersonalAdministrativoDTO : AuditoriaDTO
    {
        public string CodigoPersonalAdministrativo { get; set; }
        
        public int TipoNumeroDocumentoTm { get; set; }
        
        public string NumeroDocumento { get; set; }
        
        public string Nombres { get; set; }
        
        public string ApellidoPaterno { get; set; }

        public string NombreCompleto
        {
            get
            {
                var _valor = Nombres + " " + ApellidoPaterno;
                return _valor;
            }
        }

        public string ApellidoMaterno { get; set; }
        
        public int SexoTm { get; set; }
        
        public DateTime FechaNacimiento { get; set; }
        
        public int EstadoCivilTm { get; set; }
        
        public string UrlImagen { get; set; }
        
        public int CodigoCargo { get; set; }
        
        public string DescripcionCargo { get; set; }
        
        public DateTime FechaInicioEmpresa { get; set; }
        
        public DateTime? FechaCese { get; set; }
        
        public int MotivoCeseTm { get; set; }
        
        public string ObservacionCese { get; set; }
        
        public string Direccion { get; set; }
        
        public string Celular { get; set; }
        public string EstadoCelular { get; set; }
        
        public string Email { get; set; }
        
        public decimal SueldoBase { get; set; }
        
        public decimal MontoDescuento { get; set; }
        
        public bool Estado { get; set; }
        public string Vigencia { get; set; }
        
        public string DesVigencia { get; set; }
        
        public Common.Operation Operation { get; set; }
        
        public string Xml { get; set; }
        
        public PersonalAsistenciaConfiguracionDTO AsistenciaConfiguracion { get; set; }

    }

    
    public class ReqPersonalAdministrativoDTO : Request
    {
        
        public List<PersonalAdministrativoDTO> List { get; set; }
        
        public string Usuario { get; set; }
    }

    
    public class ReqFilterPersonalAdministrativoDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public PersonalAdministrativoDTO Item { get; set; }
        
        public Common.filterCasePersonalAdministrativo FilterCase { get; set; }
    }

    
    public class RespPersonalAdministrativoDTO : Response
    {

    }

    
    public class RespItemPersonalAdministrativoDTO : Response
    {
        
        public PersonalAdministrativoDTO Item { get; set; }
    }

    
    public class RespListPersonalAdministrativoDTO : Response
    {
        
        public List<PersonalAdministrativoDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
}
