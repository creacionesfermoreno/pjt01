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
	
	public class AgendaSeguimientoLogic: IDisposable
	{
		AgendaSeguimientoData oAgendaSeguimientoData = null;
		public AgendaSeguimientoLogic()
		{
			oAgendaSeguimientoData = new AgendaSeguimientoData();
		}
		
	
		public RespListAgendaSeguimientoDTO AgendaSeguimientoGetList(ReqFilterAgendaSeguimientoDTO oReqFilterAgendaSeguimientoDTO)
		{
			RespListAgendaSeguimientoDTO oRespListAgendaSeguimientoDTO = new RespListAgendaSeguimientoDTO();
		
			oRespListAgendaSeguimientoDTO.List = new List<AgendaSeguimientoDTO>();
			oRespListAgendaSeguimientoDTO.User = oReqFilterAgendaSeguimientoDTO.User;
			oRespListAgendaSeguimientoDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterAgendaSeguimientoDTO.User))
            {
                oRespListAgendaSeguimientoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de AgendaSeguimiento no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterAgendaSeguimientoDTO.Paging == null)
            {
                oRespListAgendaSeguimientoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListAgendaSeguimientoDTO.MessageList.Count == 0)
            {
                
                try
                {
                    
                  

                    List<AgendaSeguimientoDTO> AgendaSeguimientoDTOList = new List<AgendaSeguimientoDTO>();

                    switch (oReqFilterAgendaSeguimientoDTO.FilterCase)
                    {

                        case filterCaseAgendaSeguimiento.uspListarGridAgendaGeneral_Paginacion:
                            {
                                if (!oReqFilterAgendaSeguimientoDTO.Paging.All && oReqFilterAgendaSeguimientoDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterAgendaSeguimientoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarGridAgendaGeneral_NumeroRegistros"]);
                                }

                                AgendaSeguimientoDTOList = oAgendaSeguimientoData.uspListarGridAgendaGeneral_Paginacion(oReqFilterAgendaSeguimientoDTO.Item, oReqFilterAgendaSeguimientoDTO.Paging);

                            }
                            break;
                        case filterCaseAgendaSeguimiento.uspListarGridAgendaGeneralAuditoria_Paginacion:
                            {
                                if (!oReqFilterAgendaSeguimientoDTO.Paging.All && oReqFilterAgendaSeguimientoDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterAgendaSeguimientoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarGridAgendaGeneral_NumeroRegistros"]);
                                }
                                AgendaSeguimientoDTOList = oAgendaSeguimientoData.uspListarGridAgendaGeneralAuditoria_Paginacion(oReqFilterAgendaSeguimientoDTO.Item, oReqFilterAgendaSeguimientoDTO.Paging);
                            }
                            break;
                      
                        case filterCaseAgendaSeguimiento.uspListarGridAgendaGeneralAuditoria_TotalActividadPorVendedor:
                            {

                                AgendaSeguimientoDTOList = oAgendaSeguimientoData.uspListarGridAgendaGeneralAuditoria_TotalActividadPorVendedor(oReqFilterAgendaSeguimientoDTO.Item);
                            }
                            break;                      
                        case filterCaseAgendaSeguimiento.uspListarGridAgendaGeneral_ExportarExcel:
                            {
                                if (!oReqFilterAgendaSeguimientoDTO.Paging.All && oReqFilterAgendaSeguimientoDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterAgendaSeguimientoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarGridAgendaGeneral_NumeroRegistros"]);
                                }
                                AgendaSeguimientoDTOList = oAgendaSeguimientoData.uspListarGridAgendaGeneral_ExportarExcel(oReqFilterAgendaSeguimientoDTO.Item, oReqFilterAgendaSeguimientoDTO.Paging);
                            }
                            break;
                      
                        case filterCaseAgendaSeguimiento.ListarInformeCitaVendedores:
                            {
                                AgendaSeguimientoDTOList = oAgendaSeguimientoData.uspListarInformeCantidadCitasVendedores(oReqFilterAgendaSeguimientoDTO.Item);
                            }
                            break;
                        case filterCaseAgendaSeguimiento.ListarVerSeguimientoAgendaSocios:
                            {
                                AgendaSeguimientoDTOList = oAgendaSeguimientoData.uspListarVerSeguimientoAgendaSocios(oReqFilterAgendaSeguimientoDTO.Item);
                            }
                            break;

                        default:
                            {
                                AgendaSeguimientoDTOList = oAgendaSeguimientoData.Listar(oReqFilterAgendaSeguimientoDTO.Item);
                            }
                            break;

                    }

                    oRespListAgendaSeguimientoDTO.List = AgendaSeguimientoDTOList;
                    oRespListAgendaSeguimientoDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListAgendaSeguimientoDTO.Success = false;
                    oRespListAgendaSeguimientoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListAgendaSeguimientoDTO;	
           
		}
		
		
		public RespItemAgendaSeguimientoDTO AgendaSeguimientoGetItem(ReqFilterAgendaSeguimientoDTO oReqFilterAgendaSeguimientoDTO)
		{
			RespItemAgendaSeguimientoDTO oRespItemAgendaSeguimientoDTO = new RespItemAgendaSeguimientoDTO();

            oRespItemAgendaSeguimientoDTO.Success = false;
            oRespItemAgendaSeguimientoDTO.Item = null;
            oRespItemAgendaSeguimientoDTO.User = oReqFilterAgendaSeguimientoDTO.User;
            oRespItemAgendaSeguimientoDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterAgendaSeguimientoDTO.User))
            {
                oRespItemAgendaSeguimientoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de AgendaSeguimiento no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemAgendaSeguimientoDTO.MessageList.Count == 0)
            {
                AgendaSeguimientoDTO oAgendaSeguimientoDTO = null;
                try
                {
                    switch (oReqFilterAgendaSeguimientoDTO.FilterCase)
                    {

                        case filterCaseAgendaSeguimiento.uspListarGridAgendaGeneral_NumeroRegistros:
                            {
                                oAgendaSeguimientoDTO = new AgendaSeguimientoDTO();
                                oAgendaSeguimientoDTO = oAgendaSeguimientoData.uspListarGridAgendaGeneral_NumeroRegistros(oReqFilterAgendaSeguimientoDTO.Item);
                            }
                            break;
                        case filterCaseAgendaSeguimiento.uspListarGridAgendaGeneralAuditoria_NumeroRegistros:
                            {
                                oAgendaSeguimientoDTO = new AgendaSeguimientoDTO();
                                oAgendaSeguimientoDTO = oAgendaSeguimientoData.uspListarGridAgendaGeneralAuditoria_NumeroRegistros(oReqFilterAgendaSeguimientoDTO.Item);
                            }
                            break;
                      
                        default:
                            {
                                oAgendaSeguimientoDTO = new AgendaSeguimientoDTO();
                            }
                            break;
                    }

                    oRespItemAgendaSeguimientoDTO.Item = new AgendaSeguimientoDTO();
                    oRespItemAgendaSeguimientoDTO.Item = oAgendaSeguimientoDTO;
                    oRespItemAgendaSeguimientoDTO.Success = true;
                    oRespItemAgendaSeguimientoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemAgendaSeguimientoDTO.Success = false;
                    oRespItemAgendaSeguimientoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemAgendaSeguimientoDTO;
		}
	
		
		public RespAgendaSeguimientoDTO ExecuteTransac(ReqAgendaSeguimientoDTO oReqAgendaSeguimientoDTO)
		{
			RespAgendaSeguimientoDTO oRespAgendaSeguimientoDTO = new RespAgendaSeguimientoDTO();

            oRespAgendaSeguimientoDTO.MessageList = new List<Mensaje>();
            oRespAgendaSeguimientoDTO.User = oReqAgendaSeguimientoDTO.User;
            
            if (String.IsNullOrEmpty(oReqAgendaSeguimientoDTO.User))
            {
                oRespAgendaSeguimientoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de AgendaSeguimiento no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespAgendaSeguimientoDTO.MessageList.Count == 0)
            {
                string Mensaje = "";
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int codigo = 0;
                        foreach (AgendaSeguimientoDTO item in oReqAgendaSeguimientoDTO.List)
                        {
                            switch (item.Operation)
                            {
                             
                                case Operation.Create_UspAgendaSeguimientoTodos:
                                    codigo = 999999999;
                                    oAgendaSeguimientoData.RegistrarAgendaSeguimientoTodos(item);

                                    break;
                                case Operation.Create_UspReagendarAgendaSeguimientoTodosCaidos:
                                    codigo = 999999999;
                                    oAgendaSeguimientoData.UspReagendarAgendaSeguimientoTodosCaidos(item);

                                    break;
                            }
                        }
                        tx.Complete();
                        oRespAgendaSeguimientoDTO.Success = true;
                        oRespAgendaSeguimientoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = codigo,
                            Detalle = Mensaje,
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespAgendaSeguimientoDTO.Success = false;
                        oRespAgendaSeguimientoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespAgendaSeguimientoDTO;
		}

        public int uspValidarExisteCitaAgendaGeneral(int CodigoSocio, int CodigoTipoAgenda, int CodSede, int CodigoUnidadNegocio)
        {
            int flag = 0;
            flag = oAgendaSeguimientoData.uspValidarExisteCitaAgendaGeneral(CodigoSocio, CodigoTipoAgenda, CodSede, CodigoUnidadNegocio);
            return flag;
        }
        
        public int uspCerrarCitaClienteAgenda(int CodigoCita,int CodigoCliente, int Tipo, string User, int CodSede, int CodigoUnidadNegocio)
        {
            int flag = 0;
            flag = oAgendaSeguimientoData.uspCerrarCitaClienteAgenda(CodigoCita,CodigoCliente, Tipo, User, CodSede, CodigoUnidadNegocio);
            return flag;
        }

        public int uspValidarCitaAgendarDesdeCliente(int CodigoSocio, string Vendedor, int CodigoTipoAgenda, int CodSede, int CodigoUnidadNegocio)
        {
            int flag = 0;
            flag = oAgendaSeguimientoData.uspValidarCitaAgendarDesdeCliente(CodigoSocio,Vendedor, CodigoTipoAgenda, CodSede, CodigoUnidadNegocio);
            return flag;
        }

		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
