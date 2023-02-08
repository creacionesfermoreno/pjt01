using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Base;

namespace E_DataModel
{
    public class CategoriasDTO : AuditoriaDTO
    {           
        public int CodigoMenu { get; set; }
        public int CodigoMenuSuperior { get; set; }
        public string Descripcion { get; set; }
        public string UrlUbicacion { get; set; }
        public string UrlImagen { get; set; }
        public string CodigoImagenPortada { get; set; }
        public string Tipo { get; set; }
        public int Orden { get; set; }
        public int Estado { get; set; }

        public List<ItemsVentaDTO> listaItemsVenta { get; set; }

        public Common.Operation Operation { get; set; }
    }
    
    public class ReqCategoriasDTO : Request
    {
        public List<CategoriasDTO> List { get; set; }
    }

    public class ReqFilterCategoriasDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public CategoriasDTO Item { get; set; }
        public Common.filterCaseCategorias FilterCase { get; set; }
    }

    public class RespCategoriasDTO : Response
    {

    }

    public class RespItemCategoriasDTO : Response
    {
        public CategoriasDTO Item { get; set; }
    }

    public class RespListCategoriasDTO : Response
    {
        public List<CategoriasDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }


}
