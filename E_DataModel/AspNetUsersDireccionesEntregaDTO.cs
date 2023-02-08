using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Base;

namespace E_DataModel
{
    public class AspNetUsersDireccionesEntregaDTO : AuditoriaDTO
    {           
          public string  IdUser         { get; set; }
          public string CodigoDireccion { get; set; }
          public string Nombres        { get; set; }
          public string Apellidos      { get; set; }
          public string Celular        { get; set; }
          public string Telefono { get; set; }
          public int  TipoDireccion  { get; set; }
         public string DesTipoDireccion { get; set; }
          public string Direccion      { get; set; }
          public string NroLote        { get; set; }
          public string DeptoInt       { get; set; }
          public string Urbanizacion   { get; set; }
          public string Referencia     { get; set; }
          public string Ubigeo { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public Boolean DireccionDefault { get; set; }
        public decimal PrecioEnvio { get; set; }
        public int TipoTiempoEntrega { get; set; }
        public int TiempoEntrega { get; set; }

        public string Accion { get; set; }
        public string CorreoUsuario { get; set; }
        public string Email { get; set; }
        public Common.Operation Operation { get; set; }
    }


    public class ReqAspNetUsersDireccionesEntregaDTO : Request
    {
        public List<AspNetUsersDireccionesEntregaDTO> List { get; set; }
    }

    public class ReqFilterAspNetUsersDireccionesEntregaDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public AspNetUsersDireccionesEntregaDTO Item { get; set; }
        public Common.filterCaseAspNetUsersDireccionesEntrega FilterCase { get; set; }
    }

    public class RespAspNetUsersDireccionesEntregaDTO : Response
    {

    }

    public class RespItemAspNetUsersDireccionesEntregaDTO : Response
    {
        public AspNetUsersDireccionesEntregaDTO Item { get; set; }
    }

    public class RespListAspNetUsersDireccionesEntregaDTO : Response
    {
        public List<AspNetUsersDireccionesEntregaDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }

}
