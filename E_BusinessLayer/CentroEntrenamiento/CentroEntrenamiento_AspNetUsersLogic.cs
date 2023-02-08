using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer.CentroEntrenamiento;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;

namespace E_BusinessLayer.CentroEntrenamiento
{
    public class CentroEntrenamiento_AspNetUsersLogic : IDisposable
    {
        CentroEntrenamiento_AspNetUsersData oCentroEntrenamiento_AspNetUsersData = null;
        public CentroEntrenamiento_AspNetUsersLogic()
        {
            oCentroEntrenamiento_AspNetUsersData = new CentroEntrenamiento_AspNetUsersData();
        }

        public RespItemCentroEntrenamiento_AspNetUsersDTO ResponseGetItem(ReqFilterCentroEntrenamiento_AspNetUsersDTO oReqFilter)
        {
            RespItemCentroEntrenamiento_AspNetUsersDTO oRespItem = new RespItemCentroEntrenamiento_AspNetUsersDTO();

            oRespItem.Success = false;
            oRespItem.Item = null;
            oRespItem.User = oReqFilter.User;
            oRespItem.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilter.User))
            {
                oRespItem.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de usuario no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItem.MessageList.Count == 0)
            {
                CentroEntrenamiento_AspNetUsersDTO oItem = null;
                try
                {
                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseCentroEntrenamiento_AspNetUsers.CentroEntrenamiento_uspBuscarAspNetUsers_imprimirticket_DefaultKey:
                            {
                                oItem = new CentroEntrenamiento_AspNetUsersDTO();
                                oItem = oCentroEntrenamiento_AspNetUsersData.CentroEntrenamiento_uspBuscarAspNetUsers_imprimirticket_DefaultKey(oReqFilter.Item);
                            }
                            break;
                        default:
                            {
                                oItem = new CentroEntrenamiento_AspNetUsersDTO();
                            }
                            break;
                    }

                    oRespItem.Item = new CentroEntrenamiento_AspNetUsersDTO();
                    oRespItem.Item = oItem;
                    oRespItem.Success = true;
                    oRespItem.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItem.Success = false;
                    oRespItem.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItem;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }


  
}
