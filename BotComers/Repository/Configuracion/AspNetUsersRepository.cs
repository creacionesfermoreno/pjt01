using BotComers.ViewModels.Configuracion;
using E_BusinessLayer.Configuracion;
using E_DataModel.Common;
using E_DataModel.Configuracion;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.Configuracion
{
    public class AspNetUsersRepository : IDisposable
    {
        public int ecommerce_AspNetUsers_ValidarUsuarioBusiness(AspNetUsersViewModel oitem)
        {
            AspNetUsersViewModel oItemViewModel = null;

            AspNetUsersDTO oAspNetUsersDTO = new AspNetUsersDTO();
            oAspNetUsersDTO.CodigoUnidadNegocio = oitem.CodigoUnidadNegocio;
            oAspNetUsersDTO.CodigoSede = oitem.CodigoSede;
            oAspNetUsersDTO.UserName = oitem.UserName;
            oAspNetUsersDTO.PasswordHash = oitem.PasswordHash;

            ReqFilterAspNetUsersDTO oReq = new ReqFilterAspNetUsersDTO()
            {
                FilterCase = filterCaseAspNetUsers.ecommerce_AspNetUsers_ValidarUsuarioBusiness,
                Item = oAspNetUsersDTO,
                User = "appsfit"
            };
            RespItemAspNetUsersDTO oResp = null;
            using (AspNetUsersLogic oAspNetUsersLogic = new AspNetUsersLogic())
            {
                oResp = oAspNetUsersLogic.AspNetUsersGetItem(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new AspNetUsersViewModel();

                oItemViewModel.CodigoUnidadNegocio = oResp.Item.CodigoUnidadNegocio;
                oItemViewModel.CodigoSede = oResp.Item.CodigoSede;
                oItemViewModel.LoginValidation = oResp.Item.LoginValidation;

            }

            return oItemViewModel.LoginValidation;
        }
        public AspNetUsersViewModel ecommerce_AspNetUsers_ValidarUsuarioPersonaFit(AspNetUsersViewModel oitem)
        {
            AspNetUsersViewModel oItemViewModel = null;

            AspNetUsersDTO oAspNetUsersDTO = new AspNetUsersDTO();
            oAspNetUsersDTO.CodigoUnidadNegocio = oitem.CodigoUnidadNegocio;
            oAspNetUsersDTO.CodigoSede = oitem.CodigoSede;
            oAspNetUsersDTO.UserName = oitem.UserName;
            oAspNetUsersDTO.PasswordHash = oitem.PasswordHash;

            ReqFilterAspNetUsersDTO oReq = new ReqFilterAspNetUsersDTO()
            {
                FilterCase = filterCaseAspNetUsers.ecommerce_AspNetUsers_ValidarUsuarioPersonaFit,
                Item = oAspNetUsersDTO,
                User = "appsfit"
            };
            RespItemAspNetUsersDTO oResp = null;
            using (AspNetUsersLogic oAspNetUsersLogic = new AspNetUsersLogic())
            {
                oResp = oAspNetUsersLogic.AspNetUsersGetItem(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new AspNetUsersViewModel();

                if (oResp.Item != null)
                {
                    oItemViewModel.Id = oResp.Item.Id;
                    oItemViewModel.LoginValidation = oResp.Item.LoginValidation;
                }
                else
                {
                    oResp.Item = new AspNetUsersDTO();
                    oItemViewModel.Id = "sinregistro";
                    oItemViewModel.LoginValidation = 0;
                }

            }

            return oItemViewModel;
        }

        public string ecommerce_AspNetUsers_ValidarUsuarioTiendaVirtual(AspNetUsersViewModel oitem)
        {
            AspNetUsersViewModel oItemViewModel = new AspNetUsersViewModel();

            AspNetUsersDTO oAspNetUsersDTO = new AspNetUsersDTO();
            oAspNetUsersDTO.CodigoUnidadNegocio = oitem.CodigoUnidadNegocio;
            oAspNetUsersDTO.CodigoSede = oitem.CodigoSede;
            oAspNetUsersDTO.UserName = oitem.UserName;
            oAspNetUsersDTO.PasswordHash = oitem.PasswordHash;

            ReqFilterAspNetUsersDTO oReq = new ReqFilterAspNetUsersDTO()
            {
                FilterCase = filterCaseAspNetUsers.ecommerce_AspNetUsers_ValidarUsuarioPersonaFit,
                Item = oAspNetUsersDTO,
                User = "appsfit"
            };
            RespItemAspNetUsersDTO oResp = null;
            using (AspNetUsersLogic oAspNetUsersLogic = new AspNetUsersLogic())
            {
                oResp = oAspNetUsersLogic.AspNetUsersGetItem(oReq);
            }
            if (oResp.Success)
            {
                if (oResp.Item != null)
                {
                    oItemViewModel.Id = oResp.Item.Id;
                }
                else
                {
                    oItemViewModel.Id = "sinregistro";
                }
            }

            return oItemViewModel.Id;
        }

        public List<AspNetUsersViewModel> ecommerce_uspListarAspNetUsers_Paginacion(AspNetUsersViewModel oItem)
        {
            List<AspNetUsersViewModel> lista = null;

            ReqFilterAspNetUsersDTO oReq = new ReqFilterAspNetUsersDTO()
            {
                Item = new AspNetUsersDTO()
                {
                    CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                    CodigoSede = oItem.CodigoSede,
                    FullName = oItem.FullName
                },
                FilterCase = filterCaseAspNetUsers.ecommerce_uspListarAspNetUsers_Paginacion,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(oItem.PageNumber),
                    PageRecords = 0
                }
            };

            RespListAspNetUsersDTO oResp = null;

            using (AspNetUsersLogic oLogic = new AspNetUsersLogic())
            {
                oResp = oLogic.AspNetUsersGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<AspNetUsersViewModel>();
                foreach (AspNetUsersDTO item in oResp.List)
                {
                    lista.Add(new AspNetUsersViewModel()
                    {
                        CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                        CodigoSede = item.CodigoSede,
                        UserType = item.UserType,
                        Id = item.Id,
                        FullName = item.FullName,
                        UserName = item.UserName,
                        PasswordHash = item.PasswordHash,
                        Email = item.Email,
                        EmailConfirmed = item.EmailConfirmed,
                        PhoneNumber = item.PhoneNumber,
                        PhoneNumberConfirmed = item.PhoneNumberConfirmed,
                        UsuarioCreacion = item.UsuarioCreacion,
                        DesFechaCreacion = item.FechaCreacion.ToString("dd/MM/yyyy"),
                        DesCargo = item.DesCargo
                    });
                }
            }

            return lista;
        }

        public int ecommerce_uspRegistrar_AspNetUsers(AspNetUsersViewModel oItem)
        {
            int Validation = 0;

            List<AspNetUsersDTO> list = new List<AspNetUsersDTO>();

            list.Add(new AspNetUsersDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                UserType = oItem.UserType,
                FullName = oItem.FullName,
                Nombres = oItem.Nombres,
                Apellidos = oItem.Apellidos,
                Photo = oItem.Photo == null ? string.Empty : oItem.Photo,
                UserName = oItem.UserName,
                PasswordHash = oItem.PasswordHash,
                Identificacion = oItem.Identificacion,
                DefaultKey = string.Empty,
                Email = oItem.Email,
                EmailConfirmed = false,
                PhoneNumber = oItem.PhoneNumber,
                PhoneNumberConfirmed = false,
                SecurityStamp = string.Empty,
                Estate = 1,
                CodigoCargo = oItem.CodigoCargo,
                UsuarioCreacion = oItem.UsuarioCreacion,
                Operation = oItem.Accion == "N" ? Operation.Create : Operation.Update,

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
                Validation = oResp.MessageList[0].Codigo;
            }

            return Validation;
        }
        public int ecommerce_uspRegistrar_AspNetUsersTiendaVirtual(AspNetUsersViewModel oItem)
        {
            int Validation = 0;

            List<AspNetUsersDTO> list = new List<AspNetUsersDTO>();

            list.Add(new AspNetUsersDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                UserType = oItem.UserType,
                FullName = oItem.FullName,
                Nombres = oItem.Nombres,
                Apellidos = oItem.Apellidos,
                Photo = oItem.Photo == null ? string.Empty : oItem.Photo,
                UserName = oItem.PasswordHash,
                PasswordHash = oItem.PasswordHash,
                DefaultKey = string.Empty,
                Identificacion = oItem.Identificacion,
                Email = oItem.Email,
                EmailConfirmed = false,
                PhoneNumber = oItem.PhoneNumber,
                PhoneNumberConfirmed = false,
                SecurityStamp = string.Empty,
                Estate = 1,
                CodigoCargo = oItem.CodigoCargo,
                UsuarioCreacion = oItem.UsuarioCreacion,
                Operation = oItem.Accion == "N" ? Operation.ecommerce_uspRegistrar_AspNetUsersTiendaVirtual : Operation.Update,

            });

            ReqAspNetUsersDTO oReq = new ReqAspNetUsersDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespAspNetUsersDTO oResp = null;
            using (AspNetUsersLogic oAspNetUsersLogic = new AspNetUsersLogic())
            {
                oResp = oAspNetUsersLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                Validation = oResp.MessageList[0].Codigo;
            }

            return Validation;
        }

        public int ecommerce_uspActualizarCambiarClave_AspNetUsers(AspNetUsersViewModel oItem)
        {
            int validacion = 0;

            List<AspNetUsersDTO> list = new List<AspNetUsersDTO>();

            list.Add(new AspNetUsersDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                Id = oItem.Id,
                PasswordHashActual = oItem.PasswordHashActual,
                PasswordHashNueva = oItem.PasswordHashNueva,
                UsuarioCreacion = oItem.UsuarioCreacion,
                Operation = Operation.UpdateClave
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
                validacion = oResp.MessageList[0].Codigo;
            }

            return validacion;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}