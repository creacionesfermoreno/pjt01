using E_BusinessLayer.CentroEntrenamiento;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.CentroEntrenamiento
{
    public class CentroEntrenamiento_ProfesorRepository : IDisposable
    {

        public CentroEntrenamiento_ProfesorDTO CentroEntrenamiento_uspBuscarProfesorPorNombres(CentroEntrenamiento_ProfesorDTO request)
        {
            CentroEntrenamiento_ProfesorDTO oItemViewModel = null;

            CentroEntrenamiento_ProfesorDTO oCentroEntrenamiento_ProfesorDTO = new CentroEntrenamiento_ProfesorDTO();
            oCentroEntrenamiento_ProfesorDTO.NroDocumento = request.NroDocumento;
            oCentroEntrenamiento_ProfesorDTO.Nombres = request.Nombres;
            oCentroEntrenamiento_ProfesorDTO.Apellidos = request.Apellidos;

            ReqFilterCentroEntrenamiento_ProfesorDTO oReq = new ReqFilterCentroEntrenamiento_ProfesorDTO()
            {
                FilterCase = filterCaseCentroEntrenamiento_Profesor.CentroEntrenamiento_uspBuscarProfesorPorNombres,
                Item = oCentroEntrenamiento_ProfesorDTO,
                User = "admin"
            };
            RespItemCentroEntrenamiento_ProfesorDTO oResp = null;
            using (CentroEntrenamiento_ProfesorLogic oCentroEntrenamiento_ProfesorLogic = new CentroEntrenamiento_ProfesorLogic())
            {
                oResp = oCentroEntrenamiento_ProfesorLogic.CentroEntrenamiento_ProfesorGetItem(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new CentroEntrenamiento_ProfesorDTO();
                oItemViewModel = oResp.Item;
            }

            if (oItemViewModel == null)
            {
                oItemViewModel = new CentroEntrenamiento_ProfesorDTO();
                oItemViewModel.validacionBusqueda = "vacio";
            }

            return oItemViewModel;

        }

        public string CentroEntrenamiento_uspRegistrarProfesor(CentroEntrenamiento_ProfesorDTO oItem)
        {
            string mensaje = string.Empty;

            List<CentroEntrenamiento_ProfesorDTO> list = new List<CentroEntrenamiento_ProfesorDTO>();

            list.Add(new CentroEntrenamiento_ProfesorDTO()
            {
                CodigoProfesional = oItem.CodigoProfesional,
                Nombres = oItem.Nombres,
                Apellidos = oItem.Apellidos,
                TipoDocumento = oItem.TipoDocumento,
                NroDocumento = oItem.NroDocumento,
                Telefono = oItem.Telefono,
                Celular = oItem.Celular,
                Correo = oItem.Correo,
                FechaNacimiento = oItem.FechaNacimiento,
                ImagenUrl = oItem.ImagenUrl,
                Genero = oItem.Genero,
                Facebook = oItem.Facebook,
                Ubigeo = oItem.Ubigeo,
                Direccion = oItem.Direccion,
                Distrito = oItem.Distrito,
                CostoPorClase = oItem.CostoPorClase,
                DescuentoPorminuto = oItem.DescuentoPorminuto,
                Estado = true,
                UsuarioCreacion = oItem.UsuarioCreacion,
                Operation = oItem.Accion == "N" ? Operation.Create : Operation.Update,
            });

            ReqCentroEntrenamiento_ProfesorDTO oReq = new ReqCentroEntrenamiento_ProfesorDTO()
            {
                List = list,
                User = "admin"
            };
            RespCentroEntrenamiento_ProfesorDTO oResp = null;
            using (CentroEntrenamiento_ProfesorLogic oCentroEntrenamiento_ProfesorLogic = new CentroEntrenamiento_ProfesorLogic())
            {
                oResp = oCentroEntrenamiento_ProfesorLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Detalle;
            }

            return mensaje;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}