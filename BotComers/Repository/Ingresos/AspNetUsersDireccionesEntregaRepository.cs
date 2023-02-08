using E_BusinessLayer;
using E_DataModel;
using E_DataModel.Common;
using System;
using System.Collections.Generic;


namespace BotComers.Repository.Ingresos
{
    public class AspNetUsersDireccionesEntregaRepository : IDisposable
    {
        public List<AspNetUsersDireccionesEntregaDTO> ecommerce_uspListarAspNetUsers_DireccionesEntrega(AspNetUsersDireccionesEntregaDTO request)
        {
            List<AspNetUsersDireccionesEntregaDTO> lista = new List<AspNetUsersDireccionesEntregaDTO>();

            ReqFilterAspNetUsersDireccionesEntregaDTO oReq = new ReqFilterAspNetUsersDireccionesEntregaDTO()
            {
                Item = new AspNetUsersDireccionesEntregaDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CorreoUsuario = request.CorreoUsuario
                },
                FilterCase = filterCaseAspNetUsersDireccionesEntrega.ecommerce_uspListarAspNetUsers_DireccionesEntrega,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListAspNetUsersDireccionesEntregaDTO oResp = null;

            using (AspNetUsersDireccionesEntregaLogic oAspNetUsersDireccionesEntregaLogic = new AspNetUsersDireccionesEntregaLogic())
            {
                oResp = oAspNetUsersDireccionesEntregaLogic.AspNetUsersDireccionesEntregaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }

        public string ecommerce_uspRegistrarAspNetUsersDireccionesEntrega(AspNetUsersDireccionesEntregaDTO oItem)
        {
            string CodigoDireccion = String.Empty;

            List<AspNetUsersDireccionesEntregaDTO> list = new List<AspNetUsersDireccionesEntregaDTO>();

            list.Add(new AspNetUsersDireccionesEntregaDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                CorreoUsuario = oItem.CorreoUsuario,
                Email = oItem.Email,
                IdUser = string.Empty,
                CodigoDireccion = string.Empty,
                Nombres = oItem.Nombres,
                Apellidos = oItem.Apellidos,
                Celular = oItem.Celular == null ? string.Empty : oItem.Celular,
                Telefono = oItem.Telefono == null ? string.Empty : oItem.Telefono,
                TipoDireccion = oItem.TipoDireccion,
                Direccion = oItem.Direccion == null ? string.Empty : oItem.Direccion,
                NroLote = oItem.NroLote == null ? string.Empty : oItem.NroLote,
                DeptoInt = oItem.DeptoInt == null ? string.Empty : oItem.DeptoInt,
                Urbanizacion = oItem.Urbanizacion == null ? string.Empty : oItem.Urbanizacion,
                Referencia = oItem.Referencia == null ? string.Empty : oItem.Referencia,
                Ubigeo = oItem.Ubigeo == null ? string.Empty : oItem.Ubigeo,
                DireccionDefault = oItem.DireccionDefault,
                Operation = oItem.Accion == "N" ? Operation.Create : Operation.Update,

            });

            ReqAspNetUsersDireccionesEntregaDTO oReq = new ReqAspNetUsersDireccionesEntregaDTO()
            {
                List = list,
                User = "admin"
            };
            RespAspNetUsersDireccionesEntregaDTO oResp = null;
            using (AspNetUsersDireccionesEntregaLogic oAspNetUsersDireccionesEntregaLogic = new AspNetUsersDireccionesEntregaLogic())
            {
                oResp = oAspNetUsersDireccionesEntregaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                CodigoDireccion = oResp.MessageList[0].Detalle;
            }

            return CodigoDireccion;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}