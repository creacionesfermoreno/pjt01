using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using BotComers.Controllers;
using BotComers.Helpers;
using BotComers.Repository.PasarelaEmpresaServices;
using E_BusinessLayer.Gimnasio;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using Operation = E_DataModel.Common.Operation;

namespace BotComers.Repository
{
    public class PasarelaRepository
    {

        //***************************************** CULQI ***************************

        //validate credential
        public ResponseModel ValidCredentialCulqRep(string kpublic, string kprivate)
        {
            ResponseModel response = new ResponseModel();
            PasarelaEmpresaService services = new PasarelaEmpresaService();
            var key = services.validatekeyService(kpublic);
            var keyPrivate = services.validatekeyPrivateService(kprivate);

            if (key.Success == true && keyPrivate.Success == true)
            {
                response.Success = true;
                response.Message1 = key.Message1;
            }
            else
            {
                response.Message1 = "credenciales ingresados es inválida";
                response.Status = 1;
                response.Success = false;
            }

            return response;
        }



        //***************************************** END CULQI ***************************



        //***************************************** PAYPAL ***************************

        //validate credencial paypal
        public async Task<ResponseModel> ValidCredentialPaypalRep(string clientId, string secretId, bool entorno)
        {
            ResponseModel response = new ResponseModel();
            PasarelaEmpresaService pservice = new PasarelaEmpresaService();
            var resp = await pservice.PaypalTokenService(clientId, secretId, entorno);

            if (resp.Success == false)
            {
                response.Message1 = "credenciales ingresados es inválida";
                response.Status = 1;
                response.Success = false;
            }
            else
            {
                response.Message1 = resp.Message1;
                response.Success = true;
            }

            return response;
        }
        //***************************************** END PAYPAL ***********************************

        //register pasarela pago
        public ResponseModel registerUpAccountPay(Dictionary<string, dynamic> parms)
        {

            ResponseModel response = new ResponseModel();
            try
            {
                List<PasarelaEmpresaDTO> list = new List<PasarelaEmpresaDTO>();
                list.Add(new PasarelaEmpresaDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    UsuarioCreacion = Commun.Usuario,
                    CodigoPlantillaFormaPago = parms["code"],
                    Valor1 = parms["kpublic"],
                    Valor2 = parms["kpri"],
                    Valor3 = "--",
                    Estado = Convert.ToBoolean(parms["status"]),
                    EstadoProduccion = Convert.ToBoolean(parms["entorno"]),
                    Operation = Convert.ToBoolean(parms["created"]) ? Operation.RegisterPEmpresa: Operation.UpdatePEmpresa,
                    
                });
                ReqPasarelaEmpresaDTO oReq = new ReqPasarelaEmpresaDTO()
                {
                    List = list,
                    User = Commun.Usuario
                };

                RespPasarelaEmpresaDTO oResp = null;
                using (PasarelaEmpresaLogic logic = new PasarelaEmpresaLogic())
                {
                    oResp = logic.ExecuteTransac(oReq);
                }
                if (oResp.Success)
                {
                    response.Message1 = oResp.MessageList[0].Detalle;
                    response.Status = 0;
                }
            }
            catch (Exception ex)
            {
                response.Status = 1;
                response.Message1 = ex.Message;
            }

            return response;
        }


       //get item account paymet

        public PasarelaEmpresaDTO getItemAccount(string code)
        {
            PasarelaEmpresaDTO oPasarelaEmpresaDTO = new PasarelaEmpresaDTO();
            oPasarelaEmpresaDTO.CodigoSede = Commun.CodigoSede;
            oPasarelaEmpresaDTO.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oPasarelaEmpresaDTO.CodigoPlantillaFormaPago = code;
            ReqFilterPasarelaEmpresaDTO oReq = new ReqFilterPasarelaEmpresaDTO()
            {
                FilterCase = FilterCasePasarelaEmpresa.SearchByCode,
                Item = oPasarelaEmpresaDTO,
                User = Commun.Usuario,
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