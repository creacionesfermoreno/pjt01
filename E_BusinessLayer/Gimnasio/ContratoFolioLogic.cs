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
	    
	public class ContratoFolioLogic : IDisposable
	{
		ContratoFolioData oContratoFolioData = null;
        public ContratoFolioLogic()
		{
            oContratoFolioData = new ContratoFolioData();
		}
		
        public RespItemContratoFolioDTO ContratoFolioGetItem(ReqFilterContratoFolioDTO oReqFilterContratoFolioDTO)
        {
            RespItemContratoFolioDTO oRespItemContratoFolioDTO = new RespItemContratoFolioDTO();

            oRespItemContratoFolioDTO.Success = false;
            oRespItemContratoFolioDTO.Item = null;
            oRespItemContratoFolioDTO.User = oReqFilterContratoFolioDTO.User;
            oRespItemContratoFolioDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterContratoFolioDTO.User))
            {
                oRespItemContratoFolioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Socios no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemContratoFolioDTO.MessageList.Count == 0)
            {
                ContratoFolioDTO oContratoMembresiaDTO = null;
                try
                {
                    switch (oReqFilterContratoFolioDTO.FilterCase)
                    {
                        case filterCaseContratoFolioDTO.porCodigo:
                            {
                                oContratoMembresiaDTO = new ContratoFolioDTO();
                                oContratoMembresiaDTO = oContratoFolioData.ListarContratoMembresia(oReqFilterContratoFolioDTO.Item);
                            }
                            break;
                        default:
                            {
                                oContratoMembresiaDTO = new ContratoFolioDTO();
                            }
                            break;
                    }

                    oRespItemContratoFolioDTO.Item = new ContratoFolioDTO();
                    oRespItemContratoFolioDTO.Item = oContratoMembresiaDTO;
                    oRespItemContratoFolioDTO.Success = true;
                    oRespItemContratoFolioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemContratoFolioDTO.Success = false;
                    oRespItemContratoFolioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemContratoFolioDTO;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
