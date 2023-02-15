﻿using AppsfitWebApi.Models;
using AppsfitWebApi.Repository.Services;
using E_BusinessLayer.Gimnasio;
using E_DataModel.Common;
using E_DataModel.Corporativo;
using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AppsfitWebApi.Repository
{
    public class HomeRepository
    {

        string CLIENT_ID = "AbmZirpHBl-LcoP8oxKIzar_0A-U-uEGZMD1HQ-_I5Jcuv_jWXgBscRHLMuZQ2ioiO6HbFNP59G5VZae";
        string CLIENT_SECRET = "ELckHiGwNjKxSMxudP9XvvAUMowBEpSHqvgYKTNu2ndmphvcdy3JtZQjW1DJ4p9nH1c-GwK5DqCyfPPX";



        //validate Pasarela
        public async Task<ResponseApi> ValidPasarelaRepo(string DefaultKeyEmpresa,string CodigoPlantillaFormaPago)
        {
            ResponseApi responseModel = new ResponseApi();
            PaypalService paypalService = new PaypalService();

            //search acount pasarela
            var pasarela = GetItemAccountPaymentsRepo(DefaultKeyEmpresa, CodigoPlantillaFormaPago);
            if (string.IsNullOrEmpty(pasarela?.CodigoPlantillaFormaPago) || pasarela?.CodigoPlantillaFormaPago == "0")
            {
                responseModel.Message1 = "No tiene una cuenta de pasarela registrada";
                responseModel.Status = 1;
                responseModel.Success = false;
            }
            else
            {
                string type = pasarela?.DesFormaPago;
                responseModel.Message3 = type;
                switch (type.ToUpper())
                {
                    case "PAYPAL":
                        responseModel = await paypalService.PaypalTokenService(CLIENT_ID, CLIENT_SECRET);
                        break; 
                    
                    case "CULQI":
                        responseModel = await paypalService.PaypalTokenService(CLIENT_ID, CLIENT_SECRET);
                        break;

                    default:
                        break;
                }
            }
            return responseModel;
        }




        //list pasarela by keyempresa
        public ResponseApi AccountPaymentsRepo(string DefaultKeyEmpresa)
        {
            ResponseApi responseApi = new ResponseApi();
            try
            {
                List<PasarelaEmpresaDTO> list = new List<PasarelaEmpresaDTO>();
                PasarelaEmpresaDTO oPasarelaEmpresaDTO = new PasarelaEmpresaDTO();
                oPasarelaEmpresaDTO.DefaultKeyEmpresa = DefaultKeyEmpresa;
                ReqFilterPasarelaEmpresaDTO oReq = new ReqFilterPasarelaEmpresaDTO()
                {
                    FilterCase = FilterCasePasarelaEmpresa.ListApi,
                    Item = oPasarelaEmpresaDTO,
                    User = "Admin",
                    Paging = new E_DataModel.Common.Paging()
                    {
                        All = true,
                        PageNumber = 0,
                        PageRecords = 9999
                    }
                };
                RespListPasarelaEmpresaDTO oResp = null;
                using (PasarelaEmpresaLogic ologic = new PasarelaEmpresaLogic())
                {
                    oResp = ologic.GetList(oReq);
                }
                if (oResp.Success)
                {
                    responseApi.Date = oResp.List;
                    responseApi.Success = true;
                }
            }
            catch (Exception ex)
            {
                responseApi.Status = 1;
                responseApi.Message1 = ex.Message;
            }
            return responseApi;
        }


        //get account payment by code
        public PasarelaEmpresaDTO GetItemAccountPaymentsRepo(string DefaultKeyEmpresa, string CodigoPlantillaFormaPago)
        {
            PasarelaEmpresaDTO oEmpresaDTO = new PasarelaEmpresaDTO();
            oEmpresaDTO.DefaultKeyEmpresa = DefaultKeyEmpresa;
            oEmpresaDTO.CodigoPlantillaFormaPago = CodigoPlantillaFormaPago;

            ReqFilterPasarelaEmpresaDTO oReq = new ReqFilterPasarelaEmpresaDTO()
            {
                FilterCase = FilterCasePasarelaEmpresa.SearchCodeApi,
                Item = oEmpresaDTO,
                User = "Admin",
            };
            RespItemPasarelaEmpresaDTO oResp = null;
            using (PasarelaEmpresaLogic oLogic = new PasarelaEmpresaLogic())
            {
                oResp = oLogic.GetItem(oReq);
            }
            if (oResp.Success)
            {
                oEmpresaDTO = oResp.Item;
            }
            return oEmpresaDTO;
        }



    }
}