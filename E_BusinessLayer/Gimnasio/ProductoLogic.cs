using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer.Gimnasio;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.IO;

namespace E_BusinessLayer.Gimnasio
{
	//-------------------------------------------------------------------
	//Archivo     : ProductoLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 20/08/2014
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class ProductoLogic: IDisposable
	{
		ProductoData oProductoData = null;
		public ProductoLogic()
		{
			oProductoData = new ProductoData();
		}
        string ruta = @"D:\Licencia.txt";
		//-------------------------------------------------------------------
		//Nombre:	ProductoGetList
		//Objetivo: Retorna una colección de registros de tipo ProductoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListProductoDTO ProductoGetList(ReqFilterProductoDTO oReqFilterProductoDTO)
		{
		
			RespListProductoDTO oRespListProductoDTO = new RespListProductoDTO();
		
			oRespListProductoDTO.List = new List<ProductoDTO>();
			oRespListProductoDTO.User = oReqFilterProductoDTO.User;
			oRespListProductoDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterProductoDTO.User))
            {
                oRespListProductoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Producto no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterProductoDTO.Paging == null)
            {
                oRespListProductoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }


			if (oRespListProductoDTO.MessageList.Count == 0)
            {
                
                try
                {
                    List<ProductoDTO> ProductoDTOList = new List<ProductoDTO>();
                    switch (oReqFilterProductoDTO.FilterCase)
                    {
                        case filterCaseProducto.uspListarProductoBuscadorPorNombre:
                            {
                                ProductoDTOList = oProductoData.uspListarProductoBuscadorPorNombre(oReqFilterProductoDTO.Item);
                            }
                            break;
                        case filterCaseProducto.uspListarDeudasSuplementoRopaDelSocio:
                            {
                                ProductoDTOList = oProductoData.uspListarDeudasSuplementoRopaDelSocio(oReqFilterProductoDTO.Item);
                            }
                            break;
                        case filterCaseProducto.uspListarProductoPorCategoriaCompraFiltro:
                            {
                                ProductoDTOList = oProductoData.uspListarProductoPorCategoriaCompraFiltro(oReqFilterProductoDTO.Item);
                            }
                            break;
                        case filterCaseProducto.ListaPorNombre:
                            {
                                ProductoDTOList = oProductoData.ListarPorNombre(oReqFilterProductoDTO.Item);
                            }
                            break;
                        case filterCaseProducto.ListarTodo:
                            {
                                ProductoDTOList = oProductoData.ListarTodo(oReqFilterProductoDTO.Item);
                            }
                            break;
                        case filterCaseProducto.uspListarProductosPorFiltro_Paginacion:
                            {
                                if (!oReqFilterProductoDTO.Paging.All && oReqFilterProductoDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterProductoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarProductosPorFiltros_NumeroRegistros"]);
                                }
                                ProductoDTOList = oProductoData.uspListarProductosPorFiltro_Paginacion(oReqFilterProductoDTO.Item, oReqFilterProductoDTO.Paging);
                            }
                            break;
                        case filterCaseProducto.uspListarHistorialCompraProductos_Paginacion:
                            {
                                if(!oReqFilterProductoDTO.Paging.All && oReqFilterProductoDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterProductoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarProductosPorFiltros_NumeroRegistros"]);
                                }
                                ProductoDTOList = oProductoData.uspListarHistorialCompraProductos_Paginacion(oReqFilterProductoDTO.Item, oReqFilterProductoDTO.Paging);
                            }
                            break;
                        case filterCaseProducto.uspListarProductoPorCategoria:
                            {
                                ProductoDTOList = oProductoData.uspListarProductoPorCategoria(oReqFilterProductoDTO.Item);
                            }
                            break;
                        case filterCaseProducto.uspListarProductoPorCategoriaCompra:
                            {
                                ProductoDTOList = oProductoData.uspListarProductoPorCategoriaCompra(oReqFilterProductoDTO.Item);
                            }
                            break;
                        case filterCaseProducto.uspListarProductoPorCategoriaVenta:
                            {
                                ProductoDTOList = oProductoData.uspListarProductoPorCategoriaVenta(oReqFilterProductoDTO.Item);
                            }
                            break;
                        case filterCaseProducto.uspListarKardexProductos_Paginacion:
                            {
                                ProductoDTOList = oProductoData.uspListarKardexProductos_Paginacion(oReqFilterProductoDTO.Item);
                            }
                            break;
                        case filterCaseProducto.uspListarKardexSuplementos_Paginacion:
                            {
                                ProductoDTOList = oProductoData.uspListarKardexSuplementos_Paginacion(oReqFilterProductoDTO.Item);
                            }
                            break;
                        case filterCaseProducto.uspListarKardexRopas_Paginacion:
                            {
                                ProductoDTOList = oProductoData.uspListarKardexRopas_Paginacion(oReqFilterProductoDTO.Item);
                            }
                            break;
                        case filterCaseProducto.uspListarHistorialCompraSuplementos_Paginacion:
                            {
                                if(!oReqFilterProductoDTO.Paging.All && oReqFilterProductoDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterProductoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarProductosPorFiltros_NumeroRegistros"]);
                                }
                                ProductoDTOList = oProductoData.uspListarHistorialCompraSuplementos_Paginacion(oReqFilterProductoDTO.Item,oReqFilterProductoDTO.Paging);
                            }
                            break;
                        case filterCaseProducto.uspListarHistorialCompraRopas_Paginacion:
                            {
                                if (!oReqFilterProductoDTO.Paging.All && oReqFilterProductoDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterProductoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarProductosPorFiltros_NumeroRegistros"]);
                                }
                                ProductoDTOList = oProductoData.uspListarHistorialCompraRopas_Paginacion(oReqFilterProductoDTO.Item, oReqFilterProductoDTO.Paging);
                            }
                            break;
                        default:
                            {
                                ProductoDTOList = oProductoData.Listar(oReqFilterProductoDTO.Item);
                            }
                            break;
                    }

                    oRespListProductoDTO.List = ProductoDTOList;
                    oRespListProductoDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListProductoDTO.Success = false;
                    oRespListProductoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListProductoDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ProductoGetItem
		//Objetivo: Retorna un registro de tipo ProductoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemProductoDTO ProductoGetItem(ReqFilterProductoDTO oReqFilterProductoDTO)
		{
			RespItemProductoDTO oRespItemProductoDTO = new RespItemProductoDTO();

            oRespItemProductoDTO.Success = false;
            oRespItemProductoDTO.Item = null;
            oRespItemProductoDTO.User = oReqFilterProductoDTO.User;
            oRespItemProductoDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterProductoDTO.User))
            {
                oRespItemProductoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Producto no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemProductoDTO.MessageList.Count == 0)
            {
                ProductoDTO oProductoDTO = null;
                try
                {
                    switch (oReqFilterProductoDTO.FilterCase)
                    {
                       
                        case filterCaseProducto.BuscarPorCodigo:
                            {
                                oProductoDTO = new ProductoDTO();
                                oProductoDTO = oProductoData.BuscarPorCodigo(oReqFilterProductoDTO.Item);
                            }
                            break;
                        case filterCaseProducto.uspListarHistorialCompraProductos_NumeroRegistros:
                            {
                                oProductoDTO = new ProductoDTO();
                                oProductoDTO = oProductoData.uspListarHistorialCompraProductos_NumeroRegistros(oReqFilterProductoDTO.Item);
                            }
                            break;
                        case filterCaseProducto.uspListarProductosPorFiltro_NumeroRegistros:
                            {
                                oProductoDTO = new ProductoDTO();
                                oProductoDTO = oProductoData.uspListarProductosPorFiltro_NumeroRegistros(oReqFilterProductoDTO.Item);
                            }
                            break;
                        case filterCaseProducto.uspListarHistorialCompraSuplementos_NumeroRegistros:
                            {
                                oProductoDTO = new ProductoDTO();
                                oProductoDTO = oProductoData.uspListarHistorialCompraSuplementos_NumeroRegistros(oReqFilterProductoDTO.Item);
                            }
                            break;
                        case filterCaseProducto.uspListarHistorialCompraRopas_NumeroRegistros:
                            {
                                oProductoDTO = new ProductoDTO();
                                oProductoDTO = oProductoData.uspListarHistorialCompraRopas_NumeroRegistros(oReqFilterProductoDTO.Item);
                            }
                            break;
                        default:
                            {
                                oProductoDTO = new ProductoDTO();
                            }
                            break;
                    }

                    oRespItemProductoDTO.Item = new ProductoDTO();
                    oRespItemProductoDTO.Item = oProductoDTO;
                    oRespItemProductoDTO.Success = true;
                    oRespItemProductoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemProductoDTO.Success = false;
                    oRespItemProductoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemProductoDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo ProductoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespProductoDTO ExecuteTransac(ReqProductoDTO oReqProductoDTO)
		{
			RespProductoDTO oRespProductoDTO = new RespProductoDTO();

            oRespProductoDTO.MessageList = new List<Mensaje>();
            oRespProductoDTO.User = oReqProductoDTO.User;
            
            if (String.IsNullOrEmpty(oReqProductoDTO.User))
            {
                oRespProductoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Producto no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespProductoDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int Codigo = 0;
                        foreach (ProductoDTO item in oReqProductoDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    UsuariosIngresosData oUsuariosIngresosDataCreate = new UsuariosIngresosData();
                                    UsuariosIngresosDTO oUsuariosIngresosDTOCreate = new UsuariosIngresosDTO();

                                    oUsuariosIngresosDTOCreate.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    oUsuariosIngresosDTOCreate.CodigoSede = item.CodigoSede;
                                    oUsuariosIngresosDTOCreate.UsuarioCreacion = item.UsuarioCreacion;
                                    oUsuariosIngresosDTOCreate.CodigoIngreso = item.TK_ID;
                                    oUsuariosIngresosDTOCreate.Latitud = item.TK_Latitude;
                                    oUsuariosIngresosDTOCreate.Longitud = item.TK_Longitude;

                                    item.CodigoInicioSesion = item.TK_ID;

                                    oUsuariosIngresosDTOCreate = oUsuariosIngresosDataCreate.uspValidarAccesoSistema(oUsuariosIngresosDTOCreate);
                                    if (oUsuariosIngresosDTOCreate.CodigoValidacion == 3)
                                    {
                                        Codigo = 999999999;
                                        oProductoData.Registrar(item);
                                    }
                                    else
                                    {
                                        Codigo = 0;
                                    }

                                    break;
                                case Operation.Update:
                                    UsuariosIngresosData oUsuariosIngresosDataUpdate = new UsuariosIngresosData();
                                    UsuariosIngresosDTO oUsuariosIngresosDTOUpdate = new UsuariosIngresosDTO();

                                    oUsuariosIngresosDTOUpdate.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    oUsuariosIngresosDTOUpdate.CodigoSede = item.CodigoSede;
                                    oUsuariosIngresosDTOUpdate.UsuarioCreacion = item.UsuarioCreacion;
                                    oUsuariosIngresosDTOUpdate.CodigoIngreso = item.TK_ID;
                                    oUsuariosIngresosDTOUpdate.Latitud = item.TK_Latitude;
                                    oUsuariosIngresosDTOUpdate.Longitud = item.TK_Longitude;

                                    item.CodigoInicioSesion = item.TK_ID;

                                    oUsuariosIngresosDTOUpdate = oUsuariosIngresosDataUpdate.uspValidarAccesoSistema(oUsuariosIngresosDTOUpdate);
                                    if (oUsuariosIngresosDTOUpdate.CodigoValidacion == 3)
                                    {
                                        Codigo = 999999999;
                                        oProductoData.Actualizar(item);
                                    }
                                    else
                                    {
                                        Codigo = 0;
                                    }

                                    break;
                                case Operation.Delete:
                                    UsuariosIngresosData oUsuariosIngresosDataDelete = new UsuariosIngresosData();
                                    UsuariosIngresosDTO oUsuariosIngresosDTODelete = new UsuariosIngresosDTO();

                                    oUsuariosIngresosDTODelete.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    oUsuariosIngresosDTODelete.CodigoSede = item.CodigoSede;
                                    oUsuariosIngresosDTODelete.UsuarioCreacion = item.UsuarioCreacion;
                                    oUsuariosIngresosDTODelete.CodigoIngreso = item.TK_ID;
                                    oUsuariosIngresosDTODelete.Latitud = item.TK_Latitude;
                                    oUsuariosIngresosDTODelete.Longitud = item.TK_Longitude;

                                    item.CodigoInicioSesion = item.TK_ID;

                                    oUsuariosIngresosDTODelete = oUsuariosIngresosDataDelete.uspValidarAccesoSistema(oUsuariosIngresosDTODelete);
                                    if (oUsuariosIngresosDTODelete.CodigoValidacion == 3)
                                    {
                                        Codigo = 999999999;
                                        oProductoData.Eliminar(item);
                                    }
                                    else
                                    {
                                        Codigo = 0;
                                    }

                                    break;
                            }
                        }
                        tx.Complete();
                        oRespProductoDTO.Success = true;
                        oRespProductoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = Codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespProductoDTO.Success = false;
                        oRespProductoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespProductoDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
