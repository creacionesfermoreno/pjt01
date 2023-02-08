using BotComers.Controllers;
using BotComers.Helpers;
using BotComers.Repository.WhatsappServices;
using E_BusinessLayer.Gimnasio;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using iTextSharp.text.pdf.qrcode;
using MercadoPago;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BotComers.Repository.Gimnasio
{
    public class WhatsappRepository : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public WhatsappConfigDTO getItemByCodeRepository(string code)
        {
            WhatsappConfigDTO oWhatsappConfigDTO = new WhatsappConfigDTO();
            oWhatsappConfigDTO.CodigoSede = Commun.CodigoSede;
            oWhatsappConfigDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oWhatsappConfigDTO.CodigoWhatsappConfiguracion = code;
            ReqFilterWhatsappConfigDTO oReq = new ReqFilterWhatsappConfigDTO()
            {
                FilterCase = FilterCaseGlobal.SearchByCode,
                Item = oWhatsappConfigDTO,
                User = Commun.Usuario,
            };
            RespItemWhatsappConfigDTO oResp = null;
            using (WhatsappConfigLogic oWhatsappConfigLogic = new WhatsappConfigLogic())
            {
                oResp = oWhatsappConfigLogic.GetItem(oReq);
            }
            if (oResp.Success)
            {
                oWhatsappConfigDTO = oResp.Item;
                
            }
            return oWhatsappConfigDTO;

        }


        //get campaign
        public WhatsappConfigDTO getItemCampaignRepository(string code)
        {
            WhatsappConfigDTO oWhatsappConfigDTO = new WhatsappConfigDTO();
            oWhatsappConfigDTO.CodigoSede = Commun.CodigoSede;
            oWhatsappConfigDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oWhatsappConfigDTO.CodigoWhatsappCampania = code;
            ReqFilterWhatsappConfigDTO oReq = new ReqFilterWhatsappConfigDTO()
            {
                FilterCase = FilterCaseGlobal.SearchByCodeCamp,
                Item = oWhatsappConfigDTO,
                User = Commun.Usuario,
            };
            RespItemWhatsappConfigDTO oResp = null;
            using (WhatsappConfigLogic oWhatsappConfigLogic = new WhatsappConfigLogic())
            {
                oResp = oWhatsappConfigLogic.GetItem(oReq);
            }
            if (oResp.Success)
            {
                oWhatsappConfigDTO = oResp.Item;

            }
            return oWhatsappConfigDTO;
        }

        public bool updateCampaignStateRepository(string code, bool status)
        {
            bool resp = false;
            try
            {
                List<WhatsappConfigDTO> list = new List<WhatsappConfigDTO>();
                list.Add(new WhatsappConfigDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    UsuarioCreacion = Commun.Usuario,
                    CodigoWhatsappCampania = code,
                    EstadoWhatsappCampania = status,
                    Operation = Operation.UpdateCampStatu,
                }); ;
                ReqWhatsappConfigDTO oReq = new ReqWhatsappConfigDTO()
                {
                    List = list,
                    User = Commun.Usuario
                };

                RespWhatsappConfigDTO oResp = null;
                using (WhatsappConfigLogic logic = new WhatsappConfigLogic())
                {
                    oResp = logic.ExecuteTransac(oReq);
                }
                if (oResp.Success)
                {
                    resp = true;
                }
                else
                {
                    resp = false;
                }
            }
            catch (Exception)
            {

                resp = false;
            }
            return resp;

        }

        //******************************************************************************* CAMPAIGN DETAIL***********************************************

        //campaing detail register
        public bool registerCampaignDetailRepository(string codecampaign,string customer,string phone,bool status)
        {
            bool resp = false;
            try
            {
                List<WhatsappConfigDTO> list = new List<WhatsappConfigDTO>();
                list.Add(new WhatsappConfigDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    UsuarioCreacion = Commun.Usuario,
                    CodigoWhatsappCampania = codecampaign,
                    CodigoWhatsappCampaniaDetalle = "0",
                    Destinatario = customer,
                    Phone = phone,
                    EstadoWhatsappCampania = status,
                    Operation = Operation.RegisterCampDetail,
                }); ;
                ReqWhatsappConfigDTO oReq = new ReqWhatsappConfigDTO()
                {
                    List = list,
                    User = Commun.Usuario
                };

                RespWhatsappConfigDTO oResp = null;
                using (WhatsappConfigLogic logic = new WhatsappConfigLogic())
                {
                    oResp = logic.ExecuteTransac(oReq);
                }
                if (oResp.Success)
                {
                    resp = true;
                }
                else
                {
                    resp = false;
                }
            }
            catch (Exception)
            {

                resp = false;
            }
            return resp;
            
        }


        //******************************************************************************* CAMPAIGN DETAIL***********************************************



    }
}