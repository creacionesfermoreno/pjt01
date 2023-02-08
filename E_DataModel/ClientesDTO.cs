using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Base;

namespace E_DataModel
{
    public class ClientesDTO: AuditoriaDTO
    {      
		public int CodigoCliente       { get; set; }
		public string Nombres             { get; set; }
		public string Apellidos           { get; set; }
		public string Telefono            { get; set; }

        public string Celular { get; set; }
        public string CorreoElectronico { get; set; }
        
       public int CodigoTipoIdentificacion { get; set; }
       public string Identificacion           { get; set; }
       public string Direccion                { get; set; }
       public string Departamento             { get; set; }
       public string provincia                { get; set; }
       public string Distrito                 { get; set; }
       public string Urbanizacion             { get; set; }
       public string CodigoUbigeo             { get; set; }
      
       public int CodigoVendedor           { get; set; }
       public string DireccionDelivery { get; set; }       
        public Common.Operation Operation { get; set; }

    }

    public class ReqClientesDTO : Request
    {
        public List<ClientesDTO> List { get; set; }
    }

    public class ReqFilterClientesDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public ClientesDTO Item { get; set; }
        public Common.filterCaseClientes FilterCase { get; set; }
    }

    public class RespClientesDTO : Response
    {

    }

    public class RespItemClientesDTO : Response
    {
        public ClientesDTO Item { get; set; }
    }

    public class RespListClientesDTO : Response
    {
        public List<ClientesDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }

}      
