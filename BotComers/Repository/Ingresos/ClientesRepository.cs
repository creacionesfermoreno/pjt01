using BotComers.ViewModels.Ingresos;
using E_BusinessLayer;
using E_DataModel;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.Ingresos
{
    public class ClientesRepository : IDisposable
    {
        public List<ClientesViewModel> ecommerce_uspBuscadorClientes(ClientesViewModel request)
        {
            List<ClientesViewModel> lista = new List<ClientesViewModel>();

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                Item = new ClientesDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    Nombres = request.Nombres
                },
                FilterCase = filterCaseClientes.ecommerce_uspBuscadorClientes,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oClientesLogic = new ClientesLogic())
            {
                oResp = oClientesLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                if (oResp.List != null)
                {
                    foreach (ClientesDTO item in oResp.List)
                    {
                        lista.Add(new ClientesViewModel()
                        {
                            CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                            CodigoSede = item.CodigoSede,
                            CodigoCliente = item.CodigoCliente,
                            Nombres = item.Nombres,
                            Apellidos = item.Apellidos,
                            Telefono = item.Telefono,
                            CorreoElectronico = item.CorreoElectronico,
                            NombreCompleto = item.Nombres + " " + item.Apellidos
                        });
                    }
                }

            }
            return lista;
        }

        public ClientesViewModel ecommerce_uspBuscadorClientesPorIdentificacion(ClientesViewModel request)
        {
            ClientesViewModel response = new ClientesViewModel();

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                Item = new ClientesDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    Identificacion = request.Identificacion
                },
                FilterCase = filterCaseClientes.ecommerce_uspBuscadorClientesPorIdentificacion,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespItemClientesDTO oResp = null;

            using (ClientesLogic oClientesLogic = new ClientesLogic())
            {
                oResp = oClientesLogic.ClientesGetItem(oReq);
            }

            if (oResp.Success)
            {
                if (oResp.Item != null)
                {
                    response.Identificacion = oResp.Item.Identificacion;
                    response.CodigoCliente = oResp.Item.CodigoCliente;
                }

            }
            return response;
        }

        public int ecommerce_uspRegistrarClientes(ClientesViewModel oItem)
        {
            int CodigoCliente = 0;

            List<ClientesDTO> list = new List<ClientesDTO>();

            list.Add(new ClientesDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                CodigoCliente = oItem.CodigoCliente,
                Nombres = oItem.Nombres,
                Apellidos = oItem.Apellidos,
                CodigoTipoIdentificacion = oItem.CodigoTipoIdentificacion,
                Identificacion = oItem.Identificacion == null ? string.Empty : oItem.Identificacion,
                Direccion = oItem.Direccion == null ? string.Empty : oItem.Direccion,
                Departamento = oItem.Departamento == null ? string.Empty : oItem.Departamento,
                provincia = oItem.provincia == null ? string.Empty : oItem.provincia,
                Distrito = oItem.Distrito == null ? string.Empty : oItem.Distrito,
                Urbanizacion = oItem.Urbanizacion == null ? string.Empty : oItem.Urbanizacion,
                CodigoUbigeo = oItem.CodigoUbigeo == null ? string.Empty : oItem.CodigoUbigeo,
                CorreoElectronico = oItem.CorreoElectronico == null ? string.Empty : oItem.CorreoElectronico,
                Telefono = oItem.Telefono == null ? string.Empty : oItem.Telefono,
                Celular = oItem.Celular == null ? string.Empty : oItem.Celular,
                CodigoVendedor = oItem.CodigoVendedor,
                DireccionDelivery = oItem.DireccionDelivery == null ? string.Empty : oItem.DireccionDelivery,
                UsuarioCreacion = oItem.UsuarioCreacion,
                Operation = oItem.Accion == "N" ? Operation.Create : Operation.Update,

            });

            ReqClientesDTO oReq = new ReqClientesDTO()
            {
                List = list,
                User = "admin"
            };
            RespClientesDTO oResp = null;
            using (ClientesLogic oClientesLogic = new ClientesLogic())
            {
                oResp = oClientesLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                CodigoCliente = oResp.MessageList[0].Codigo;
            }

            return CodigoCliente;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}