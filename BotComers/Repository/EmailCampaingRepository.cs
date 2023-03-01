using BotComers.Helpers;
using BotComers.ViewModels;
using E_BusinessLayer;
using E_BusinessLayer.Gimnasio;
using E_DataModel;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BotComers.Repository
{
    public class EmailCampaingRepository
    {
        //register email campaign
        public string RegisterECampainRepo(EmailCampaingDTO oItem)
        {
            string code = null;
            try
            {
                List<EmailCampaingDTO> list = new List<EmailCampaingDTO>();

                list.Add(new EmailCampaingDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    CodigoCorreoCampania = oItem.CodigoCorreoCampania,
                    NombreCorreoCampania = oItem.NombreCorreoCampania,
                    UrlDestinatarios = oItem.UrlDestinatarios,
                    SendCorreo = oItem.SendCorreo,
                    Content_html = oItem.Content_html,
                    UsuarioCreacion = Commun.Usuario,
                    Operation = oItem.Action == "created" ? Operation.Create : Operation.Update,
                });

                ReqEmailCampaingDTO oReq = new ReqEmailCampaingDTO()
                {
                    List = list,
                    User = "admin"
                };
                RespEmailCampaingDTO oResp = null;
                EmailCampaingLogic oLogic = new EmailCampaingLogic();
                oResp = oLogic.ExecuteTransac(oReq);
                if (oResp.Success)
                {
                    code = oResp.MessageList[0].Code.ToString();
                }
            }
            catch (Exception)
            {

            }
            return code;
        }


        //register email campaign - detail
        public string RegisterECampainDetailRepo(string CodigoCorreoCampania, string Destinatario, bool EstadoCorreoCampania)
        {
            string code = null;
            try
            {
                List<EmailCampaingDTO> list = new List<EmailCampaingDTO>();

                list.Add(new EmailCampaingDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    CodigoCorreoCampania = CodigoCorreoCampania,
                    CodigoCorreoCampaniaDetalle = "",
                    Destinatario = Destinatario,
                    EstadoCorreoCampania = EstadoCorreoCampania,
                    Operation = Operation.CampaingRegisterDetail,
                });

                ReqEmailCampaingDTO oReq = new ReqEmailCampaingDTO()
                {
                    List = list,
                    User = "admin"
                };
                RespEmailCampaingDTO oResp = null;
                EmailCampaingLogic oLogic = new EmailCampaingLogic();
                oResp = oLogic.ExecuteTransac(oReq);
                if (oResp.Success)
                {
                    code = oResp.MessageList[0].Code.ToString();
                }
            }
            catch (Exception)
            {

            }
            return code;
        }


        //register files email campaign 
        public int RegisterECampainFilesRepo(string CodigoCorreoCampania, string url)
        {
            int code = 0;
            try
            {
                List<EmailCampaingDTO> list = new List<EmailCampaingDTO>();

                list.Add(new EmailCampaingDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    CodigoCorreoCampania = CodigoCorreoCampania,
                    CodigoCorreoCampaniaArchivosAdjuntos = 0,
                    UrlArchivosAdjunto = url,
                    Operation = Operation.CampaingRegisterFile,
                });

                ReqEmailCampaingDTO oReq = new ReqEmailCampaingDTO()
                {
                    List = list,
                    User = "admin"
                };
                RespEmailCampaingDTO oResp = null;
                EmailCampaingLogic oLogic = new EmailCampaingLogic();
                oResp = oLogic.ExecuteTransac(oReq);
                if (oResp.Success)
                {
                    code = oResp.MessageList[0].Codigo;
                }
            }
            catch (Exception)
            {

            }
            return code;
        }


        //update sendcorreo
        public bool UpdateSendCorreoECampainRepo(string code = "")
        {
            bool resp = false;
            try
            {
                List<EmailCampaingDTO> list = new List<EmailCampaingDTO>();

                list.Add(new EmailCampaingDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    CodigoCorreoCampania = code,
                    Operation = Operation.UpdateSendCampaing,
                });

                ReqEmailCampaingDTO oReq = new ReqEmailCampaingDTO()
                {
                    List = list,
                    User = "admin"
                };
                RespEmailCampaingDTO oResp = null;
                EmailCampaingLogic oLogic = new EmailCampaingLogic();
                oResp = oLogic.ExecuteTransac(oReq);
                if (oResp.Success)
                {
                    resp = true;
                }
            }
            catch (Exception)
            {
                resp = false;
            }
            return resp;

        }


        //get item email campaign
        public EmailCampaingDTO getItemECampRepo(string code)
        {
            EmailCampaingDTO oDTO = new EmailCampaingDTO();
            try
            {
                oDTO.CodigoSede = Commun.CodigoSede;
                oDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
                oDTO.CodigoCorreoCampania = code;
                ReqFilterEmailCampaingDTO oReq = new ReqFilterEmailCampaingDTO()
                {
                    FilterCase = FilterEmailCampaing.Search,
                    Item = oDTO,
                    User = Commun.Usuario,
                };
                RespItemEmailCampaingDTO oResp = null;
                EmailCampaingLogic oLogic = new EmailCampaingLogic();

                oResp = oLogic.GetItem(oReq);

                if (oResp.Success)
                {
                    oDTO = oResp.Item;

                }
            }
            catch (Exception) { }
            return oDTO;
        }


        //get files by code email campaign
        public List<EmailCampaingDTO> filesByECamp(string code = "")
        {

            List<EmailCampaingDTO> lista = new List<EmailCampaingDTO>();

            EmailCampaingDTO oDto = new EmailCampaingDTO();
            oDto.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oDto.CodigoSede = Commun.CodigoSede;
            oDto.CodigoCorreoCampania = code;

            ReqFilterEmailCampaingDTO oReq = new ReqFilterEmailCampaingDTO()

            {
                FilterCase = FilterEmailCampaing.ListFiles,
                Item = oDto,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 99999
                }
            };

            RespListEmailCampaingDTO oResp = null;

            EmailCampaingLogic oLogic = new EmailCampaingLogic();
            oResp = oLogic.GetList(oReq);

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;

        }




    }
}