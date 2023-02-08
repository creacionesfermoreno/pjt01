
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
	
	public class AdFitnessAtencionAlClienteLogic: IDisposable
	{
		AdFitnessAtencionAlClienteData oAdFitnessAtencionAlClienteData = null;
		public AdFitnessAtencionAlClienteLogic()
		{
			oAdFitnessAtencionAlClienteData = new AdFitnessAtencionAlClienteData();
		}
		
		public RespListAdFitnessAtencionAlClienteDTO AdFitnessAtencionAlClienteGetList(ReqFilterAdFitnessAtencionAlClienteDTO oReqFilterAdFitnessAtencionAlClienteDTO)
		{
		
			RespListAdFitnessAtencionAlClienteDTO oRespListAdFitnessAtencionAlClienteDTO = new RespListAdFitnessAtencionAlClienteDTO();
		
			oRespListAdFitnessAtencionAlClienteDTO.List = new List<AdFitnessAtencionAlClienteDTO>();
			oRespListAdFitnessAtencionAlClienteDTO.User = oReqFilterAdFitnessAtencionAlClienteDTO.User;
			oRespListAdFitnessAtencionAlClienteDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterAdFitnessAtencionAlClienteDTO.User))
            {
                oRespListAdFitnessAtencionAlClienteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de AdFitnessAtencionAlCliente no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterAdFitnessAtencionAlClienteDTO.Paging == null)
            {
                oRespListAdFitnessAtencionAlClienteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListAdFitnessAtencionAlClienteDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterAdFitnessAtencionAlClienteDTO.Paging.All && oReqFilterAdFitnessAtencionAlClienteDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterAdFitnessAtencionAlClienteDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<AdFitnessAtencionAlClienteDTO> AdFitnessAtencionAlClienteDTOList = new List<AdFitnessAtencionAlClienteDTO>();

                    switch (oReqFilterAdFitnessAtencionAlClienteDTO.FilterCase)
                    {
                        case filterCaseAdFitnessAtencionAlCliente.uspListarAdFitnessAtencionAlCliente_Paginacion:
                            if (!oReqFilterAdFitnessAtencionAlClienteDTO.Paging.All && oReqFilterAdFitnessAtencionAlClienteDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterAdFitnessAtencionAlClienteDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarAdFitnessAtencionAlCliente_NumeroRegistros"]);
                            }

                            AdFitnessAtencionAlClienteDTOList = oAdFitnessAtencionAlClienteData.uspListarAdFitnessAtencionAlCliente_Paginacion(oReqFilterAdFitnessAtencionAlClienteDTO.Item, oReqFilterAdFitnessAtencionAlClienteDTO.Paging);
                            break;
                    }

                    oRespListAdFitnessAtencionAlClienteDTO.List = AdFitnessAtencionAlClienteDTOList;
                    oRespListAdFitnessAtencionAlClienteDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListAdFitnessAtencionAlClienteDTO.Success = false;
                    oRespListAdFitnessAtencionAlClienteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListAdFitnessAtencionAlClienteDTO;	
           
		}
		
		
		public RespItemAdFitnessAtencionAlClienteDTO AdFitnessAtencionAlClienteGetItem(ReqFilterAdFitnessAtencionAlClienteDTO oReqFilterAdFitnessAtencionAlClienteDTO)
		{
			RespItemAdFitnessAtencionAlClienteDTO oRespItemAdFitnessAtencionAlClienteDTO = new RespItemAdFitnessAtencionAlClienteDTO();

            oRespItemAdFitnessAtencionAlClienteDTO.Success = false;
            oRespItemAdFitnessAtencionAlClienteDTO.Item = null;
            oRespItemAdFitnessAtencionAlClienteDTO.User = oReqFilterAdFitnessAtencionAlClienteDTO.User;
            oRespItemAdFitnessAtencionAlClienteDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterAdFitnessAtencionAlClienteDTO.User))
            {
                oRespItemAdFitnessAtencionAlClienteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de AdFitnessAtencionAlCliente no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemAdFitnessAtencionAlClienteDTO.MessageList.Count == 0)
            {
                AdFitnessAtencionAlClienteDTO oAdFitnessAtencionAlClienteDTO = null;
                try
                {
                    switch (oReqFilterAdFitnessAtencionAlClienteDTO.FilterCase)
                    {

                        case filterCaseAdFitnessAtencionAlCliente.uspListarAdFitnessAtencionAlCliente_NumeroRegistros:
                            {
                                oAdFitnessAtencionAlClienteDTO = new AdFitnessAtencionAlClienteDTO();
                                oAdFitnessAtencionAlClienteDTO = oAdFitnessAtencionAlClienteData.uspListarAdFitnessAtencionAlCliente_NumeroRegistros(oReqFilterAdFitnessAtencionAlClienteDTO.Item);
                            }
                            break;
                        default:
                            {
                                oAdFitnessAtencionAlClienteDTO = new AdFitnessAtencionAlClienteDTO();
                            }
                            break;
                    }

                    oRespItemAdFitnessAtencionAlClienteDTO.Item = new AdFitnessAtencionAlClienteDTO();
                    oRespItemAdFitnessAtencionAlClienteDTO.Item = oAdFitnessAtencionAlClienteDTO;
                    oRespItemAdFitnessAtencionAlClienteDTO.Success = true;
                    oRespItemAdFitnessAtencionAlClienteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemAdFitnessAtencionAlClienteDTO.Success = false;
                    oRespItemAdFitnessAtencionAlClienteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemAdFitnessAtencionAlClienteDTO;
		}
	
		
		public RespAdFitnessAtencionAlClienteDTO ExecuteTransac(ReqAdFitnessAtencionAlClienteDTO oReqAdFitnessAtencionAlClienteDTO)
		{
			RespAdFitnessAtencionAlClienteDTO oRespAdFitnessAtencionAlClienteDTO = new RespAdFitnessAtencionAlClienteDTO();

            oRespAdFitnessAtencionAlClienteDTO.MessageList = new List<Mensaje>();
            oRespAdFitnessAtencionAlClienteDTO.User = oReqAdFitnessAtencionAlClienteDTO.User;
            
            if (String.IsNullOrEmpty(oReqAdFitnessAtencionAlClienteDTO.User))
            {
                oRespAdFitnessAtencionAlClienteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de AdFitnessAtencionAlCliente no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespAdFitnessAtencionAlClienteDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (AdFitnessAtencionAlClienteDTO item in oReqAdFitnessAtencionAlClienteDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oAdFitnessAtencionAlClienteData.Registrar(item);
                                    break;
                                case Operation.uspActualizarEstadoAdFitness_AtencionCliente:
                                    oAdFitnessAtencionAlClienteData.uspActualizarEstadoAdFitness_AtencionCliente(item);
                                 break;


                            }
                        }
                        tx.Complete();
                        oRespAdFitnessAtencionAlClienteDTO.Success = true;
                        oRespAdFitnessAtencionAlClienteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespAdFitnessAtencionAlClienteDTO.Success = false;
                        oRespAdFitnessAtencionAlClienteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespAdFitnessAtencionAlClienteDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public AdFitnessAtencionAlClienteDTO uspValidarPagosClientes_AdFitness(int CodigoUnidadNegocio, int CodigoSede)
        {
            AdFitnessAtencionAlClienteDTO oAdFitnessAtencionAlClienteDTO = new AdFitnessAtencionAlClienteDTO();
            oAdFitnessAtencionAlClienteDTO = oAdFitnessAtencionAlClienteData.uspValidarPagosClientes_AdFitness(CodigoUnidadNegocio, CodigoSede);
            return oAdFitnessAtencionAlClienteDTO;
        }

        public int uspValidarIngresoUsuarios_Saludo_AdFitness(int CodigoUnidadNegocio, int CodigoSede, string User)
        {
            int flag = 0;
            flag = oAdFitnessAtencionAlClienteData.uspValidarIngresoUsuarios_Saludo_AdFitness(CodigoUnidadNegocio, CodigoSede, User);
            return flag;
        }


    }
}
