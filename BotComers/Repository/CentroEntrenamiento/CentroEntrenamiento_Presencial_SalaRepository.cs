using E_BusinessLayer.CentroEntrenamiento;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.CentroEntrenamiento
{
    public class CentroEntrenamiento_Presencial_SalaRepository : IDisposable
    {

        public List<CentroEntrenamiento_Presencial_SalaDTO> CentroEntrenamiento_uspListarSala_Presencial(CentroEntrenamiento_Presencial_SalaDTO request)
        {
            List<CentroEntrenamiento_Presencial_SalaDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_SalaDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_SalaDTO()
            {
                Item = new CentroEntrenamiento_Presencial_SalaDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_Sala.CentroEntrenamiento_uspListarSala_Presencial,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_SalaDTO oResp = null;

            using (CentroEntrenamiento_Presencial_SalaLogic oCentroEntrenamiento_Presencial_SalaLogic = new CentroEntrenamiento_Presencial_SalaLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_SalaLogic.CentroEntrenamiento_Presencial_SalaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_SalaDTO>();
                lista = oResp.List;
            }

            return lista;
        }

        public List<CentroEntrenamiento_Presencial_SalaDTO> CentroEntrenamiento_uspListarSalaMaquinas_Presencial(CentroEntrenamiento_Presencial_SalaDTO request)
        {
            List<CentroEntrenamiento_Presencial_SalaDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_SalaDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_SalaDTO()
            {
                Item = new CentroEntrenamiento_Presencial_SalaDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_Sala.CentroEntrenamiento_uspListarSalaMaquinas_Presencial,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_SalaDTO oResp = null;

            using (CentroEntrenamiento_Presencial_SalaLogic oCentroEntrenamiento_Presencial_SalaLogic = new CentroEntrenamiento_Presencial_SalaLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_SalaLogic.CentroEntrenamiento_Presencial_SalaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_SalaDTO>();
                lista = oResp.List;
            }

            return lista;
        }


        public int CentroEntrenamiento_uspRegistrarSala_Presencial(CentroEntrenamiento_Presencial_SalaDTO request)
        {
            int mensaje = 0;

            List<CentroEntrenamiento_Presencial_SalaDTO> list = new List<CentroEntrenamiento_Presencial_SalaDTO>();

            list.Add(new CentroEntrenamiento_Presencial_SalaDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                CodigoSala = request.CodigoSala,
                Descripcion = request.Descripcion,
                UsuarioCreacion = request.UsuarioCreacion,
                Operation = Operation.Create
            });

            ReqCentroEntrenamiento_Presencial_SalaDTO oReq = new ReqCentroEntrenamiento_Presencial_SalaDTO()
            {
                List = list,
                User = "admin"
            };
            RespCentroEntrenamiento_Presencial_SalaDTO oResp = null;
            using (CentroEntrenamiento_Presencial_SalaLogic oCentroEntrenamiento_Presencial_SalaLogic = new CentroEntrenamiento_Presencial_SalaLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_SalaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return mensaje;
        }

        public int CentroEntrenamiento_uspRegistrarSalaMaquinas_Presencial(CentroEntrenamiento_Presencial_SalaDTO request)
        {
            int mensaje = 0;

            List<CentroEntrenamiento_Presencial_SalaDTO> list = new List<CentroEntrenamiento_Presencial_SalaDTO>();

            list.Add(new CentroEntrenamiento_Presencial_SalaDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                CodigoSala = request.CodigoSala,
                Descripcion = request.Descripcion,
                UsuarioCreacion = request.UsuarioCreacion,
                Operation = Operation.CentroEntrenamiento_uspRegistrarSalaMaquinas_Presencial
            });

            ReqCentroEntrenamiento_Presencial_SalaDTO oReq = new ReqCentroEntrenamiento_Presencial_SalaDTO()
            {
                List = list,
                User = "admin"
            };
            RespCentroEntrenamiento_Presencial_SalaDTO oResp = null;
            using (CentroEntrenamiento_Presencial_SalaLogic oCentroEntrenamiento_Presencial_SalaLogic = new CentroEntrenamiento_Presencial_SalaLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_SalaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return mensaje;
        }

        public int CentroEntrenamiento_uspEditarSala_Presencial(CentroEntrenamiento_Presencial_SalaDTO request)
        {
            int mensaje = 0;

            List<CentroEntrenamiento_Presencial_SalaDTO> list = new List<CentroEntrenamiento_Presencial_SalaDTO>();

            list.Add(new CentroEntrenamiento_Presencial_SalaDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                CodigoSala = request.CodigoSala,
                Descripcion = request.Descripcion,
                Operation = Operation.Update
            });

            ReqCentroEntrenamiento_Presencial_SalaDTO oReq = new ReqCentroEntrenamiento_Presencial_SalaDTO()
            {
                List = list,
                User = "admin"
            };
            RespCentroEntrenamiento_Presencial_SalaDTO oResp = null;
            using (CentroEntrenamiento_Presencial_SalaLogic oCentroEntrenamiento_Presencial_SalaLogic = new CentroEntrenamiento_Presencial_SalaLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_SalaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return mensaje;
        }

        public int CentroEntrenamiento_uspEliminarSala_Presencial(CentroEntrenamiento_Presencial_SalaDTO request)
        {
            int mensaje = 0;

            List<CentroEntrenamiento_Presencial_SalaDTO> list = new List<CentroEntrenamiento_Presencial_SalaDTO>();

            list.Add(new CentroEntrenamiento_Presencial_SalaDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                CodigoSala = request.CodigoSala,
                Descripcion = request.Descripcion,
                Operation = Operation.Delete
            });

            ReqCentroEntrenamiento_Presencial_SalaDTO oReq = new ReqCentroEntrenamiento_Presencial_SalaDTO()
            {
                List = list,
                User = "admin"
            };
            RespCentroEntrenamiento_Presencial_SalaDTO oResp = null;
            using (CentroEntrenamiento_Presencial_SalaLogic oCentroEntrenamiento_Presencial_SalaLogic = new CentroEntrenamiento_Presencial_SalaLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_SalaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return mensaje;

        }

        public int CentroEntrenamiento_uspEliminarSalaMaquinas_Presencial(CentroEntrenamiento_Presencial_SalaDTO request)
        {
            int mensaje = 0;

            List<CentroEntrenamiento_Presencial_SalaDTO> list = new List<CentroEntrenamiento_Presencial_SalaDTO>();

            list.Add(new CentroEntrenamiento_Presencial_SalaDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                CodigoSala = request.CodigoSala,
                Descripcion = request.Descripcion,
                Operation = Operation.CentroEntrenamiento_uspEliminarSalaMaquinas_Presencial
            });

            ReqCentroEntrenamiento_Presencial_SalaDTO oReq = new ReqCentroEntrenamiento_Presencial_SalaDTO()
            {
                List = list,
                User = "admin"
            };
            RespCentroEntrenamiento_Presencial_SalaDTO oResp = null;
            using (CentroEntrenamiento_Presencial_SalaLogic oCentroEntrenamiento_Presencial_SalaLogic = new CentroEntrenamiento_Presencial_SalaLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_SalaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return mensaje;

        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }

}