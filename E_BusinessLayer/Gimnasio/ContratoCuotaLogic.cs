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
	
	public class ContratoCuotaLogic: IDisposable
	{
		ContratoCuotaData oContratoCuotaData = null;
		public ContratoCuotaLogic()
		{
			oContratoCuotaData = new ContratoCuotaData();
		}
		
        public RespListContratoCuotaDTO ContratoCuotaGetList(ReqFilterContratoCuotaDTO oReqFilterContratoCuotaDTO)
		{
			RespListContratoCuotaDTO oRespListContratoCuotaDTO = new RespListContratoCuotaDTO();
		    
			oRespListContratoCuotaDTO.List = new List<ContratoCuotaDTO>();
			oRespListContratoCuotaDTO.User = oReqFilterContratoCuotaDTO.User;
			oRespListContratoCuotaDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterContratoCuotaDTO.User))
            {
                oRespListContratoCuotaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de MembresiasCuota no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterContratoCuotaDTO.Paging == null)
            {
                oRespListContratoCuotaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListContratoCuotaDTO.MessageList.Count == 0)
            {
                
                try
                {
                    
                    if (!oReqFilterContratoCuotaDTO.Paging.All && oReqFilterContratoCuotaDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterContratoCuotaDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<ContratoCuotaDTO> ContratoCuotaDTOList = new List<ContratoCuotaDTO>();

                    switch (oReqFilterContratoCuotaDTO.FilterCase)
                    {

                        case filterCaseContratoCuota.porCodigoMembresia:
                            {
                                ContratoCuotaDTOList = oContratoCuotaData.Listar(oReqFilterContratoCuotaDTO.Item);
                            }
                            break;
                      case filterCaseContratoCuota.uspListarClientesMenbresiasCuotas:
                            {
                                ContratoCuotaDTOList = oContratoCuotaData.uspListarClientesMenbresiasCuotas(oReqFilterContratoCuotaDTO.Item);
                            }
                            break;

                        default:
                            {
                                ContratoCuotaDTOList = oContratoCuotaData.Listar(oReqFilterContratoCuotaDTO.Item);
                            }
                            break;
                    }

                    oRespListContratoCuotaDTO.List = ContratoCuotaDTOList;
                    oRespListContratoCuotaDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListContratoCuotaDTO.Success = false;
                    oRespListContratoCuotaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListContratoCuotaDTO;	
           
		}

		public RespItemContratoCuotaDTO ContratoCuotaGetItem(ReqFilterContratoCuotaDTO oReqFilterContratoCuotaDTO)
		{
			RespItemContratoCuotaDTO oRespItemContratoCuotaDTO = new RespItemContratoCuotaDTO();

            oRespItemContratoCuotaDTO.Success = false;
            oRespItemContratoCuotaDTO.Item = null;
            oRespItemContratoCuotaDTO.User = oReqFilterContratoCuotaDTO.User;
            oRespItemContratoCuotaDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterContratoCuotaDTO.User))
            {
                oRespItemContratoCuotaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de MembresiasCuota no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemContratoCuotaDTO.MessageList.Count == 0)
            {
                ContratoCuotaDTO oContratoCuotaDTO = null;
                try
                {
                    switch (oReqFilterContratoCuotaDTO.FilterCase)
                    {                       
                        default:
                            {
                                oContratoCuotaDTO = new ContratoCuotaDTO();
                            }
                            break;
                    }

                    oRespItemContratoCuotaDTO.Item = new ContratoCuotaDTO();
                    oRespItemContratoCuotaDTO.Item = oContratoCuotaDTO;
                    oRespItemContratoCuotaDTO.Success = true;
                    oRespItemContratoCuotaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemContratoCuotaDTO.Success = false;
                    oRespItemContratoCuotaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemContratoCuotaDTO;
		}
            
		public RespContratoCuotaDTO ExecuteTransac(ReqContratoCuotaDTO oReqContratoCuotaDTO)
		{
			RespContratoCuotaDTO oRespContratoCuotaDTO = new RespContratoCuotaDTO();

            oRespContratoCuotaDTO.MessageList = new List<Mensaje>();
            oRespContratoCuotaDTO.User = oReqContratoCuotaDTO.User;
            
            if (String.IsNullOrEmpty(oReqContratoCuotaDTO.User))
            {
                oRespContratoCuotaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de MembresiasCuota no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespContratoCuotaDTO.MessageList.Count == 0)
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int CodigoCuota = 0;
                        foreach (ContratoCuotaDTO item in oReqContratoCuotaDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    CodigoCuota = 100; //solo es para saber si ingreso a registrar
                                    oContratoCuotaData.uspRegistrarCuotasContrato(item);

                                    break;                             
                                case Operation.Delete:
                                    CodigoCuota = 100; //solo es para saber si ingreso a eliminar
                                    oContratoCuotaData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespContratoCuotaDTO.Success = true;
                        oRespContratoCuotaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = CodigoCuota,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespContratoCuotaDTO.Success = false;
                        oRespContratoCuotaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespContratoCuotaDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
