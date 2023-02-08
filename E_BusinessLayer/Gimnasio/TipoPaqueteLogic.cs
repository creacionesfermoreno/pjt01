
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

	public class TipoPaqueteLogic: IDisposable
	{
		TipoPaqueteData oTipoPaqueteData = null;
		public TipoPaqueteLogic()
		{
			oTipoPaqueteData = new TipoPaqueteData();
		}
		
		public RespListTipoPaqueteDTO TipoPaqueteGetList(ReqFilterTipoPaqueteDTO oReqFilterTipoPaqueteDTO)
		{
		
			RespListTipoPaqueteDTO oRespListTipoPaqueteDTO = new RespListTipoPaqueteDTO();
		
			oRespListTipoPaqueteDTO.List = new List<TipoPaqueteDTO>();
			oRespListTipoPaqueteDTO.User = oReqFilterTipoPaqueteDTO.User;
			oRespListTipoPaqueteDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterTipoPaqueteDTO.User))
            {
                oRespListTipoPaqueteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoPaquete no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterTipoPaqueteDTO.Paging == null)
            {
                oRespListTipoPaqueteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListTipoPaqueteDTO.MessageList.Count == 0)
            {
                
                try
                {
                    if (!oReqFilterTipoPaqueteDTO.Paging.All && oReqFilterTipoPaqueteDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterTipoPaqueteDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<TipoPaqueteDTO> TipoPaqueteDTOList = new List<TipoPaqueteDTO>();

                    switch (oReqFilterTipoPaqueteDTO.FilterCase)
                    {
                        case filterCaseTipoPaquete.FilteruspListaDllTipoPaquete:
                            {
                                TipoPaqueteDTOList = oTipoPaqueteData.ListaDllTipoPaquete(oReqFilterTipoPaqueteDTO.Item);
                            }
                            break;
                        default:
                            {
                                TipoPaqueteDTOList = oTipoPaqueteData.Listar(oReqFilterTipoPaqueteDTO.Item);
                            }
                            break;
                    }

                    oRespListTipoPaqueteDTO.List = TipoPaqueteDTOList;
                    oRespListTipoPaqueteDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListTipoPaqueteDTO.Success = false;
                    oRespListTipoPaqueteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListTipoPaqueteDTO;	
           
		}
		
		public RespItemTipoPaqueteDTO TipoPaqueteGetItem(ReqFilterTipoPaqueteDTO oReqFilterTipoPaqueteDTO)
		{
			RespItemTipoPaqueteDTO oRespItemTipoPaqueteDTO = new RespItemTipoPaqueteDTO();

            oRespItemTipoPaqueteDTO.Success = false;
            oRespItemTipoPaqueteDTO.Item = null;
            oRespItemTipoPaqueteDTO.User = oReqFilterTipoPaqueteDTO.User;
            oRespItemTipoPaqueteDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterTipoPaqueteDTO.User))
            {
                oRespItemTipoPaqueteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoPaquete no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemTipoPaqueteDTO.MessageList.Count == 0)
            {
                TipoPaqueteDTO oTipoPaqueteDTO = null;
                try
                {
                    switch (oReqFilterTipoPaqueteDTO.FilterCase)
                    {
                       
                        case filterCaseTipoPaquete.porCodigo:
                            {
                                oTipoPaqueteDTO = new TipoPaqueteDTO();
                                oTipoPaqueteDTO = oTipoPaqueteData.BuscarPorCodigoTipoPaquete(oReqFilterTipoPaqueteDTO.Item);
                            }
                            break;
                        default:
                            {
                                oTipoPaqueteDTO = new TipoPaqueteDTO();
                            }
                            break;
                    }

                    oRespItemTipoPaqueteDTO.Item = new TipoPaqueteDTO();
                    oRespItemTipoPaqueteDTO.Item = oTipoPaqueteDTO;
                    oRespItemTipoPaqueteDTO.Success = true;
                    oRespItemTipoPaqueteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemTipoPaqueteDTO.Success = false;
                    oRespItemTipoPaqueteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemTipoPaqueteDTO;
		}
	
		public RespTipoPaqueteDTO ExecuteTransac(ReqTipoPaqueteDTO oReqTipoPaqueteDTO)
		{
			RespTipoPaqueteDTO oRespTipoPaqueteDTO = new RespTipoPaqueteDTO();

            oRespTipoPaqueteDTO.MessageList = new List<Mensaje>();
            oRespTipoPaqueteDTO.User = oReqTipoPaqueteDTO.User;
            
            if (String.IsNullOrEmpty(oReqTipoPaqueteDTO.User))
            {
                oRespTipoPaqueteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoPaquete no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespTipoPaqueteDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (TipoPaqueteDTO item in oReqTipoPaqueteDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oTipoPaqueteData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oTipoPaqueteData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oTipoPaqueteData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespTipoPaqueteDTO.Success = true;
                        oRespTipoPaqueteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespTipoPaqueteDTO.Success = false;
                        oRespTipoPaqueteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespTipoPaqueteDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
