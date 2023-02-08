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
	//Archivo     : ProfesionalFitnessLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 31/08/2017
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class ProfesionalFitnessLogic: IDisposable
	{
		ProfesionalFitnessData oProfesionalFitnessData = null;
		public ProfesionalFitnessLogic()
		{
			oProfesionalFitnessData = new ProfesionalFitnessData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ProfesionalFitnessGetList
		//Objetivo: Retorna una colección de registros de tipo ProfesionalFitnessDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListProfesionalFitnessDTO ProfesionalFitnessGetList(ReqFilterProfesionalFitnessDTO oReqFilterProfesionalFitnessDTO)
		{
		
			RespListProfesionalFitnessDTO oRespListProfesionalFitnessDTO = new RespListProfesionalFitnessDTO();
		
			oRespListProfesionalFitnessDTO.List = new List<ProfesionalFitnessDTO>();
			oRespListProfesionalFitnessDTO.User = oReqFilterProfesionalFitnessDTO.User;
			oRespListProfesionalFitnessDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterProfesionalFitnessDTO.User))
            {
                oRespListProfesionalFitnessDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ProfesionalFitness no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterProfesionalFitnessDTO.Paging == null)
            {
                oRespListProfesionalFitnessDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListProfesionalFitnessDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterProfesionalFitnessDTO.Paging.All && oReqFilterProfesionalFitnessDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterProfesionalFitnessDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<ProfesionalFitnessDTO> ProfesionalFitnessDTOList = new List<ProfesionalFitnessDTO>();

                    switch (oReqFilterProfesionalFitnessDTO.FilterCase)
                    {
                        default:
                            {
                                ProfesionalFitnessDTOList = oProfesionalFitnessData.Listar(oReqFilterProfesionalFitnessDTO.Item, oReqFilterProfesionalFitnessDTO.Paging, ref recordCount);
                            }
                            break;
                    }

                    oRespListProfesionalFitnessDTO.List = ProfesionalFitnessDTOList;
                    oRespListProfesionalFitnessDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListProfesionalFitnessDTO.Success = false;
                    oRespListProfesionalFitnessDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListProfesionalFitnessDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ProfesionalFitnessGetItem
		//Objetivo: Retorna un registro de tipo ProfesionalFitnessDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemProfesionalFitnessDTO ProfesionalFitnessGetItem(ReqFilterProfesionalFitnessDTO oReqFilterProfesionalFitnessDTO)
		{
			RespItemProfesionalFitnessDTO oRespItemProfesionalFitnessDTO = new RespItemProfesionalFitnessDTO();

            oRespItemProfesionalFitnessDTO.Success = false;
            oRespItemProfesionalFitnessDTO.Item = null;
            oRespItemProfesionalFitnessDTO.User = oReqFilterProfesionalFitnessDTO.User;
            oRespItemProfesionalFitnessDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterProfesionalFitnessDTO.User))
            {
                oRespItemProfesionalFitnessDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ProfesionalFitness no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemProfesionalFitnessDTO.MessageList.Count == 0)
            {
                ProfesionalFitnessDTO oProfesionalFitnessDTO = null;
                try
                {
                    switch (oReqFilterProfesionalFitnessDTO.FilterCase)
                    {
                        case filterCaseProfesionalFitness.BuscarNumeroDocumento:
                            {
                                oProfesionalFitnessDTO = new ProfesionalFitnessDTO();
                                oProfesionalFitnessDTO = oProfesionalFitnessData.uspBuscarProfesionalFitnessPorDNI(oReqFilterProfesionalFitnessDTO.Item);
                            }
                            break;
                        case filterCaseProfesionalFitness.uspBuscarProfesionalFitnessPorCodigo:
                            {
                                oProfesionalFitnessDTO = new ProfesionalFitnessDTO();
                                oProfesionalFitnessDTO = oProfesionalFitnessData.uspBuscarProfesionalFitnessPorCodigo(oReqFilterProfesionalFitnessDTO.Item);
                            }
                            break;
                        case filterCaseProfesionalFitness.BuscarPorNombres:
                            {
                                oProfesionalFitnessDTO = new ProfesionalFitnessDTO();
                                oProfesionalFitnessDTO = oProfesionalFitnessData.uspBuscarProfesionalFitnessPorNombres(oReqFilterProfesionalFitnessDTO.Item);
                            }
                            break;

                    }

                    oRespItemProfesionalFitnessDTO.Item = new ProfesionalFitnessDTO();
                    oRespItemProfesionalFitnessDTO.Item = oProfesionalFitnessDTO;
                    oRespItemProfesionalFitnessDTO.Success = true;
                    oRespItemProfesionalFitnessDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemProfesionalFitnessDTO.Success = false;
                    oRespItemProfesionalFitnessDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemProfesionalFitnessDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo ProfesionalFitnessDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespProfesionalFitnessDTO ExecuteTransac(ReqProfesionalFitnessDTO oReqProfesionalFitnessDTO)
		{
			RespProfesionalFitnessDTO oRespProfesionalFitnessDTO = new RespProfesionalFitnessDTO();

            oRespProfesionalFitnessDTO.MessageList = new List<Mensaje>();
            oRespProfesionalFitnessDTO.User = oReqProfesionalFitnessDTO.User;
            
            if (String.IsNullOrEmpty(oReqProfesionalFitnessDTO.User))
            {
                oRespProfesionalFitnessDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ProfesionalFitness no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespProfesionalFitnessDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        //int Codigo = 0;
                        foreach (ProfesionalFitnessDTO item in oReqProfesionalFitnessDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    if (oProfesionalFitnessData.uspValidadorDNIProfesionalFitness(item.DNI)>0)
                                    {
                                        throw new Exception("Ya existe un profesional registrado con el numero documento :" + item.DNI);
                                    }
                                    else
                                    {
                                     string CodigoProfesional = oProfesionalFitnessData.Registrar(item);
                                        oProfesionalFitnessData.RegistrarActualizarProfesionalFitnessPago(new ProfesionalFitnessDTO.ProfesionalFitnessPagos()
                                        {
                                            CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                                            CodigoProfesional = CodigoProfesional,
                                            CodigoSede = item.CodigoSede,
                                            UsuarioCreacion = item.UsuarioCreacion,
                                            CostoPorHora = item.CostoPorHora,
                                            DstoPorMinuto = item.DstoPorMinuto
                                        });
                                    }
                                    break;
                                case Operation.Update:
                                    oProfesionalFitnessData.Actualizar(item);
                                    oProfesionalFitnessData.RegistrarActualizarProfesionalFitnessPago(new ProfesionalFitnessDTO.ProfesionalFitnessPagos()
                                    {
                                        CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                                        CodigoProfesional = item.CodigoProfesional,
                                        CodigoSede = item.CodigoSede,
                                        UsuarioCreacion = item.UsuarioCreacion,
                                        CostoPorHora = item.CostoPorHora,
                                        DstoPorMinuto = item.DstoPorMinuto
                                    });
                                    break;
                                case Operation.ActualizarFotoProfesor:
                                    oProfesionalFitnessData.ActualizarFoto(item);
                                    break;
                               
                            }
                        }
                        tx.Complete();
                        oRespProfesionalFitnessDTO.Success = true;
                        oRespProfesionalFitnessDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            //Codigo = Codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespProfesionalFitnessDTO.Success = false;
                        oRespProfesionalFitnessDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespProfesionalFitnessDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public int uspValidadorDNIProfesionalFitness(string dni)
        {
            int flag = 0;
            flag = oProfesionalFitnessData.uspValidadorDNIProfesionalFitness(dni);
            return flag;
        }

    }
}
