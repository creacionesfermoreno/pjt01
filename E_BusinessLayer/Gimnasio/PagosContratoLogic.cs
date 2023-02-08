
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
	
	public class PagosContratoLogic: IDisposable
	{
		PagosContratoData oPagosContratoData = null;
		public PagosContratoLogic()
		{
			oPagosContratoData = new PagosContratoData();
		}
		
		public RespListPagosContratoDTO PagosContratoGetList(ReqFilterPagosContratoDTO oReqFilterPagosContratoDTO)
		{		
			RespListPagosContratoDTO oRespListPagosContratoDTO = new RespListPagosContratoDTO();
		
			oRespListPagosContratoDTO.List = new List<PagosContratoDTO>();
			oRespListPagosContratoDTO.User = oReqFilterPagosContratoDTO.User;
			oRespListPagosContratoDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterPagosContratoDTO.User))
            {
                oRespListPagosContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PagoMembresia no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterPagosContratoDTO.Paging == null)
            {
                oRespListPagosContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListPagosContratoDTO.MessageList.Count == 0)
            {
                
                try
                {
                    
                    if (!oReqFilterPagosContratoDTO.Paging.All && oReqFilterPagosContratoDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterPagosContratoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<PagosContratoDTO> PagosContratoDTOList = new List<PagosContratoDTO>();

                    switch (oReqFilterPagosContratoDTO.FilterCase)
                    {

                        case filterCasePagosContrato.ListarPagosFormaPago:
                            {
                                PagosContratoData PMData = new PagosContratoData();                               
                                PagosContratoDTOList = PMData.Listar(oReqFilterPagosContratoDTO.Item);
                            }
                            break;
                        case filterCasePagosContrato.uspListarPagoMembresia_Anulados:
                            {
                                PagosContratoData PMData = new PagosContratoData();
                                PagosContratoDTOList = PMData.uspListarPagoMembresia_Anulados(oReqFilterPagosContratoDTO.Item);
                            }
                            break;
                        default:
                            {
                                PagosContratoDTOList = oPagosContratoData.Listar(oReqFilterPagosContratoDTO.Item);
                            }
                            break;
                    }

                    oRespListPagosContratoDTO.List = PagosContratoDTOList;
                    oRespListPagosContratoDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListPagosContratoDTO.Success = false;
                    oRespListPagosContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListPagosContratoDTO;	
           
		}
		
	
		public RespItemPagosContratoDTO PagosContratoGetItem(ReqFilterPagosContratoDTO oReqFilterPagosContratoDTO)
		{
			RespItemPagosContratoDTO oRespItemPagosContratoDTO = new RespItemPagosContratoDTO();

            oRespItemPagosContratoDTO.Success = false;
            oRespItemPagosContratoDTO.Item = null;
            oRespItemPagosContratoDTO.User = oReqFilterPagosContratoDTO.User;
            oRespItemPagosContratoDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterPagosContratoDTO.User))
            {
                oRespItemPagosContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PagoMembresia no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemPagosContratoDTO.MessageList.Count == 0)
            {
                PagosContratoDTO oPagoMembresiaDTO = null;
                try
                {
                    switch (oReqFilterPagosContratoDTO.FilterCase)
                    {
                    
                        default:
                            {
                                oPagoMembresiaDTO = new PagosContratoDTO();
                            }
                            break;
                    }

                    oRespItemPagosContratoDTO.Item = new PagosContratoDTO();
                    oRespItemPagosContratoDTO.Item = oPagoMembresiaDTO;
                    oRespItemPagosContratoDTO.Success = true;
                    oRespItemPagosContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemPagosContratoDTO.Success = false;
                    oRespItemPagosContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemPagosContratoDTO;
		}
	
		public RespPagosContratoDTO ExecuteTransac(ReqPagosContratoDTO oReqPagosContratoDTO)
		{
			RespPagosContratoDTO oRespPagoMembresiaDTO = new RespPagosContratoDTO();

            oRespPagoMembresiaDTO.MessageList = new List<Mensaje>();
            oRespPagoMembresiaDTO.User = oReqPagosContratoDTO.User;
            
            if (String.IsNullOrEmpty(oReqPagosContratoDTO.User))
            {
                oRespPagoMembresiaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PagoMembresia no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespPagoMembresiaDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        var Codigo = 0;
                        foreach (PagosContratoDTO item in oReqPagosContratoDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oPagosContratoData.Registrar(item);
                                    break;
                                case Operation.Delete:
                                    //UsuariosIngresosData oUsuariosIngresosDataDeleteMenbresia = new UsuariosIngresosData();
                                    //UsuariosIngresosDTO oUsuariosIngresosDTODeleteMenbresia = new UsuariosIngresosDTO();

                                    //oUsuariosIngresosDTODeleteMenbresia.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    //oUsuariosIngresosDTODeleteMenbresia.CodigoSede = item.CodigoSede;
                                    //oUsuariosIngresosDTODeleteMenbresia.UsuarioCreacion = item.UsuarioCreacion;
                                    //oUsuariosIngresosDTODeleteMenbresia.CodigoIngreso = item.TK_ID;
                                    //oUsuariosIngresosDTODeleteMenbresia.Latitud = item.TK_Latitude;
                                    //oUsuariosIngresosDTODeleteMenbresia.Longitud = item.TK_Longitude;

                                    //item.CodigoInicioSesion = item.TK_ID;
                                    //oUsuariosIngresosDTODeleteMenbresia = oUsuariosIngresosDataDeleteMenbresia.uspValidarAccesoSistema(oUsuariosIngresosDTODeleteMenbresia);
                                    //if (oUsuariosIngresosDTODeleteMenbresia.CodigoValidacion == 3)
                                    //{

                                    //}
                                    //else
                                    //{
                                    //    Codigo = 0;
                                    //}
                                    Codigo = 999999999;
                                    oPagosContratoData.Eliminar(item);
                                    break;

                            }
                        }
                        tx.Complete();
                        oRespPagoMembresiaDTO.Success = true;
                        oRespPagoMembresiaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = Codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespPagoMembresiaDTO.Success = false;
                        oRespPagoMembresiaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespPagoMembresiaDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
