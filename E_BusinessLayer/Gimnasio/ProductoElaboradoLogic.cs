
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer.Gimnasio;
using E_DataModel.Gimnasio;
using E_DataModel.Common;

namespace E_BusinessLayer.Gimnasio
{
    //-------------------------------------------------------------------
    //Archivo     : ProductoElaboradoLogic.cs
    //Proyecto    : (NOMBRE DEL PROYECTO)
    //Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
    //Fecha       : 25/09/2015
    //Descripcion : Clase para capa de negocio
    //-------------------------------------------------------------------
    public class ProductoElaboradoLogic : IDisposable
    {
        ProductoElaboradoData oProductoElaboradoData = null;
        public ProductoElaboradoLogic()
        {
            oProductoElaboradoData = new ProductoElaboradoData();
        }

        //-------------------------------------------------------------------
        //Nombre:	ProductoElaboradoGetList
        //Objetivo: Retorna una colección de registros de tipo ProductoElaboradoDTO
        //Valores Prueba:
        //Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //-------------------------------------------------------------------
        public RespListProductoElaboradoDTO ProductoElaboradoGetList(ReqFilterProductoElaboradoDTO oReqFilterProductoElaboradoDTO)
        {
            RespListProductoElaboradoDTO oRespListProductoElaboradoDTO = new RespListProductoElaboradoDTO();
            oRespListProductoElaboradoDTO.List = new List<ProductoElaboradoDTO>();
            oRespListProductoElaboradoDTO.User = oReqFilterProductoElaboradoDTO.User;
            oRespListProductoElaboradoDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterProductoElaboradoDTO.User))
            {
                oRespListProductoElaboradoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ProductoElaborado no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oReqFilterProductoElaboradoDTO.Paging == null)
            {
                oRespListProductoElaboradoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespListProductoElaboradoDTO.MessageList.Count == 0)
            {
                try
                {
                    
                    //if (!oReqFilterProductoElaboradoDTO.Paging.All && oReqFilterProductoElaboradoDTO.Paging.PageRecords == 0)
                    //{
                    //    oReqFilterProductoElaboradoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    //}

                    List<ProductoElaboradoDTO> ProductoElaboradoDTOList = new List<ProductoElaboradoDTO>();

                    switch (oReqFilterProductoElaboradoDTO.FilterCase)
                    {
                        case filterCaseProductoElaborado.ListaPorNombre:
                            {
                                ProductoElaboradoDTOList = oProductoElaboradoData.Listar(oReqFilterProductoElaboradoDTO.Item);
                            }
                            break;
                        case filterCaseProductoElaborado.uspListarProductoElaboradoPorFiltro_Paginacion:
                            {
                                if (!oReqFilterProductoElaboradoDTO.Paging.All && oReqFilterProductoElaboradoDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterProductoElaboradoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarProductoElaboradoPorFiltro_NumeroRegistros"]);
                                    ProductoElaboradoDTOList = oProductoElaboradoData.uspListarProductoElaboradoPorFiltro_Paginacion(oReqFilterProductoElaboradoDTO.Item, oReqFilterProductoElaboradoDTO.Paging);
                                }
                               
                            }
                            break;

                        case filterCaseProductoElaborado.uspListarDiario:
                            {
                                ProductoElaboradoDTOList = oProductoElaboradoData.uspListarDiario(oReqFilterProductoElaboradoDTO.Item);
                            }
                            break;

                        default:
                            {
                                ProductoElaboradoDTOList = new List<ProductoElaboradoDTO>();
                            }
                            break;
                    }

                    oRespListProductoElaboradoDTO.List = ProductoElaboradoDTOList;
                    oRespListProductoElaboradoDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListProductoElaboradoDTO.Success = false;
                    oRespListProductoElaboradoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }

            return oRespListProductoElaboradoDTO;

        }

