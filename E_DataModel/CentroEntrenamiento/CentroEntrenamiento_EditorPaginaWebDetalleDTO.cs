using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
using System.Web;

namespace E_DataModel.CentroEntrenamiento
{
    public class CentroEntrenamiento_EditorPaginaWebDetalleDTO : AuditoriaDTO
    {
        public string Codigo      { get; set; }
        public string Tipo        { get; set; }
        public string Titulo      { get; set; }
        public string SubTitulo   { get; set; }
        public string UrlUmagen { get; set; }

        public string LinkPago { get; set; }
        
        public Common.Operation Operation { get; set; }    
    }
    
    public class ReqCentroEntrenamiento_EditorPaginaWebDetalleDTO : Request
    {
        public List<CentroEntrenamiento_EditorPaginaWebDetalleDTO> List { get; set; }
    }

    public class ReqFilterCentroEntrenamiento_EditorPaginaWebDetalleDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public CentroEntrenamiento_EditorPaginaWebDetalleDTO Item { get; set; }
        public Common.filterCaseCentroEntrenamiento_EditorPaginaWebDetalle FilterCase { get; set; }
    }

    public class RespCentroEntrenamiento_EditorPaginaWebDetalleDTO : Response
    {

    }

    public class RespItemCentroEntrenamiento_EditorPaginaWebDetalleDTO : Response
    {
        public CentroEntrenamiento_EditorPaginaWebDetalleDTO Item { get; set; }
    }

    public class RespListCentroEntrenamiento_EditorPaginaWebDetalleDTO : Response
    {
        public List<CentroEntrenamiento_EditorPaginaWebDetalleDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }


}
