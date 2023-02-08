using E_BusinessLayer.CentroEntrenamiento;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.CentroEntrenamiento
{
    public class CentroEntrenamiento_EditorPaginaWebRepository : IDisposable
    {
        public CentroEntrenamiento_EditorPaginaWebDTO CentroEntrenamiento_uspBuscarEdicionPaginaWeb_BannerReserva(CentroEntrenamiento_EditorPaginaWebDTO request)
        {
            CentroEntrenamiento_EditorPaginaWebDTO oItemViewModel = null;

            CentroEntrenamiento_EditorPaginaWebDTO oCentroEntrenamiento_EditorPaginaWebDTO = new CentroEntrenamiento_EditorPaginaWebDTO();
            // oCentroEntrenamiento_EditorPaginaWebDTO.CodigoPagina = request.CodigoPagina;
            oCentroEntrenamiento_EditorPaginaWebDTO.CodigoUnidadNegocio = request.CodigoUnidadNegocio;
            oCentroEntrenamiento_EditorPaginaWebDTO.CodigoSede = request.CodigoSede;

            ReqFilterCentroEntrenamiento_EditorPaginaWebDTO oReq = new ReqFilterCentroEntrenamiento_EditorPaginaWebDTO()
            {
                FilterCase = filterCaseCentroEntrenamiento_EditorPaginaWeb.CentroEntrenamiento_uspBuscarEdicionPaginaWeb_BannerReserva,
                Item = oCentroEntrenamiento_EditorPaginaWebDTO,
                User = "appsfit"
            };
            RespItemCentroEntrenamiento_EditorPaginaWebDTO oResp = null;
            using (CentroEntrenamiento_EditorPaginaWebLogic oCentroEntrenamiento_EditorPaginaWebLogic = new CentroEntrenamiento_EditorPaginaWebLogic())
            {
                oResp = oCentroEntrenamiento_EditorPaginaWebLogic.CentroEntrenamiento_EditorPaginaWebGetItem(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new CentroEntrenamiento_EditorPaginaWebDTO();
                oItemViewModel = oResp.Item;
            }

            return oItemViewModel;
        }

        public CentroEntrenamiento_EditorPaginaWebDTO CentroEntrenamiento_uspBuscarLogoCorporativo(CentroEntrenamiento_EditorPaginaWebDTO request)
        {
            CentroEntrenamiento_EditorPaginaWebDTO oItemViewModel = null;

            CentroEntrenamiento_EditorPaginaWebDTO oCentroEntrenamiento_EditorPaginaWebDTO = new CentroEntrenamiento_EditorPaginaWebDTO();
            //  oCentroEntrenamiento_EditorPaginaWebDTO.CodigoPagina = request.CodigoPagina;
            oCentroEntrenamiento_EditorPaginaWebDTO.CodigoUnidadNegocio = request.CodigoUnidadNegocio;
            oCentroEntrenamiento_EditorPaginaWebDTO.CodigoSede = request.CodigoSede;
            oCentroEntrenamiento_EditorPaginaWebDTO.UsuarioCreacion = request.UsuarioCreacion;

            ReqFilterCentroEntrenamiento_EditorPaginaWebDTO oReq = new ReqFilterCentroEntrenamiento_EditorPaginaWebDTO()
            {
                FilterCase = filterCaseCentroEntrenamiento_EditorPaginaWeb.uspBuscarLogoCorporativo,
                Item = oCentroEntrenamiento_EditorPaginaWebDTO,
                User = "appsfit"
            };
            RespItemCentroEntrenamiento_EditorPaginaWebDTO oResp = null;
            using (CentroEntrenamiento_EditorPaginaWebLogic oCentroEntrenamiento_EditorPaginaWebLogic = new CentroEntrenamiento_EditorPaginaWebLogic())
            {
                oResp = oCentroEntrenamiento_EditorPaginaWebLogic.CentroEntrenamiento_EditorPaginaWebGetItem(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new CentroEntrenamiento_EditorPaginaWebDTO();
                oItemViewModel = oResp.Item;
            }

            return oItemViewModel;
        }


        public int CentroEntrenamiento_uspActualizarEdicionPaginaWeb(CentroEntrenamiento_EditorPaginaWebDTO request)
        {
            int mensaje = 0;

            List<CentroEntrenamiento_EditorPaginaWebDTO> list = new List<CentroEntrenamiento_EditorPaginaWebDTO>();

            list.Add(new CentroEntrenamiento_EditorPaginaWebDTO()
            {
                CodigoPagina = request.CodigoPagina,
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                // logoPagina = request.logoPagina,
                ColorPrincipalPagina = request.ColorPrincipalPagina,
                BannerReserva_Titulo = request.BannerReserva_Titulo,
                BannerReserva_Descripcion = request.BannerReserva_Descripcion,
                BannerReserva_Descripcion2 = request.BannerReserva_Descripcion2,
                //BannerReserva_FondoImagen =  request.BannerReserva_FondoImagen,
                BannerReserva_Estado = request.BannerReserva_Estado,
                BannerCentro_Titulo = request.BannerCentro_Titulo,
                BannerCentro_Descripcion = request.BannerCentro_Descripcion,
                BannerCentro_Beneficio1 = request.BannerCentro_Beneficio1,
                BannerCentro_Beneficio2 = request.BannerCentro_Beneficio2,
                BannerCentro_Beneficio3 = request.BannerCentro_Beneficio3,
                //BannerCentro_FondoImagen = request.BannerCentro_FondoImagen,
                BannerCentro_Estado = request.BannerCentro_Estado,
                SesionFormulario_Titulo = request.SesionFormulario_Titulo,
                SesionFormulario_Descripcion = request.SesionFormulario_Descripcion,
                SesionFormulario_Tituloformulario = request.SesionFormulario_Tituloformulario,
                SesionFormulario_Whatsapp = request.SesionFormulario_Whatsapp,
                //SesionFormulario_Imagen   = request.SesionFormulario_Imagen,
                SesionFormulario_Estado = request.SesionFormulario_Estado,
                SesionTrainner_Titulo = request.SesionTrainner_Titulo,
                SesionTrainner_Descripcion = request.SesionTrainner_Descripcion,
                SesionTrainner_Estado = request.SesionTrainner_Estado,
                SesionServicio_Titulo = request.SesionServicio_Titulo,
                SesionServicio_Descripcion = request.SesionServicio_Descripcion,
                SesionServicio_Estado = request.SesionServicio_Estado,
                SesionPlan_Titulo = request.SesionPlan_Titulo,
                SesionPlan_Descripcion = request.SesionPlan_Descripcion,
                SesionPlan_Estado = request.SesionPlan_Estado,
                SesionVideo_Titulo = string.Empty,// request.SesionVideo_Titulo,
                SesionVideo_Descripcion = string.Empty,//request.SesionVideo_Descripcion,
                SesionVideo_Linkvideo = string.Empty,//request.SesionVideo_Linkvideo,
                SesionVideo_Estado = true,//request.SesionVideo_Estado,
                SesionFreepass_Titulo = request.SesionFreepass_Titulo,
                SesionFreepass_Descripcion = request.SesionFreepass_Descripcion,
                SesionFreepass_Estado = request.SesionFreepass_Estado,
                SesionInformacion_Direccion1 = request.SesionInformacion_Direccion1,
                SesionInformacion_Direccion2 = request.SesionInformacion_Direccion2,
                SesionInformacion_Contactanos1 = request.SesionInformacion_Contactanos1,
                SesionInformacion_Contactanos2 = request.SesionInformacion_Contactanos2,
                SesionInformacion_Horario1 = request.SesionInformacion_Horario1,
                SesionInformacion_Horario2 = request.SesionInformacion_Horario2,
                SesionInformacion_LatitudMapa = request.SesionInformacion_LatitudMapa,
                SesionInformacion_LongitudMapa = request.SesionInformacion_LongitudMapa,
                SesionInformacion_Estado = request.SesionInformacion_Estado,
                Operation = Operation.Update
            });

            ReqCentroEntrenamiento_EditorPaginaWebDTO oReq = new ReqCentroEntrenamiento_EditorPaginaWebDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespCentroEntrenamiento_EditorPaginaWebDTO oResp = null;
            using (CentroEntrenamiento_EditorPaginaWebLogic oCentroEntrenamiento_EditorPaginaWebLogic = new CentroEntrenamiento_EditorPaginaWebLogic())
            {
                oResp = oCentroEntrenamiento_EditorPaginaWebLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return mensaje;
        }

        public int CentroEntrenamiento_uspActualizarEdicionPaginaWeb_ColorPrincipalPagina(CentroEntrenamiento_EditorPaginaWebDTO request)
        {
            int mensaje = 0;

            List<CentroEntrenamiento_EditorPaginaWebDTO> list = new List<CentroEntrenamiento_EditorPaginaWebDTO>();

            list.Add(new CentroEntrenamiento_EditorPaginaWebDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                ColorPrincipalPagina = request.ColorPrincipalPagina,
                ReservasNormativa = request.ReservasNormativa,
                ReservasNotas = request.ReservasNotas,
                ReservasMinutosCancelar = request.ReservasMinutosCancelar,
                Operation = Operation.CentroEntrenamiento_uspActualizarEdicionPaginaWeb_ColorPrincipalPagina
            });

            ReqCentroEntrenamiento_EditorPaginaWebDTO oReq = new ReqCentroEntrenamiento_EditorPaginaWebDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespCentroEntrenamiento_EditorPaginaWebDTO oResp = null;
            using (CentroEntrenamiento_EditorPaginaWebLogic oCentroEntrenamiento_EditorPaginaWebLogic = new CentroEntrenamiento_EditorPaginaWebLogic())
            {
                oResp = oCentroEntrenamiento_EditorPaginaWebLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return mensaje;
        }

        public string CentroEntrenamiento_uspActualizarEdicionPaginaWeb_Foto(CentroEntrenamiento_EditorPaginaWebDTO request)
        {
            string mensaje = string.Empty;

            List<CentroEntrenamiento_EditorPaginaWebDTO> list = new List<CentroEntrenamiento_EditorPaginaWebDTO>();

            list.Add(new CentroEntrenamiento_EditorPaginaWebDTO()
            {
                CodigoPagina = request.CodigoPagina,
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                UrlImagen = request.UrlImagen,
                TipoFoto = request.TipoFoto,
                Operation = Operation.UpdateFoto
            });

            ReqCentroEntrenamiento_EditorPaginaWebDTO oReq = new ReqCentroEntrenamiento_EditorPaginaWebDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespCentroEntrenamiento_EditorPaginaWebDTO oResp = null;
            using (CentroEntrenamiento_EditorPaginaWebLogic oCentroEntrenamiento_EditorPaginaWebLogic = new CentroEntrenamiento_EditorPaginaWebLogic())
            {
                oResp = oCentroEntrenamiento_EditorPaginaWebLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "Excelente";
            }

            return mensaje;
        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}