        //-------------------------------------------------------------------
        //Nombre:	ProductoElaboradoGetItem
        //Objetivo: Retorna un registro de tipo ProductoElaboradoDTO
        //Valores Prueba:
        //Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //-------------------------------------------------------------------
        public RespItemProductoElaboradoDTO ProductoElaboradoGetItem(ReqFilterProductoElaboradoDTO oReqFilterProductoElaboradoDTO)
        {
            RespItemProductoElaboradoDTO oRespItemProductoElaboradoDTO = new RespItemProductoElaboradoDTO();

            oRespItemProductoElaboradoDTO.Success = false;
            oRespItemProductoElaboradoDTO.Item = null;
            oRespItemProductoElaboradoDTO.User = oReqFilterProductoElaboradoDTO.User;
            oRespItemProductoElaboradoDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterProductoElaboradoDTO.User))
            {
                oRespItemProductoElaboradoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ProductoElaborado no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemProductoElaboradoDTO.MessageList.Count == 0)
            {
                ProductoElaboradoDTO oProductoElaboradoDTO = null;
                try
                {
                    switch (oReqFilterProductoElaboradoDTO.FilterCase)
                    {                       
                        case filterCaseProductoElaborado.BuscarPorCodigo:
                            {
                                oProductoElaboradoDTO = new ProductoElaboradoDTO();
                                oProductoElaboradoDTO = oProductoElaboradoData.BuscarProductoElaboradoPorCodigo(oReqFilterProductoElaboradoDTO.Item);
                            }
                            break;
                        case filterCaseProductoElaborado.uspListarProductoElaboradoPorFiltro_NumeroRegistros:
                            {
                                oProductoElaboradoDTO = new ProductoElaboradoDTO();
                                oProductoElaboradoDTO = oProductoElaboradoData.uspListarProductoElaboradoPorFiltro_NumeroRegistros(oReqFilterProductoElaboradoDTO.Item);
                            }
                            break;
                        default:
                            {
                                oProductoElaboradoDTO = new ProductoElaboradoDTO();
                            }
                            break;
                    }

                    oRespItemProductoElaboradoDTO.Item = new ProductoElaboradoDTO();
                    oRespItemProductoElaboradoDTO.Item = oProductoElaboradoDTO;
                    oRespItemProductoElaboradoDTO.Success = true;
                    oRespItemProductoElaboradoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemProductoElaboradoDTO.Success = false;
                    oRespItemProductoElaboradoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemProductoElaboradoDTO;
        }

        //-------------------------------------------------------------------
        //Nombre:	ExecuteTransac
        //Objetivo: Almacena el registro de un objeto de tipo ProductoElaboradoDTO
        //Valores Prueba:
        //Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //-------------------------------------------------------------------
        public RespProductoElaboradoDTO ExecuteTransac(ReqProductoElaboradoDTO oReqProductoElaboradoDTO)
        {
            RespProductoElaboradoDTO oRespProductoElaboradoDTO = new RespProductoElaboradoDTO();

            oRespProductoElaboradoDTO.MessageList = new List<Mensaje>();
            oRespProductoElaboradoDTO.User = oReqProductoElaboradoDTO.User;

            if (String.IsNullOrEmpty(oReqProductoElaboradoDTO.User))
            {
                oRespProductoElaboradoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ProductoElaborado no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespProductoElaboradoDTO.MessageList.Count == 0)
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int Codigo = 0;
                        foreach (ProductoElaboradoDTO item in oReqProductoElaboradoDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    Codigo = 999999999;
                                    oProductoElaboradoData.Registrar(item);

                                    break;
                                case Operation.Update:
                                    Codigo = 999999999;
                                    oProductoElaboradoData.Actualizar(item);


                                    break;
                                case Operation.Delete:
                                    Codigo = 999999999;
                                    oProductoElaboradoData.Eliminar(item);

                                    break;
                            }
                        }
                        tx.Complete();
                        oRespProductoElaboradoDTO.Success = true;
                        oRespProductoElaboradoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = Codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });

                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespProductoElaboradoDTO.Success = false;
                        oRespProductoElaboradoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespProductoElaboradoDTO;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
