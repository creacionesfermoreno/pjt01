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
    public class UbicacionesLogic : IDisposable
    {
        UbicacionesData oUbicacionesData = null;
        public UbicacionesLogic()
		{
            oUbicacionesData = new UbicacionesData();
		}

        //-------------------------------------------------------------------
        //Nombre:	UbicacionesGetList
        //Objetivo: Retorna una colección de registros de tipo UbicacionesDTO
        //Valores Prueba:
        //Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //-------------------------------------------------------------------
        public RespListUbicacionesDTO UbicacionesGetList(ReqFilterUbicacionesDTO oReqFilterUbicacionesDTO)
        {

            RespListUbicacionesDTO oRespListUbicacionesDTO = new RespListUbicacionesDTO();

            oRespListUbicacionesDTO.List = new List<UbicacionesDTO>();
            oRespListUbicacionesDTO.User = oReqFilterUbicacionesDTO.User;
            oRespListUbicacionesDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterUbicacionesDTO.User))
            {
                oRespListUbicacionesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Ubicaciones no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oReqFilterUbicacionesDTO.Paging == null)
            {
                oRespListUbicacionesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespListUbicacionesDTO.MessageList.Count == 0)
            {

                try
                {
                    
                    if (!oReqFilterUbicacionesDTO.Paging.All && oReqFilterUbicacionesDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterUbicacionesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<UbicacionesDTO> UbicacionesDTOList = new List<UbicacionesDTO>();

                    switch (oReqFilterUbicacionesDTO.FilterCase)
                    {
                        default:
                            {
                                UbicacionesDTOList = oUbicacionesData.Listar(oReqFilterUbicacionesDTO.Item);
                            }
                            break;
                    }

                    oRespListUbicacionesDTO.List = UbicacionesDTOList;
                    oRespListUbicacionesDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListUbicacionesDTO.Success = false;
                    oRespListUbicacionesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }

            return oRespListUbicacionesDTO;

        }
		

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


    }
}
