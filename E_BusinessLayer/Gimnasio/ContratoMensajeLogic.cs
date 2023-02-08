
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
	
	public class ContratoMensajeLogic: IDisposable
	{
		ContratoMensajeData oContratoMensajeData = null;
		public ContratoMensajeLogic()
		{
			oContratoMensajeData = new ContratoMensajeData();
		}
		
		public RespListContratoMensajeDTO ContratoMensajeGetList(ReqFilterContratoMensajeDTO oReqFilterContratoMensajeDTO)
		{
		
			RespListContratoMensajeDTO oRespListContratoMensajeDTO = new RespListContratoMensajeDTO();
		
			oRespListContratoMensajeDTO.List = new List<ContratoMensajeDTO>();
			oRespListContratoMensajeDTO.User = oReqFilterContratoMensajeDTO.User;
			oRespListContratoMensajeDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterContratoMensajeDTO.User))
            {
                oRespListContratoMensajeDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de MensajesMenbresia no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterContratoMensajeDTO.Paging == null)
            {
                oRespListContratoMensajeDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListContratoMensajeDTO.MessageList.Count == 0)
            {
                
                try
                {
                   
                    if (!oReqFilterContratoMensajeDTO.Paging.All && oReqFilterContratoMensajeDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterContratoMensajeDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<ContratoMensajeDTO> MensajesMenbresiaDTOList = new List<ContratoMensajeDTO>();

                    switch (oReqFilterContratoMensajeDTO.FilterCase)
                    {
                        case filterCaseContratoMensaje.ListarMensajesMenbresia:
                            {
                                MensajesMenbresiaDTOList = oContratoMensajeData.ListarMensajesMenbresia(oReqFilterContratoMensajeDTO.Item);
                            }
                            break;
                        
                    }

                    oRespListContratoMensajeDTO.List = MensajesMenbresiaDTOList;
                    oRespListContratoMensajeDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListContratoMensajeDTO.Success = false;
                    oRespListContratoMensajeDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListContratoMensajeDTO;	
           
		}
		
		public RespItemContratoMensajeDTO ContratoMensajeGetItem(ReqFilterContratoMensajeDTO oReqFilterContratoMensajeDTO)
		{
			RespItemContratoMensajeDTO oRespItemContratoMensajeDTO = new RespItemContratoMensajeDTO();

            oRespItemContratoMensajeDTO.Success = false;
            oRespItemContratoMensajeDTO.Item = null;
            oRespItemContratoMensajeDTO.User = oReqFilterContratoMensajeDTO.User;
            oRespItemContratoMensajeDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterContratoMensajeDTO.User))
            {
                oRespItemContratoMensajeDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de MensajesMenbresia no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemContratoMensajeDTO.MessageList.Count == 0)
            {
                ContratoMensajeDTO oContratoMensajeDTO = null;
                try
                {
                    switch (oReqFilterContratoMensajeDTO.FilterCase)
                    {
                        default:
                            {
                                oContratoMensajeDTO = new ContratoMensajeDTO();
                            }
                            break;
                    }

                    oRespItemContratoMensajeDTO.Item = new ContratoMensajeDTO();
                    oRespItemContratoMensajeDTO.Item = oContratoMensajeDTO;
                    oRespItemContratoMensajeDTO.Success = true;
                    oRespItemContratoMensajeDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemContratoMensajeDTO.Success = false;
                    oRespItemContratoMensajeDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemContratoMensajeDTO;
		}
	    	
		public RespContratoMensajeDTO ExecuteTransac(ReqContratoMensajeDTO oReqContratoMensajeDTO)
		{
			RespContratoMensajeDTO oRespContratoMensajeDTO = new RespContratoMensajeDTO();

            oRespContratoMensajeDTO.MessageList = new List<Mensaje>();
            oRespContratoMensajeDTO.User = oReqContratoMensajeDTO.User;
            
            if (String.IsNullOrEmpty(oReqContratoMensajeDTO.User))
            {
                oRespContratoMensajeDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de MensajesMenbresia no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespContratoMensajeDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int codigo = 0;
                        foreach (ContratoMensajeDTO item in oReqContratoMensajeDTO.List)
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

                                    codigo = 999999999;
                                    oContratoMensajeData.Registrar(item);
                                    //oUsuariosIngresosDTOCreate = oUsuariosIngresosDataCreate.uspValidarAccesoSistema(oUsuariosIngresosDTOCreate);
                                    //if (oUsuariosIngresosDTOCreate.CodigoValidacion == 3)
                                    //{

                                    //}
                                    //else
                                    //{
                                    //    codigo = 0;
                                    //}

                                    break;
                              
                            }
                        }
                        tx.Complete();
                        oRespContratoMensajeDTO.Success = true;
                        oRespContratoMensajeDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespContratoMensajeDTO.Success = false;
                        oRespContratoMensajeDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespContratoMensajeDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
