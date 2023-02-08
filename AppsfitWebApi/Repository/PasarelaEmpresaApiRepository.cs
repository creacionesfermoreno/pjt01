
using AppsfitWebApi.Models;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using E_BusinessLayer.Gimnasio;

namespace AppsfitWebApi.Repository
{
    public class PasarelaEmpresaApiRepository
    {

       
        public PasarelaEmpresaDTO PasarelaActiveRepository(string DefaultKeyEmpresa)
        {
            PasarelaEmpresaDTO oPasarelaEmpresaDTO = new PasarelaEmpresaDTO();
            oPasarelaEmpresaDTO.DefaultKeyEmpresa = DefaultKeyEmpresa;
            ReqFilterPasarelaEmpresaDTO oReq = new ReqFilterPasarelaEmpresaDTO()
            {
                FilterCase = FilterCasePasarelaEmpresa.ListActive,
                Item = oPasarelaEmpresaDTO,
                User = "Admin",
            };
            RespItemPasarelaEmpresaDTO oResp = null;
            using (PasarelaEmpresaLogic oPasarelaEmpresaLogic = new PasarelaEmpresaLogic())
            {
                oResp = oPasarelaEmpresaLogic.GetItem(oReq);
            }
            if (oResp.Success)
            {
                oPasarelaEmpresaDTO = oResp.Item;
            }
            return oPasarelaEmpresaDTO;
        }

    }
}