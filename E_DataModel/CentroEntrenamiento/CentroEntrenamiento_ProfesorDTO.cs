
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.CentroEntrenamiento
{
    public class CentroEntrenamiento_ProfesorDTO: AuditoriaDTO
    {
        public string CodigoProfesional { get; set; }

        public string NombreCompleto { get; set; }
        public string Nombres           { get; set; }
        public string Apellidos         { get; set; }
        public int TipoDocumento     { get; set; }
        public string NroDocumento      { get; set; }
        public string Telefono          { get; set; }
        public string Celular           { get; set; } 
        public string EstadoCelular { get; set; }
        public string Correo            { get; set; }
        public DateTime FechaNacimiento   { get; set; }
        public string ImagenUrl         { get; set; }
        public string Genero            { get; set; }
        public string Facebook          { get; set; }
        public string Ubigeo            { get; set; }
        public string Direccion         { get; set; }
        public string Distrito          { get; set; }

        public decimal CostoPorClase { get; set; }
        public decimal DescuentoPorminuto { get; set; }

        public bool Estado            { get; set; }
        public Common.Operation Operation { get; set; }
        public string Accion { get; set; }

        public string validacionBusqueda { get; set; }

    }


    public class ReqCentroEntrenamiento_ProfesorDTO : Request 
    {
        public List<CentroEntrenamiento_ProfesorDTO> List { get; set; }
    }

    public class ReqFilterCentroEntrenamiento_ProfesorDTO : Request 
    {
        public Common.Paging Paging { get; set; }
        public CentroEntrenamiento_ProfesorDTO Item { get; set; }
        public Common.filterCaseCentroEntrenamiento_Profesor FilterCase { get; set; }
    }

    public class RespCentroEntrenamiento_ProfesorDTO : Response 
    {

    }

    public class RespItemCentroEntrenamiento_ProfesorDTO : Response
    {
        public CentroEntrenamiento_ProfesorDTO Item { get; set; }
    }

    public class RespListCentroEntrenamiento_ProfesorDTO : Response
    {
        public List<CentroEntrenamiento_ProfesorDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    } 

}
