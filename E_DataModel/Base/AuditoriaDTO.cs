using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Common;

namespace E_DataModel.Base
{
    public class AuditoriaDTO
    {
        public int CodigoUnidadNegocio { get; set; }
        public int CodigoSede { get; set; }
        public string UsuarioCreacion { get; set; }      
        public DateTime FechaCreacion { get; set; }      
        public string UsuarioEdicion { get; set; }
        public DateTime FechaEdicion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public DateTime FechaEliminacion { get; set; }

        public int CodigoInicioSesion { get; set; }

        public string DescFechaCreacion { get; set; }
        public string DescFechaEdicion { get; set; }

        public string DefaultKeyUser { get; set; }
        public string DefaultKeyEmpresa { get; set; }

    }
}
