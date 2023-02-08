using E_BusinessLayer.Configuracion;
using E_DataModel.Common;
using E_DataModel.Configuracion;
using System;
using System.Collections.Generic;

namespace AppsfitWebApi.Repository
{
    public class AspNetUsersRepository : IDisposable
    {
        public int ecommerce_uspRegistrar_AspNetUsersToken_AppFitness(AspNetUsersDTO oitem)
        {
            List<AspNetUsersDTO> list = new List<AspNetUsersDTO>();
            int validacion = 0;
            list.Add(new AspNetUsersDTO()
            {
                Id = oitem.Id,
                DefaultKey = oitem.DefaultKey,
                Operation = Operation.ecommerce_uspRegistrar_AspNetUsersToken_AppFitness,
            });

            ReqAspNetUsersDTO oReq = new ReqAspNetUsersDTO()
            {
                List = list,
                User = "admin"
            };
            RespAspNetUsersDTO oResp = null;
            using (AspNetUsersLogic oAspNetUsersLogic = new AspNetUsersLogic())
            {
                oResp = oAspNetUsersLogic.ExecuteTransac(oReq);
            }

            if (oResp.Success)
            {
                validacion = 0; //correcto
            }
            else
            {
                validacion = 1; //problemas con internet o para traer información
            }

            return validacion;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}