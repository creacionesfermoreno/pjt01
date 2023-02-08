using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.CentroEntrenamiento
{
    public class CentroEntrenamiento_EditorPaginaWebDTO : AuditoriaDTO
    {
          public int CodigoPerfil { get; set; }
          public string CodigoPagina                     { get; set; }
          public string NombreComercial { get; set; }
          public string logoPagina { get; set; }
          public string LogoCorporativo { get; set; }

          public string ColorPrincipalPagina { get; set; }
          public string BannerReserva_Titulo             { get; set; }
          public string BannerReserva_Descripcion        { get; set; }
          public string BannerReserva_Descripcion2       { get; set; }
          public string BannerReserva_FondoImagen        { get; set; }
          public Boolean BannerReserva_Estado             { get; set; }
          public string BannerCentro_Titulo              { get; set; }
          public string BannerCentro_Descripcion         { get; set; }
          public string BannerCentro_Beneficio1          { get; set; }
          public string BannerCentro_Beneficio2          { get; set; }
          public string BannerCentro_Beneficio3          { get; set; }
          public string BannerCentro_FondoImagen         { get; set; }
          public Boolean BannerCentro_Estado              { get; set; }
          public string SesionFormulario_Titulo          { get; set; }
          public string SesionFormulario_Descripcion     { get; set; }
          public string SesionFormulario_Tituloformulario { get; set; }
        public string SesionFormulario_CodigoPais { get; set; }

        public string SesionFormulario_Whatsapp { get; set; }

          public string SesionFormulario_Imagen          { get; set; }
          public Boolean SesionFormulario_Estado          { get; set; }
          public string SesionTrainner_Titulo            { get; set; }
          public string SesionTrainner_Descripcion       { get; set; }
          public Boolean SesionTrainner_Estado            { get; set; }
          public string SesionServicio_Titulo            { get; set; }
          public string SesionServicio_Descripcion       { get; set; }
          public Boolean SesionServicio_Estado            { get; set; }
          public string SesionPlan_Titulo                { get; set; }
          public string SesionPlan_Descripcion           { get; set; }
          public Boolean SesionPlan_Estado                { get; set; }
          public string SesionVideo_Titulo               { get; set; }
          public string SesionVideo_Descripcion          { get; set; }
          public string SesionVideo_Linkvideo            { get; set; }
          public Boolean SesionVideo_Estado               { get; set; }
          public string SesionFreepass_Titulo            { get; set; }
          public string SesionFreepass_Descripcion       { get; set; }
          public Boolean SesionFreepass_Estado            { get; set; }
          public string SesionInformacion_Direccion1     { get; set; }
          public string SesionInformacion_Direccion2     { get; set; }
          public string SesionInformacion_Contactanos1   { get; set; }
          public string SesionInformacion_Contactanos2   { get; set; }
          public string SesionInformacion_Horario1       { get; set; }
          public string SesionInformacion_Horario2       { get; set; }
          public string SesionInformacion_LatitudMapa    { get; set; }
          public string SesionInformacion_LongitudMapa   { get; set; }
          public Boolean SesionInformacion_Estado         { get; set; }

          public List<CentroEntrenamiento_EditorPaginaWebDetalleDTO> List_Trainners { get; set; }

        public List<CentroEntrenamiento_EditorPaginaWebDetalleDTO> List_Servicios { get; set; }

        public List<CentroEntrenamiento_EditorPaginaWebDetalleDTO> List_Planes { get; set; }

        public Common.Operation Operation { get; set; }

        public int TipoFoto { get; set; }
        public string UrlImagen { get; set; }
        public string SubDominio { get; set; }

        public string ReservasNormativa { get; set; }
        public string ReservasNotas { get; set; }
        public int ReservasMinutosCancelar { get; set; }

        public string DesPlanEmpresa { get; set; }
        public string EstadoEmpresa { get; set; }

        public string UrlAPISunafact { get; set; }
        public string TokenSunafact { get; set; }
        public bool TieneFacturacionElectronica { get; set; }
    }


    public class ReqCentroEntrenamiento_EditorPaginaWebDTO : Request
    {
        public List<CentroEntrenamiento_EditorPaginaWebDTO> List { get; set; }
    }

    public class ReqFilterCentroEntrenamiento_EditorPaginaWebDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public CentroEntrenamiento_EditorPaginaWebDTO Item { get; set; }
        public Common.filterCaseCentroEntrenamiento_EditorPaginaWeb FilterCase { get; set; }
    }

    public class RespCentroEntrenamiento_EditorPaginaWebDTO : Response
    {

    }

    public class RespItemCentroEntrenamiento_EditorPaginaWebDTO : Response
    {
        public CentroEntrenamiento_EditorPaginaWebDTO Item { get; set; }
    }

    public class RespListCentroEntrenamiento_EditorPaginaWebDTO : Response
    {
        public List<CentroEntrenamiento_EditorPaginaWebDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }

}
