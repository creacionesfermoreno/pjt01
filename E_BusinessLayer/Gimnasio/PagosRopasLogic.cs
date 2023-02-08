
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
	//Archivo     : PagosRopasLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 25/01/2018
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class PagosRopasLogic: IDisposable
	{
		PagosRopasData oPagosRopasData = null;
		public PagosRopasLogic()
		{
			oPagosRopasData = new PagosRopasData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	PagosRopasGetList
		//Objetivo: Retorna una colección de registros de tipo PagosRopasDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListPagosRopasDTO PagosRopasGetList(ReqFilterPagosRopasDTO oReqFilterPagosRopasDTO)
		{
		
			RespListPagosRopasDTO oRespListPagosRopasDTO = new RespListPagosRopasDTO();
		
			oRespListPagosRopasDTO.List = new List<PagosRopasDTO>();
			oRespListPagosRopasDTO.User = oReqFilterPagosRopasDTO.User;
			oRespListPagosRopasDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterPagosRopasDTO.User))
            {
                oRespListPagosRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PagosRopas no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterPagosRopasDTO.Paging == null)
            {
                oRespListPagosRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListPagosRopasDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterPagosRopasDTO.Paging.All && oReqFilterPagosRopasDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterPagosRopasDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<PagosRopasDTO> PagosRopasDTOList = new List<PagosRopasDTO>();

                    switch (oReqFilterPagosRopasDTO.FilterCase)
                    {


                        case filterCasePagosRopas.uspListarPagosRopasPorCodigoSalida:
                            {
                                PagosRopasDTOList = oPagosRopasData.uspListarPagosRopasPorCodigoSalida(oReqFilterPagosRopasDTO.Item);
                            }
                            break;
                        case filterCasePagosRopas.uspListarDeudasRopasTotalesDiaRangoFechas_Paginacion:
                            
                            if (!oReqFilterPagosRopasDTO.Paging.All && oReqFilterPagosRopasDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterPagosRopasDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarDeudasRopasTotalesDiaRangoFechas_Paginacion"]);
                            }
                            PagosRopasDTOList = oPagosRopasData.uspListarDeudasRopasTotalesDiaRangoFechas_Paginacion(oReqFilterPagosRopasDTO.Item, oReqFilterPagosRopasDTO.Paging);
                            break;
                        default:
                            {
                               // PagosRopasDTOList = oPagosRopasData.Listar(oReqFilterPagosRopasDTO.Item, oReqFilterPagosRopasDTO.Paging, ref recordCount);
                            }
                            break;
                    }

                    oRespListPagosRopasDTO.List = PagosRopasDTOList;
                    oRespListPagosRopasDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListPagosRopasDTO.Success = false;
                    oRespListPagosRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListPagosRopasDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	PagosRopasGetItem
		//Objetivo: Retorna un registro de tipo PagosRopasDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemPagosRopasDTO PagosRopasGetItem(ReqFilterPagosRopasDTO oReqFilterPagosRopasDTO)
		{
			RespItemPagosRopasDTO oRespItemPagosRopasDTO = new RespItemPagosRopasDTO();

            oRespItemPagosRopasDTO.Success = false;
            oRespItemPagosRopasDTO.Item = null;
            oRespItemPagosRopasDTO.User = oReqFilterPagosRopasDTO.User;
            oRespItemPagosRopasDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterPagosRopasDTO.User))
            {
                oRespItemPagosRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PagosRopas no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemPagosRopasDTO.MessageList.Count == 0)
            {
                PagosRopasDTO oPagosRopasDTO = null;
                try
                {
                    switch (oReqFilterPagosRopasDTO.FilterCase)
                    {

                        case filterCasePagosRopas.uspListarDeudasRopasTotalesDiaRangoFechas_NumeroRegistros:
                            {
                                oPagosRopasDTO = new PagosRopasDTO();
                                oPagosRopasDTO = oPagosRopasData.uspListarDeudasRopasTotalesDiaRangoFechas_NumeroRegistros(oReqFilterPagosRopasDTO.Item);
                            }
                            break;
                        case filterCasePagosRopas.PorCodigo:
                            {
                                oPagosRopasDTO = new PagosRopasDTO();
                               // oPagosRopasDTO = oPagosRopasData.BuscarPorCodigoPagosRopas(oReqFilterPagosRopasDTO.Item);
                            }
                            break;
                        default:
                            {
                                oPagosRopasDTO = new PagosRopasDTO();
                            }
                            break;
                    }

                    oRespItemPagosRopasDTO.Item = new PagosRopasDTO();
                    oRespItemPagosRopasDTO.Item = oPagosRopasDTO;
                    oRespItemPagosRopasDTO.Success = true;
                    oRespItemPagosRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemPagosRopasDTO.Success = false;
                    oRespItemPagosRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemPagosRopasDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo PagosRopasDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespPagosRopasDTO ExecuteTransac(ReqPagosRopasDTO oReqPagosRopasDTO)
		{
			RespPagosRopasDTO oRespPagosRopasDTO = new RespPagosRopasDTO();

            oRespPagosRopasDTO.MessageList = new List<Mensaje>();
            oRespPagosRopasDTO.User = oReqPagosRopasDTO.User;
            
            if (String.IsNullOrEmpty(oReqPagosRopasDTO.User))
            {
                oRespPagosRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PagosRopas no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespPagosRopasDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int codigo = 0;
                        int CodigoValidacionOperaciones = 999999999;
                        foreach (PagosRopasDTO item in oReqPagosRopasDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    UsuariosIngresosData oUsuariosIngresosDataCreate = new UsuariosIngresosData();
                                    UsuariosIngresosDTO oUsuariosIngresosDTOCreate = new UsuariosIngresosDTO();

                                    oUsuariosIngresosDTOCreate.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    oUsuariosIngresosDTOCreate.CodigoSede = item.CodigoSede;
                                    oUsuariosIngresosDTOCreate.UsuarioCreacion = item.User;
                                    oUsuariosIngresosDTOCreate.CodigoIngreso = item.TK_ID;
                                    oUsuariosIngresosDTOCreate.Latitud = item.TK_Latitude;
                                    oUsuariosIngresosDTOCreate.Longitud = item.TK_Longitude;

                                    item.CodigoInicioSesion = item.TK_ID;

                                    oUsuariosIngresosDTOCreate = oUsuariosIngresosDataCreate.uspValidarAccesoSistema(oUsuariosIngresosDTOCreate);

                                    if (oUsuariosIngresosDTOCreate.CodigoValidacion == 3)
                                    {

                                        UsuariosIngresosData oUsuariosIngresosDataValidacion = new UsuariosIngresosData();
                                        UsuariosIngresosDTO oUsuariosIngresosDTOValidacion = new UsuariosIngresosDTO();
                                        oUsuariosIngresosDTOValidacion.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                        oUsuariosIngresosDTOValidacion.CodigoSede = item.CodigoSede;
                                        oUsuariosIngresosDTOValidacion.UsuarioCreacion = item.User;
                                        oUsuariosIngresosDTOValidacion.CodigoInicioSesion = item.CodigoInicioSesion;
                                        oUsuariosIngresosDTOValidacion.Operacion = "I";
                                        oUsuariosIngresosDTOValidacion.DescripcionTabla = "PagosRopas";

                                        CodigoValidacionOperaciones = oUsuariosIngresosDataValidacion.uspObtenerValidacionOperaciones(oUsuariosIngresosDTOValidacion);

                                        if (CodigoValidacionOperaciones == 0)
                                        {

                                            ConfiguracionData oConfiguracionData = new ConfiguracionData();
                                            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
                                            oConfiguracionDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            oConfiguracionDTO.Codigo = item.CodigoSede;
                                            oConfiguracionDTO = oConfiguracionData.BuscarPorCodigoConfiguracion(oConfiguracionDTO);

                                            if (oConfiguracionDTO.GenerarSerie)
                                            {
                                                SeriesData oSeriesData = new SeriesData();
                                                SeriesDTO oSeriesDTO = new SeriesDTO();
                                                oSeriesDTO.TipoDocumento = item.CodigoTipoComprobante;
                                                oSeriesDTO.SubTipoDocumento = item.CodigoSubTipoComprobante;
                                                oSeriesDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oSeriesDTO.CodigoSede = item.CodigoSede;
                                                oSeriesData.ActualizarSerieAumentar(oSeriesDTO);
                                            }

                                            codigo = 999999999;
                                            oPagosRopasData.Registrar(item);
                                        }

                                    }
                                    else
                                    {
                                        codigo = 0;

                                    }
                                    break;
                                case Operation.Update:
                                    //oPagosRopasData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                  //  oPagosRopasData.Eliminar(item);
                                    break;
                                case Operation.uspActualizarPagoRopasEstado:

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
                                        codigo = 999999999;
                                        oPagosRopasData.uspActualizarPagoRopasEstado(item);
                                    }
                                    else
                                    {
                                        codigo = 0;
                                    }

                                    break;


                            }
                        }
                        tx.Complete();
                        oRespPagosRopasDTO.Success = true;
                        oRespPagosRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespPagosRopasDTO.Success = false;
                        oRespPagosRopasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespPagosRopasDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
