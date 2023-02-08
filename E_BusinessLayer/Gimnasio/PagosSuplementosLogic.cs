
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
	//Archivo     : PagosSuplementosLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 22/08/2017
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class PagosSuplementosLogic: IDisposable
	{
		PagosSuplementosData oPagosSuplementosData = null;
		public PagosSuplementosLogic()
		{
			oPagosSuplementosData = new PagosSuplementosData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	PagosSuplementosGetList
		//Objetivo: Retorna una colección de registros de tipo PagosSuplementosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListPagosSuplementosDTO PagosSuplementosGetList(ReqFilterPagosSuplementosDTO oReqFilterPagosSuplementosDTO)
		{
		
			RespListPagosSuplementosDTO oRespListPagosSuplementosDTO = new RespListPagosSuplementosDTO();
		
			oRespListPagosSuplementosDTO.List = new List<PagosSuplementosDTO>();
			oRespListPagosSuplementosDTO.User = oReqFilterPagosSuplementosDTO.User;
			oRespListPagosSuplementosDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterPagosSuplementosDTO.User))
            {
                oRespListPagosSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PagosSuplementos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterPagosSuplementosDTO.Paging == null)
            {
                oRespListPagosSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListPagosSuplementosDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    //if (!oReqFilterPagosSuplementosDTO.Paging.All && oReqFilterPagosSuplementosDTO.Paging.PageRecords == 0)
                    //{
                    //    oReqFilterPagosSuplementosDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    //}

                    List<PagosSuplementosDTO> PagosSuplementosDTOList = new List<PagosSuplementosDTO>();

                    switch (oReqFilterPagosSuplementosDTO.FilterCase)
                    {
                        case filterCasePagosSuplementos.uspListarPagosSuplementosPorCodigoSalida:
                            {
                                PagosSuplementosDTOList = oPagosSuplementosData.uspListarPagosSuplementosPorCodigoSalida(oReqFilterPagosSuplementosDTO.Item);
                            }
                            break;
                        case filterCasePagosSuplementos.uspListarDeudasSuplementosTotalesDiaRangoFechas_Paginacion:
                            
                            if (!oReqFilterPagosSuplementosDTO.Paging.All && oReqFilterPagosSuplementosDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterPagosSuplementosDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarDeudasSuplementosTotalesDiaRangoFechas_Paginacion"]);
                            }
                              PagosSuplementosDTOList = oPagosSuplementosData.uspListarDeudasSuplementosTotalesDiaRangoFechas_Paginacion(oReqFilterPagosSuplementosDTO.Item, oReqFilterPagosSuplementosDTO.Paging);
                            break;
                        case filterCasePagosSuplementos.uspListarACuentaSuplementosPorFechaRangoFechas_Paginacion:
                            
                            if (!oReqFilterPagosSuplementosDTO.Paging.All && oReqFilterPagosSuplementosDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterPagosSuplementosDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarACuentaSuplementosPorFechaRangoFechas_Paginacion"]);
                            }
                              PagosSuplementosDTOList = oPagosSuplementosData.uspListarACuentaSuplementosPorFechaRangoFechas_Paginacion(oReqFilterPagosSuplementosDTO.Item, oReqFilterPagosSuplementosDTO.Paging);
                            break;

                        default:
                            {
                                //PagosSuplementosDTOList = oPagosSuplementosData.Listar(oReqFilterPagosSuplementosDTO.Item, oReqFilterPagosSuplementosDTO.Paging, ref recordCount);
                            }
                            break;
                    }

                    oRespListPagosSuplementosDTO.List = PagosSuplementosDTOList;
                    oRespListPagosSuplementosDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListPagosSuplementosDTO.Success = false;
                    oRespListPagosSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListPagosSuplementosDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	PagosSuplementosGetItem
		//Objetivo: Retorna un registro de tipo PagosSuplementosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemPagosSuplementosDTO PagosSuplementosGetItem(ReqFilterPagosSuplementosDTO oReqFilterPagosSuplementosDTO)
		{
			RespItemPagosSuplementosDTO oRespItemPagosSuplementosDTO = new RespItemPagosSuplementosDTO();

            oRespItemPagosSuplementosDTO.Success = false;
            oRespItemPagosSuplementosDTO.Item = null;
            oRespItemPagosSuplementosDTO.User = oReqFilterPagosSuplementosDTO.User;
            oRespItemPagosSuplementosDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterPagosSuplementosDTO.User))
            {
                oRespItemPagosSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PagosSuplementos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemPagosSuplementosDTO.MessageList.Count == 0)
            {
                PagosSuplementosDTO oPagosSuplementosDTO = null;
                try
                {
                    switch (oReqFilterPagosSuplementosDTO.FilterCase)
                    {
                       
                        case filterCasePagosSuplementos.uspListarDeudasSuplementosTotalesDiaRangoFechas_NumeroRegistros:
                            {
                                oPagosSuplementosDTO = new PagosSuplementosDTO();
                                oPagosSuplementosDTO = oPagosSuplementosData.uspListarDeudasSuplementosTotalesDiaRangoFechas_NumeroRegistros(oReqFilterPagosSuplementosDTO.Item);
                            }
                            break;

                        case filterCasePagosSuplementos.uspListarACuentaSuplementosPorFechaRangoFechas_NumeroRegistros:
                            {
                                oPagosSuplementosDTO = new PagosSuplementosDTO();
                                oPagosSuplementosDTO = oPagosSuplementosData.uspListarACuentaSuplementosPorFechaRangoFechas_NumeroRegistros(oReqFilterPagosSuplementosDTO.Item);
                            }
                            break;
                        case filterCasePagosSuplementos.porCodigo:
                            {
                                oPagosSuplementosDTO = new PagosSuplementosDTO();
                                //oPagosSuplementosDTO = oPagosSuplementosData.BuscarPorCodigoPagosSuplementos(oReqFilterPagosSuplementosDTO.Item);
                            }
                            break;
                        default:
                            {
                                oPagosSuplementosDTO = new PagosSuplementosDTO();
                            }
                            break;
                    }

                    oRespItemPagosSuplementosDTO.Item = new PagosSuplementosDTO();
                    oRespItemPagosSuplementosDTO.Item = oPagosSuplementosDTO;
                    oRespItemPagosSuplementosDTO.Success = true;
                    oRespItemPagosSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemPagosSuplementosDTO.Success = false;
                    oRespItemPagosSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemPagosSuplementosDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo PagosSuplementosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespPagosSuplementosDTO ExecuteTransac(ReqPagosSuplementosDTO oReqPagosSuplementosDTO)
		{
			RespPagosSuplementosDTO oRespPagosSuplementosDTO = new RespPagosSuplementosDTO();

            oRespPagosSuplementosDTO.MessageList = new List<Mensaje>();
            oRespPagosSuplementosDTO.User = oReqPagosSuplementosDTO.User;
            
            if (String.IsNullOrEmpty(oReqPagosSuplementosDTO.User))
            {
                oRespPagosSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PagosSuplementos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespPagosSuplementosDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int codigo = 0;
                        int CodigoValidacionOperaciones = 999999999;
                        foreach (PagosSuplementosDTO item in oReqPagosSuplementosDTO.List)
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
                                        oUsuariosIngresosDTOValidacion.DescripcionTabla = "PagosSuplementos";

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
                                            oPagosSuplementosData.Registrar(item);
                                        }

                                    }
                                    else
                                    {
                                        codigo = 0;

                                    }
                                    break;
                                case Operation.Update:
                                  //  oPagosSuplementosData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                   // oPagosSuplementosData.Eliminar(item);
                                    break;
                               case Operation.uspActualizarPagoSuplementosEstado:

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
                                        oPagosSuplementosData.uspActualizarPagoSuplementosEstado(item);
                                    }
                                    else
                                    {
                                        codigo = 0;
                                    }

                                    break;

                            }
                        }
                        tx.Complete();
                        oRespPagosSuplementosDTO.Success = true;
                        oRespPagosSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespPagosSuplementosDTO.Success = false;
                        oRespPagosSuplementosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespPagosSuplementosDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
