using BotComers.ViewModels.Inventario;
using E_BusinessLayer;
using E_DataModel;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.Inventario
{
    public class AlmacenesRepository : IDisposable
    {
        public List<AlmacenesViewModel> ecommerce_uspListarAlmacenes(int CodigoUnidadNegocio, int CodigoSede, int PageNumber)
        {
            List<AlmacenesViewModel> lista = null;

            ReqFilterAlmacenesDTO oReq = new ReqFilterAlmacenesDTO()
            {
                Item = new AlmacenesDTO()
                {
                    CodigoUnidadNegocio = CodigoUnidadNegocio,
                    CodigoSede = CodigoSede
                },
                FilterCase = filterCaseAlmacenes.ecommerce_uspListarAlmacenes_Paginacion,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListAlmacenesDTO oResp = null;

            using (AlmacenesLogic oAlmacenesLogic = new AlmacenesLogic())
            {
                oResp = oAlmacenesLogic.AlmacenesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<AlmacenesViewModel>();
                foreach (AlmacenesDTO item in oResp.List)
                {
                    lista.Add(new AlmacenesViewModel()
                    {
                        CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                        CodigoSede = item.CodigoSede,
                        CodigoAlmacen = item.CodigoAlmacen,
                        Descripcion = item.Descripcion,
                        Direccion = item.Direccion,
                        Observaciones = item.Observaciones,
                        UsuarioCreacion = item.UsuarioCreacion
                    });
                }
            }

            return lista;

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}