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
    public class SociosFichaSaludMasterLogic : IDisposable
    {
        SociosFichaSaludMasterData oSociosFichaSaludMasterData = null;
        public SociosFichaSaludMasterLogic()
        {
            oSociosFichaSaludMasterData = new SociosFichaSaludMasterData();
        }

        public RespListSociosFichaSaludMasterDTO SociosFichaSaludMasterGetList(ReqFilterSociosFichaSaludMasterDTO oReqFilterSociosFichaSaludMasterDTO)
        {

            RespListSociosFichaSaludMasterDTO oRespListSociosFichaSaludMasterDTO = new RespListSociosFichaSaludMasterDTO();

            oRespListSociosFichaSaludMasterDTO.List = new List<SociosFichaSaludMasterDTO>();
            oRespListSociosFichaSaludMasterDTO.User = oReqFilterSociosFichaSaludMasterDTO.User;
            oRespListSociosFichaSaludMasterDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterSociosFichaSaludMasterDTO.User))
            {
                oRespListSociosFichaSaludMasterDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de CategoriaSuplemento no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oReqFilterSociosFichaSaludMasterDTO.Paging == null)
            {
                oRespListSociosFichaSaludMasterDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespListSociosFichaSaludMasterDTO.MessageList.Count == 0)
            {

                try
                {
                    uint recordCount = 0;

                    if (!oReqFilterSociosFichaSaludMasterDTO.Paging.All && oReqFilterSociosFichaSaludMasterDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterSociosFichaSaludMasterDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<SociosFichaSaludMasterDTO> SociosFichaSaludMasterDTOList = new List<SociosFichaSaludMasterDTO>();

                    switch (oReqFilterSociosFichaSaludMasterDTO.FilterCase)
                    {
                        default:
                            {
                                SociosFichaSaludMasterDTOList = oSociosFichaSaludMasterData.uspListarSociosFichaSaludMaster(oReqFilterSociosFichaSaludMasterDTO.Item);
                            }
                            break;
                    }

                    oRespListSociosFichaSaludMasterDTO.List = SociosFichaSaludMasterDTOList;
                    oRespListSociosFichaSaludMasterDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListSociosFichaSaludMasterDTO.Success = false;
                    oRespListSociosFichaSaludMasterDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }

            return oRespListSociosFichaSaludMasterDTO;

        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
